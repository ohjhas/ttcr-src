using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CSPartsCounter : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _counter;

		[SerializeField]
		private float _countingTime;

		[SerializeField]
		private float _punchStrength;

		private bool _isAnimated;

		private float _oldValue;

		private float _newValue;

		private float _currentValue;

		private int _neededParts;

		private int _prevValueInt;

		private float _timer;

		private int _punchThreshold;

		private float _punchDuration = 0.1f;

		private Tweener _punchCounterTween;

		private void Update()
		{
			if (!_isAnimated)
			{
				return;
			}
			_timer += Time.deltaTime;
			float t = _timer / _countingTime;
			_currentValue = Mathf.Lerp(_oldValue, _newValue, t);
			int num = Mathf.RoundToInt(_currentValue);
			_counter.text = "<color=\"white\">" + NumbersHelper.GetNumberSplitedBySpaces(num) + "</color>/" + NumbersHelper.GetNumberSplitedBySpaces(_neededParts);
			if (_prevValueInt - num >= _punchThreshold)
			{
				if (_punchCounterTween != null)
				{
					_punchCounterTween.Complete();
				}
				_punchCounterTween = base.transform.DOPunchScale(Vector3.one * _punchStrength, _punchDuration, 1);
				_prevValueInt = num;
			}
			if (num == Mathf.RoundToInt(_newValue))
			{
				_isAnimated = false;
			}
		}

		public void SetData(int ownedParts, int neededParts, bool enoughPartsToUpgrade)
		{
			_counter.text = "<color=\"white\">" + NumbersHelper.GetNumberSplitedBySpaces(ownedParts) + "</color>/" + NumbersHelper.GetNumberSplitedBySpaces(neededParts);
			_isAnimated = false;
		}

		public void Animate(int oldOwnedPartsValue, int newOwnedPartsValue, int newNeededParts)
		{
			_isAnimated = true;
			_timer = 0f;
			_oldValue = (_currentValue = (_prevValueInt = oldOwnedPartsValue));
			_newValue = newOwnedPartsValue;
			_neededParts = newNeededParts;
			_punchThreshold = Mathf.Max(1, Mathf.RoundToInt((_oldValue - _newValue) / (_countingTime / _punchDuration)));
		}
	}
}
