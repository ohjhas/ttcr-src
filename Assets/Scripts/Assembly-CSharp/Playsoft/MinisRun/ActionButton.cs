using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ActionButton : MonoBehaviour
	{
		[SerializeField]
		protected CanvasGroup canvasGroup;

		private bool isShowed;

		public virtual void Show(float delay = 0f)
		{
			if (!isShowed)
			{
				isShowed = true;
				canvasGroup.alpha = 1f;
				base.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack).From()
					.SetDelay(delay);
			}
		}

		public virtual void Hide()
		{
			isShowed = false;
			canvasGroup.alpha = 0f;
		}
	}
}
