using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SDKUserStateSendInitialStateSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public O7SDKUserStateSendInitialStateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.LocalUserState.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			O7SDKRestoreManager.ForceOverwriteMode forceOverwriteMode = O7SDKRestoreManager.ForceOverwriteMode.None;
			string currentState = JsonUtility.ToJson(stateContext.localUserStateEntity.localUserState.StateData);
			stateContext.o7SdkEntity.o7SdkUserStateManager.Manager.RestoreStateForApp(forceOverwriteMode, currentState);
		}
	}
}
