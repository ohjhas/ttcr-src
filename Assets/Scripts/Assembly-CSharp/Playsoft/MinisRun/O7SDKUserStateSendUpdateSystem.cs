using System;
using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SDKUserStateSendUpdateSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public O7SDKUserStateSendUpdateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.CurrentRealTime.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasCurrentRealTime && stateContext.localUserStateEntity != null && stateContext.balancingDataEntity != null && (!stateContext.o7SdkEntity.hasRemoteUserState || stateContext.o7SdkEntity.remoteUserState.ResponseCode == 200);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (stateContext.o7SdkEntity.hasUserStateLastUpdateTime)
				{
					UpdateStateIfNeeded(entity.currentRealTime.CurrentTime);
				}
				else
				{
					stateContext.o7SdkEntity.ReplaceUserStateLastUpdateTime(entity.currentRealTime.CurrentTime);
				}
			}
		}

		private void UpdateStateIfNeeded(DateTime currentTime)
		{
			TimeSpan timeSinceLastUpdate = currentTime - stateContext.o7SdkEntity.userStateLastUpdateTime.Time;
			UserStateData stateData = stateContext.localUserStateEntity.localUserState.StateData;
			int num = stateContext.balancingDataEntity.initConfigData.Map[EnumSettings.UserStateUpdatePeriod_inS];
			if (timeSinceLastUpdate.TotalSeconds > (double)num && stateData.IsStateUpdated)
			{
				ReplaceUserState(stateData, currentTime);
				LogUpdate(timeSinceLastUpdate);
			}
		}

		private void ReplaceUserState(UserStateData currentState, DateTime currentTime)
		{
			O7SDKRestoreManager.ForceOverwriteMode forceOverwriteMode = O7SDKRestoreManager.ForceOverwriteMode.None;
			string currentState2 = JsonUtility.ToJson(currentState);
			stateContext.o7SdkEntity.o7SdkUserStateManager.Manager.RestoreStateForApp(forceOverwriteMode, currentState2);
			stateContext.o7SdkEntity.ReplaceUserStateLastUpdateTime(currentTime);
			currentState.IsStateUpdated = false;
		}

		private void LogUpdate(TimeSpan timeSinceLastUpdate)
		{
			if (O7SDKBase.O7SDK_LOG)
			{
				Debug.Log(string.Format("last user state update {0} seconds ago", timeSinceLastUpdate.TotalSeconds.ToString("F2")));
			}
		}
	}
}
