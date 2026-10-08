using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialDisableStepSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public TutorialDisableStepSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				TutorialBaseStep step = entity.tutorialDisableStep.Step;
				TutorialAction[] disableActions = step.DisableActions;
				foreach (TutorialAction tutorialAction in disableActions)
				{
					if (!(null == tutorialAction))
					{
						tutorialAction.Execute();
					}
				}
				RemoveDisabledStep(entity.tutorialDisableStep.Step);
				entity.isDestroyed = true;
			}
		}

		private void RemoveDisabledStep(TutorialBaseStep step)
		{
			if (step.Repeating == GroupRepeating.Always)
			{
				return;
			}
			foreach (TutorialTrigger trigger in stateContext.tutorialEntity.tutorialTriggersList.Triggers)
			{
				if (trigger.Step == step)
				{
					trigger.IsEnabled = false;
				}
			}
		}
	}
}
