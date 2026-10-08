using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogRestoreProgressSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public LogRestoreProgressSystem(Contexts contexts)
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
				LogRestoreStatus(entities[i].userStateSolveConflict.KeepLocalData);
			}
		}

		private void LogRestoreStatus(bool keepLocalData)
		{
			string text = null;
			text = ((!keepLocalData) ? "restored" : "new");
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("restore", "user-state", "US_UID", text, null, null, null, null, null, false);
		}
	}
}
