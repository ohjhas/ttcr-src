using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class TutorialSafetyDisableStepSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public TutorialSafetyDisableStepSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.FindTargetAction.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				RectTransform rectTransform = ((!stateContext.tutorialEntity.hasTutorialTargetElement) ? null : stateContext.tutorialEntity.tutorialTargetElement.Target);
				TutorialBaseStep tutorialBaseStep = ((!stateContext.tutorialEntity.hasTutorialActiveStep) ? null : stateContext.tutorialEntity.tutorialActiveStep.Step);
				if (!(tutorialBaseStep != null) || !(tutorialBaseStep.DisableTrigger is TutorialTargetClickedTrigger))
				{
					continue;
				}
				bool flag = false;
				if (rectTransform != null)
				{
					Button button = rectTransform.GetComponent<Button>();
					if (null == button)
					{
						button = rectTransform.GetComponentInChildren<Button>();
					}
					flag = button != null;
				}
				if (!flag)
				{
					DisableStep(tutorialBaseStep);
					Debug.LogError(string.Format("Target ui: {0} was not founded for step: {1}", tutorialBaseStep.TargetId, tutorialBaseStep.Id));
				}
			}
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
