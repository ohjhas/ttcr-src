using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class TutorialDisplayLockLayerSystem : ReactiveSystem<StateEntity>
	{
		private Tweener disableTweener;

		public TutorialDisplayLockLayerSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.DisplayLockLayerAction.Added(), StateMatcher.TutorialDisableStep.Added());
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
					TutorialLayer tutorialLayer = TutorialHelpers.TutorialLayer;
					if (tutorialLayer != null)
					{
						Image overlay = tutorialLayer.OverlayImage;
						if (overlay != null)
						{
							disableTweener = overlay.DOFade(0f, 0.3f).OnComplete(delegate
							{
								if (overlay != null)
								{
									overlay.enabled = false;
								}
							});
						}
					}
				}
				else
				{
					if (disableTweener != null)
					{
						disableTweener.Kill();
					}
					TutorialLayer tutorialLayer2 = TutorialHelpers.TutorialLayer;
					if (tutorialLayer2 != null)
					{
						Image overlayImage = tutorialLayer2.OverlayImage;
						if (overlayImage != null)
						{
							overlayImage.enabled = true;
							overlayImage.color = new Color(0f, 0f, 0f, 0f);
							if (!entity.displayLockLayerAction.LayerTransparent)
							{
								overlayImage.DOFade(0.6f, 0.8f).SetDelay(0.5f);
							}
						}
					}
				}
				entity.isDestroyed = true;
			}
		}
	}
}
