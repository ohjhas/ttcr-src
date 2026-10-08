using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class MexicanWaveBarBehaviour : MonoBehaviour
	{
		private Dictionary<Transform, Tweener> _tweensDict;

		protected void Awake()
		{
			_tweensDict = new Dictionary<Transform, Tweener>();
		}

		protected void PunchBarsIfPossible(List<GameObject> bars, float animationDelay, float singlePunchDuration, float inBetweenPunchesOffset, float firstPunchStrength, float lastBarStrength)
		{
			for (int i = 0; i < bars.Count; i++)
			{
				float punchStrength = Mathf.Lerp(firstPunchStrength, lastBarStrength, (float)i / (float)(bars.Count - 1));
				PunchSingleBarIfNeeded(bars[i].transform, punchStrength, singlePunchDuration, animationDelay + inBetweenPunchesOffset * (float)i);
			}
		}

		private void PunchSingleBarIfNeeded(Transform bar, float punchStrength, float punchDuration, float punchDelay)
		{
			if (!_tweensDict.ContainsKey(bar))
			{
				Tweener tweener = bar.DOPunchScale(Vector3.one * punchStrength, punchDuration, 1).SetDelay(punchDelay);
				tweener.OnComplete(delegate
				{
					if (_tweensDict.ContainsKey(bar))
					{
						_tweensDict.Remove(bar);
					}
				});
				_tweensDict.Add(bar, tweener);
			}
			else
			{
				_tweensDict[bar].Complete();
				_tweensDict.Remove(bar);
				PunchSingleBarIfNeeded(bar, punchStrength, punchDuration, punchDelay);
			}
		}
	}
}
