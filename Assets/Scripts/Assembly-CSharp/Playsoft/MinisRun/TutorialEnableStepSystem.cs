using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialEnableStepSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public TutorialEnableStepSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TutorialEnableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				TutorialBaseStep step = entity.tutorialEnableStep.Step;
				if (!string.IsNullOrEmpty(step.TargetId))
				{
					stateContext.CreateEntity().AddFindTargetAction(step.TargetId);
				}
				if (step.EnableLockLayer)
				{
					stateContext.CreateEntity().AddDisplayLockLayerAction(step.LayerTransparent);
				}
				if (step.MoveTargetToTop)
				{
					stateContext.CreateEntity().isMoveTargetToTopAction = true;
				}
				if (step.HandOptions.enable)
				{
					stateContext.CreateEntity().AddShowHandAciton(step.HandOptions);
				}
				if (step.HighlightOptions.enable)
				{
					stateContext.CreateEntity().AddHiglightTargetAction(step.HighlightOptions);
				}
				TutorialAction[] enableActions = step.EnableActions;
				foreach (TutorialAction tutorialAction in enableActions)
				{
					if (!(null == tutorialAction))
					{
						tutorialAction.Execute();
					}
				}
				entity.isDestroyed = true;
			}
		}
	}
}
