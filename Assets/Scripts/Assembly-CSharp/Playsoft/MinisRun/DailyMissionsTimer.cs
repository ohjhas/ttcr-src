using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class DailyMissionsTimer : MonoBehaviour
	{
		[SerializeField]
		private CountDownTimerView Counter;

		public void SetData()
		{
			SetTimer();
		}

		private void SetTimer()
		{
			DateTime endDateTime = DailyMissionsHelper.GetLastResetTime().AddHours(24.0);
			Counter.Setup(endDateTime);
		}
	}
}
