using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialTriggerScreenDisplayedSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public TutorialTriggerScreenDisplayedSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ActiveScreen.Added(), StateMatcher.PreviousScreen.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return stateContext.tutorialEntity.isTutorialInitialized;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			ScreenId screenId = (stateContext.hasPreviousScreen ? stateContext.previousScreen.ScreenId : ScreenId.Undefined);
			ScreenId screenId2 = (stateContext.hasActiveScreen ? stateContext.activeScreen.ScreenId : ScreenId.Undefined);
			foreach (TutorialTrigger trigger in stateContext.tutorialEntity.tutorialTriggersList.Triggers)
			{
				TutorialScreenOpenedTrigger tutorialScreenOpenedTrigger = trigger as TutorialScreenOpenedTrigger;
				TutorialScreenClosedTrigger tutorialScreenClosedTrigger = trigger as TutorialScreenClosedTrigger;
				if (tutorialScreenOpenedTrigger != null)
				{
					tutorialScreenOpenedTrigger.IsTriggered = tutorialScreenOpenedTrigger.ScreenId == screenId2;
					if (tutorialScreenOpenedTrigger.ScreenId != screenId2)
					{
					}
				}
				if (tutorialScreenClosedTrigger != null)
				{
					tutorialScreenClosedTrigger.IsTriggered = tutorialScreenClosedTrigger.ScreenId == screenId;
					if (tutorialScreenClosedTrigger.ScreenId != screenId)
					{
					}
				}
			}
		}
	}
}
