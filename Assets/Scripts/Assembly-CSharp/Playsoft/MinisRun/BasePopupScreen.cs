using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BasePopupScreen : BaseDialog
	{
		[Header("Base Popup Screen")]
		[SerializeField]
		protected CanvasGroup BgGroup;

		[SerializeField]
		protected CanvasGroup ContentGroup;

		[SerializeField]
		protected float BgFadeDuration = 0.25f;

		[SerializeField]
		protected float ContentScaleDuration = 0.3f;

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
				ContentGroup.transform.localScale = Vector3.one;
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
					ContentGroup.alpha = 0f;
					ContentGroup.transform.localScale = Vector3.one;
					ContentGroup.interactable = true;
					OpenAnim();
				}
				return;
			}
			if (BgGroup != null && ContentGroup != null)
			{
				BgGroup.interactable = false;
				ContentGroup.interactable = false;
				CloseAnim(delegate
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

		private void OpenAnim()
		{
			if (fadeTweener != null)
			{
				fadeTweener.Kill();
			}
			float duration = BgFadeDuration * (1f - BgGroup.alpha);
			fadeTweener = BgGroup.DOFade(1f, duration).SetUpdate(true);
			ContentGroup.alpha = 0f;
			ContentGroup.DOFade(1f, ContentScaleDuration - 0.1f);
			ContentGroup.transform.localScale = new Vector3(0.5f, 0.5f, 0.7f);
			ContentGroup.transform.DOScale(1f, ContentScaleDuration).SetUpdate(true).SetEase(Ease.OutBack);
		}

		private void CloseAnim(Action callback)
		{
			if (fadeTweener != null)
			{
				fadeTweener.Kill();
			}
			float duration = BgFadeDuration * BgGroup.alpha;
			fadeTweener = BgGroup.DOFade(0f, duration).SetUpdate(true).OnComplete(delegate
			{
				fadeTweener = null;
				if (callback != null)
				{
					callback();
				}
			});
			ContentGroup.transform.DOScale(0.5f, ContentScaleDuration / 2f).SetUpdate(true);
			ContentGroup.DOFade(0f, ContentScaleDuration / 2f).SetUpdate(true);
		}

		[CompilerGenerated]
		[DebuggerHidden]
		private void _003CSetVisible_003E__BaseCallProxy0(bool visible)
		{
			base.SetVisible(visible);
		}
	}
}
