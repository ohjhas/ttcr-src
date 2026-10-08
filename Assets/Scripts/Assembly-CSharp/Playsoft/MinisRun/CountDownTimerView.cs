using System;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CountDownTimerView : MonoBehaviour, ICurrentTimeListener
	{
		[SerializeField]
		private TextMeshProUGUI timerText;

		[SerializeField]
		private TextMeshProUGUI timerTextShadow;

		[SerializeField]
		private bool shouldShowHours = true;

		private bool setup;

		private DateTime lastTime;

		private DateTime endTime;

		private Action callback;

		private void Start()
		{
			if (null == timerText)
			{
				timerText = GetComponent<TextMeshProUGUI>();
			}
			Contexts.sharedInstance.state.CreateEntity().AddCurrentTimeListener(this);
		}

		public void Setup(int duration)
		{
			setup = true;
			endTime = Contexts.sharedInstance.state.currentRealTime.CurrentTime.AddSeconds(duration);
			CurrentTimeChanged(lastTime);
		}

		public void Setup(DateTime endDateTime, Action _callback = null)
		{
			setup = true;
			endTime = endDateTime;
			callback = _callback;
			CurrentTimeChanged(lastTime);
		}

		public void CurrentTimeChanged(DateTime currentTime)
		{
			lastTime = currentTime;
			if (setup)
			{
				bool flag = false;
				TimeSpan timeSpan;
				if (endTime >= currentTime)
				{
					timeSpan = endTime.Subtract(currentTime);
				}
				else
				{
					flag = true;
					timeSpan = TimeSpan.Zero;
				}
				string text = ((!shouldShowHours) ? string.Format("{0:D2}:{1:D2}", timeSpan.Hours * 60 + timeSpan.Minutes, timeSpan.Seconds) : string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds));
				if (timerText != null)
				{
					timerText.text = text;
				}
				if (timerTextShadow != null)
				{
					timerTextShadow.text = text;
				}
				if (flag && callback != null)
				{
					callback();
					callback = null;
				}
			}
		}
	}
}
