using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshAfterLevelUpSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public RefreshAfterLevelUpSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ActiveScreen.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return uiContext.uIEntity.isNewSceneLoaded;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			if ((stateContext.previousScreen.ScreenId.Equals(ScreenId.LevelUpPopup) || stateContext.previousScreen.ScreenId.Equals(ScreenId.ChestRevealScreen)) && stateContext.activeScreen.ScreenId.Equals(ScreenId.SagaMap))
			{
				uiContext.RefreshScreen();
			}
		}
	}
}
