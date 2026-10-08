using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class DailyMissionsCollection
	{
		public const int NUMBER_OF_MISSIONS = 3;

		public DateTime CreationTime;

		public bool IsChestClaimed;

		public DailyMissionData[] DailyMissions;
	}
}
