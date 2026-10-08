using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BaseScreen : BaseDialog
	{
		[Header("Base Screen")]
		[SerializeField]
		private CanvasGroup BgGroup;

		[SerializeField]
		protected CanvasGroup ContentGroup;

		[SerializeField]
		private float BgFadeDuration;

		[SerializeField]
		private float ContentFadeDuration;

		private Tweener fadeTweener;

		protected override void Setup()
		{
			base.Setup();
			fadeTweener = null;
			if (BgGroup != null)
			{
				BgGroup.alpha = 0f;
			}
			if (ContentGroup != null)
			{
				ContentGroup.alpha = 0f;
			}
		}

		public override void SetVisible(bool visible)
		{
			if (visible)
			{
				base.SetVisible(visible);
				if (BgGroup != null && ContentGroup != null)
				{
					BgGroup.interactable = true;
					ContentGroup.interactable = true;
					FadeInAnim();
				}
				return;
			}
			if (BgGroup != null && ContentGroup != null)
			{
				BgGroup.interactable = false;
				ContentGroup.interactable = false;
				FadeOutAnim(delegate
				{
					_003CSetVisible_003E__BaseCallProxy0(visible);
				});
			}
			else
			{
				base.SetVisible(visible);
			}
			base.IsClickable = false;
		}

		private void FadeInAnim()
		{
			if (fadeTweener != null)
			{
				fadeTweener.Kill();
			}
			float duration = BgFadeDuration * (1f - BgGroup.alpha);
			fadeTweener = BgGroup.DOFade(1f, duration).SetUpdate(true).OnComplete(delegate
			{
				duration = ContentFadeDuration * (1f - ContentGroup.alpha);
				fadeTweener = ContentGroup.DOFade(1f, duration).SetUpdate(true).OnComplete(delegate
				{
					fadeTweener = null;
				});
			});
		}

		private void FadeOutAnim(Action callback)
		{
			if (fadeTweener != null)
			{
				fadeTweener.Kill();
			}
			float duration = ContentFadeDuration * ContentGroup.alpha;
			fadeTweener = ContentGroup.DOFade(0f, duration).SetUpdate(true).OnComplete(delegate
			{
				duration = BgFadeDuration * BgGroup.alpha;
				fadeTweener = BgGroup.DOFade(0f, duration).SetUpdate(true).OnComplete(delegate
				{
					fadeTweener = null;
					if (callback != null)
					{
						callback();
					}
				});
			});
		}

		[CompilerGenerated]
		[DebuggerHidden]
		private void _003CSetVisible_003E__BaseCallProxy0(bool visible)
		{
			base.SetVisible(visible);
		}
	}
}
