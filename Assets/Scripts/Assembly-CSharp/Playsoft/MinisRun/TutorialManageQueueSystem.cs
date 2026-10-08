using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialManageQueueSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public TutorialManageQueueSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TutorialStepEnableTriggered.Added(), StateMatcher.TutorialStepDisableTriggered.Added(), StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasTutorialStepEnableTriggered || entity.hasTutorialStepDisableTriggered || entity.hasTutorialDisableStep;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			Queue<TutorialBaseStep> stepsQueue = stateContext.tutorialEntity.tutorialStepsQueue.stepsQueue;
			foreach (StateEntity entity in entities)
			{
				if (entity.hasTutorialDisableStep && stepsQueue.Count > 0)
				{
					TutorialBaseStep tutorialBaseStep = stepsQueue.Dequeue();
					if (tutorialBaseStep != null)
					{
						EnableStep(tutorialBaseStep);
					}
				}
				if (entity.hasTutorialStepEnableTriggered)
				{
					if (stepsQueue.Count <= 0 && !stateContext.tutorialEntity.hasTutorialActiveStep)
					{
						EnableStep(entity.tutorialStepEnableTriggered.TutorialStep);
					}
					else
					{
						stepsQueue.Enqueue(entity.tutorialStepEnableTriggered.TutorialStep);
					}
				}
				else if (entity.hasTutorialStepDisableTriggered)
				{
					TutorialBaseStep tutorialStep = entity.tutorialStepDisableTriggered.TutorialStep;
					if (stateContext.tutorialEntity.hasTutorialActiveStep && stateContext.tutorialEntity.tutorialActiveStep.Step == tutorialStep)
					{
						DisableStep(tutorialStep);
					}
				}
				entity.isDestroyed = true;
			}
		}

		private void EnableStep(TutorialBaseStep tutorialStep)
		{
			stateContext.tutorialEntity.ReplaceTutorialActiveStep(tutorialStep);
			stateContext.CreateEntity().AddTutorialEnableStep(tutorialStep);
		}

		private void DisableStep(TutorialBaseStep tutorialStep)
		{
			if (stateContext.tutorialEntity.hasTutorialActiveStep)
			{
				stateContext.tutorialEntity.RemoveTutorialActiveStep();
			}
			stateContext.CreateEntity().AddTutorialDisableStep(tutorialStep);
		}
	}
}
