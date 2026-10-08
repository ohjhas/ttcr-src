using System;

namespace Playsoft.MinisRun
{
	public static class DailyMissionsHelper
	{
		public static DateTime GetLastResetTime()
		{
			DateTime currentTime = Contexts.sharedInstance.state.currentRealTimeEntity.currentRealTime.CurrentTime;
			int num = Contexts.sharedInstance.state.balancingDataEntity.initConfigData.Map[EnumSettings.ResetDailyMissionsHour_24H];
			currentTime = ((currentTime.Hour < num) ? currentTime.AddDays(-1.0) : currentTime);
			return new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, num, 0, 0);
		}
	}
}
