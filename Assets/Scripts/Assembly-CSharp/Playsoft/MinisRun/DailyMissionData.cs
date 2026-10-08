using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class DailyMissionData
	{
		[SerializeField]
		private bool iC;

		[SerializeField]
		private int cP;

		[SerializeField]
		private int mD;

		private DailyMissionsBalancingData _missionData;

		public bool IsClaimed
		{
			get
			{
				return iC;
			}
			set
			{
				iC = value;
			}
		}

		public int CurrentProgress
		{
			get
			{
				return cP;
			}
			set
			{
				cP = value;
			}
		}

		private int MissionDataId
		{
			get
			{
				return mD;
			}
			set
			{
				mD = value;
			}
		}

		public DailyMissionsBalancingData MissionData
		{
			get
			{
				return _missionData;
			}
			set
			{
				_missionData = value;
				MissionDataId = _missionData.Missionid;
			}
		}

		public int GetMissionDataId()
		{
			return MissionDataId;
		}

		public override bool Equals(object obj)
		{
			DailyMissionData dailyMissionData = obj as DailyMissionData;
			if (dailyMissionData != null)
			{
				return IsClaimed.Equals(dailyMissionData.IsClaimed) && CurrentProgress.Equals(dailyMissionData.CurrentProgress) && MissionDataId.Equals(dailyMissionData.MissionDataId);
			}
			return base.Equals(obj);
		}
	}
}
