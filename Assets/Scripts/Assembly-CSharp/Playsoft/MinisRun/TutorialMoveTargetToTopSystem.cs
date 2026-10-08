using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class TutorialMoveTargetToTopSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private RectTransform displayedElement;

		private RectTransform placeHolder;

		public TutorialMoveTargetToTopSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.MoveTargetToTopAction.Added(), StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasTutorialDisableStep)
				{
					if (placeHolder != null && displayedElement != null)
					{
						CopyRectTransform(placeHolder, displayedElement);
						Object.Destroy(placeHolder.gameObject);
					}
				}
				else if (stateContext.tutorialEntity.hasTutorialTargetElement)
				{
					displayedElement = stateContext.tutorialEntity.tutorialTargetElement.Target;
					GameObject gameObject = new GameObject("TUTORIAL TARGET PLACEHOLDER");
					placeHolder = gameObject.AddComponent<RectTransform>();
					CopyRectTransform(displayedElement, placeHolder);
					LayoutElement component = displayedElement.GetComponent<LayoutElement>();
					if (component != null)
					{
						LayoutElement to = placeHolder.gameObject.AddComponent<LayoutElement>();
						CopyLayoutElement(component, to);
					}
					displayedElement.SetParent(TutorialHelpers.TutorialLayer.transform);
					displayedElement.SetAsLastSibling();
					DOVirtual.DelayedCall(0.01f, UpdatePositon).SetUpdate(UpdateType.Late, false);
				}
				entity.isDestroyed = true;
			}
		}

		private void UpdatePositon()
		{
			if (displayedElement != null && placeHolder != null)
			{
				displayedElement.position = placeHolder.transform.position;
			}
		}

		private void CopyRectTransform(RectTransform from, RectTransform to)
		{
			to.transform.SetParent(from.parent);
			to.transform.SetSiblingIndex(from.GetSiblingIndex());
			to.anchorMax = from.anchorMax;
			to.anchorMin = from.anchorMin;
			to.sizeDelta = from.sizeDelta;
			to.transform.localScale = from.transform.localScale;
			to.transform.localRotation = from.transform.localRotation;
			to.transform.localPosition = from.transform.localPosition;
			to.anchoredPosition = from.anchoredPosition;
			to.pivot = from.pivot;
		}

		private void CopyLayoutElement(LayoutElement from, LayoutElement to)
		{
			to.ignoreLayout = from.ignoreLayout;
			to.minWidth = from.minWidth;
			to.minHeight = from.minHeight;
			to.preferredWidth = from.preferredWidth;
			to.preferredHeight = from.preferredHeight;
			to.flexibleWidth = from.flexibleWidth;
			to.flexibleHeight = from.flexibleHeight;
		}
	}
}
