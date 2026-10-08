using System.Collections.Generic;
using Entitas;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class TutorialTriggerTargetClickedSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private Button target;

		private TutorialBaseStep targetStep;

		public TutorialTriggerTargetClickedSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TutorialEnableStep.Added(), StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return stateContext.tutorialEntity.isTutorialInitialized;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasTutorialDisableStep)
				{
					if (target != null)
					{
						target.onClick.RemoveListener(TriggerTargetClicked);
					}
				}
				else if (entity.hasTutorialEnableStep && stateContext.tutorialEntity.hasTutorialTargetElement)
				{
					targetStep = entity.tutorialEnableStep.Step;
					target = stateContext.tutorialEntity.tutorialTargetElement.Target.GetComponent<Button>();
					if (null == target)
					{
						target = stateContext.tutorialEntity.tutorialTargetElement.Target.GetComponentInChildren<Button>();
					}
					if (target != null)
					{
						target.onClick.AddListener(TriggerTargetClicked);
					}
				}
				entity.isDestroyed = true;
			}
		}

		private void TriggerTargetClicked()
		{
			foreach (TutorialTrigger trigger in stateContext.tutorialEntity.tutorialTriggersList.Triggers)
			{
				TutorialTargetClickedTrigger tutorialTargetClickedTrigger = trigger as TutorialTargetClickedTrigger;
				if (tutorialTargetClickedTrigger != null && tutorialTargetClickedTrigger.Step == targetStep)
				{
					tutorialTargetClickedTrigger.IsTriggered = true;
				}
			}
		}
	}
}
