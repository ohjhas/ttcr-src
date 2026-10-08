using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialFindTargetSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public TutorialFindTargetSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.FindTargetAction.Added(), StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasFindTargetAction)
				{
					RectTransform rectTransform = FindTargetUI(entity.findTargetAction.TargetId);
					if (rectTransform != null)
					{
						stateContext.tutorialEntity.ReplaceTutorialTargetElement(rectTransform);
					}
				}
				else if (stateContext.tutorialEntity.hasTutorialTargetElement)
				{
					stateContext.tutorialEntity.RemoveTutorialTargetElement();
				}
				entity.isDestroyed = true;
			}
		}

		private RectTransform FindTargetUI(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				return null;
			}
			TutorialLayer tutorialLayer = TutorialHelpers.TutorialLayer;
			if (tutorialLayer != null)
			{
				TutorialTarget[] componentsInChildren = tutorialLayer.transform.parent.GetComponentsInChildren<TutorialTarget>(true);
				if (componentsInChildren == null)
				{
					return null;
				}
				TutorialTarget[] array = componentsInChildren;
				foreach (TutorialTarget tutorialTarget in array)
				{
					if (string.Equals(id, tutorialTarget.TargetId))
					{
						return tutorialTarget.transform as RectTransform;
					}
				}
			}
			return null;
		}
	}
}
