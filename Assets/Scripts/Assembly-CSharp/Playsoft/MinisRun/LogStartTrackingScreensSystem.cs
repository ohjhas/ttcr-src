using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogStartTrackingScreensSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public LogStartTrackingScreensSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ActiveScreen.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StartCoutingTimeIfNeeded();
		}

		private void StartCoutingTimeIfNeeded()
		{
			if (stateContext.activeScreen != null && (!stateContext.activeScreenEntity.hasLastLoggedScreen || !stateContext.activeScreenEntity.hasLastSceneTransitionTime))
			{
				stateContext.activeScreenEntity.ReplaceLastLoggedScreen(stateContext.activeScreen.ScreenId);
				stateContext.activeScreenEntity.ReplaceLastSceneTransitionTime(DateTime.Now);
			}
		}
	}
}
