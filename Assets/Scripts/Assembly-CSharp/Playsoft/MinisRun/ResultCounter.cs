using System;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ResultCounter : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI CounterValue;

		private int _counterValue;

		private int _targetValue;

		private float _counterTimer;

		private float _animDuration;

		private AnimationCurve _animCurve;

		private bool _isFinished = true;

		private Action _callback;

		public void Show(int targetValue, AnimationCurve animCurve, float animDuration, Action callback = null)
		{
			base.gameObject.SetActive(true);
			_isFinished = false;
			_counterValue = 0;
			_counterTimer = 0f;
			CounterValue.text = NumbersHelper.GetNumberSplitedBySpaces(_counterValue);
			_targetValue = targetValue;
			_animCurve = animCurve;
			_animDuration = animDuration;
			_callback = callback;
		}

		private void Update()
		{
			if (_isFinished)
			{
				return;
			}
			_counterTimer += Time.unscaledDeltaTime;
			_counterValue = Mathf.RoundToInt((float)_targetValue * _animCurve.Evaluate(_counterTimer / _animDuration));
			if (_counterValue >= _targetValue)
			{
				_counterValue = _targetValue;
				_isFinished = true;
				if (_callback != null)
				{
					_callback();
				}
			}
			CounterValue.text = NumbersHelper.GetNumberSplitedBySpaces(_counterValue);
		}

		public void Hide()
		{
			base.gameObject.SetActive(false);
		}
	}
}
