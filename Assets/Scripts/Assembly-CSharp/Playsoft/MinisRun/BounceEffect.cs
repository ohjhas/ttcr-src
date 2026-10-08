using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BounceEffect : MonoBehaviour
	{
		public void Show(Vector3 punch, float duration, AnimationCurve animationCurve = null)
		{
			if (animationCurve != null)
			{
				base.transform.DOScale(punch, duration).SetEase(animationCurve);
			}
			else
			{
				base.transform.DOPunchScale(punch, duration);
			}
		}
	}
}
