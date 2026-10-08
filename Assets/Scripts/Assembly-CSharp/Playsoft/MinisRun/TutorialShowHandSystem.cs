using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialShowHandSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private bool tryShowHand;

		private HandOptions actionSettings;

		private GameObject tutorialHand;

		public TutorialShowHandSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ShowHandAciton.Added(), StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasShowHandAciton)
				{
					tryShowHand = true;
					actionSettings = entity.showHandAciton.Options;
					if (entity.showHandAciton.Options.delay > 0f)
					{
						DOVirtual.DelayedCall(entity.showHandAciton.Options.delay, TryShowHand);
					}
					else
					{
						TryShowHand();
					}
				}
				else
				{
					tryShowHand = false;
					DestroyHand();
				}
				entity.isDestroyed = true;
			}
		}

		private void TryShowHand()
		{
			if (!tryShowHand)
			{
				return;
			}
			tryShowHand = false;
			GameObject handPrefab = actionSettings.handPrefab;
			if (handPrefab != null)
			{
				tutorialHand = Object.Instantiate(handPrefab);
				tutorialHand.transform.SetParent(TutorialHelpers.TutorialLayer.transform);
				tutorialHand.transform.SetAsLastSibling();
				tutorialHand.transform.localScale = Vector3.one;
				tutorialHand.transform.localPosition = Vector3.zero;
				TutorialTargetElement tutorialTargetElement = stateContext.tutorialEntity.tutorialTargetElement;
				if (actionSettings.setAutmaticly && tutorialTargetElement != null)
				{
					tutorialHand.transform.position = tutorialTargetElement.Target.TransformPoint(tutorialTargetElement.Target.rect.center);
					tutorialHand.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, actionSettings.angle));
				}
			}
		}

		private void DestroyHand()
		{
			if (tutorialHand != null)
			{
				Object.Destroy(tutorialHand);
			}
		}
	}
}
