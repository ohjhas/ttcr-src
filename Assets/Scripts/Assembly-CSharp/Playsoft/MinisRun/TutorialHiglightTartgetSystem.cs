using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialHiglightTartgetSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private bool tryHighlight;

		private HighlightOptions actionSettings;

		private GameObject highlightedObject;

		private Material defaultMaterial;

		public TutorialHiglightTartgetSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.HiglightTargetAction.Added(), StateMatcher.TutorialDisableStep.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasHiglightTargetAction && stateContext.tutorialEntity.hasTutorialTargetElement)
				{
					tryHighlight = true;
					actionSettings = entity.higlightTargetAction.Options;
					if (entity.higlightTargetAction.Options.delay > 0f)
					{
						DOVirtual.DelayedCall(entity.higlightTargetAction.Options.delay, TryHighlight);
					}
					else
					{
						TryHighlight();
					}
				}
				else
				{
					tryHighlight = false;
					DisableHighlight();
				}
				entity.isDestroyed = true;
			}
		}

		private void TryHighlight()
		{
			if (tryHighlight)
			{
				tryHighlight = false;
				RectTransform target = stateContext.tutorialEntity.tutorialTargetElement.Target;
				highlightedObject = target.gameObject;
				HighlightWithPulsating highlightWithPulsating = highlightedObject.GetComponent<HighlightWithPulsating>();
				if (null == highlightWithPulsating)
				{
					highlightWithPulsating = highlightedObject.AddComponent<HighlightWithPulsating>();
				}
				highlightWithPulsating.Init(actionSettings.minScale, actionSettings.maxScale, actionSettings.animSpeed, actionSettings.higlightMaterial);
			}
		}

		private void DisableHighlight()
		{
			if (highlightedObject != null)
			{
				HighlightWithPulsating component = highlightedObject.gameObject.GetComponent<HighlightWithPulsating>();
				component.Disable();
			}
		}
	}
}
