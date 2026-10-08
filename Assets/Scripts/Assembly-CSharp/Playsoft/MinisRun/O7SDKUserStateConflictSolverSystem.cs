using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SDKUserStateConflictSolverSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public O7SDKUserStateConflictSolverSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.UserStateSolveConflict.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				SolveConflict(entities[i].userStateSolveConflict.KeepLocalData);
				stateContext.CreateEntity().AddO7SdkGateDone(entities[i].userStateSolveConflict.KeepLocalData);
				entities[i].isDestroyed = true;
			}
		}

		private void SolveConflict(bool keepLocalData)
		{
			RemoteUserStateComponent remoteUserState = stateContext.o7SdkEntity.remoteUserState;
			if (keepLocalData)
			{
				O7SDKRestoreManager.ForceOverwriteMode forceOverwriteMode = ((remoteUserState.ResponseCode == 406) ? O7SDKRestoreManager.ForceOverwriteMode.New : O7SDKRestoreManager.ForceOverwriteMode.Master);
				string currentState = JsonUtility.ToJson(stateContext.localUserStateEntity.localUserState.StateData);
				stateContext.o7SdkEntity.o7SdkUserStateManager.Manager.RestoreStateForApp(forceOverwriteMode, currentState);
			}
			else
			{
				O7SDKRestoreManager.ForceOverwriteMode forceOverwriteMode2 = O7SDKRestoreManager.ForceOverwriteMode.Restored;
				SendReplaceUserStateEvent(remoteUserState.State);
				string currentState2 = JsonUtility.ToJson(remoteUserState.State);
				stateContext.o7SdkEntity.o7SdkUserStateManager.Manager.RestoreStateForApp(forceOverwriteMode2, currentState2);
			}
		}

		private void SendReplaceUserStateEvent(UserStateData remote)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.AddReplaceUserState(remote);
		}
	}
}
