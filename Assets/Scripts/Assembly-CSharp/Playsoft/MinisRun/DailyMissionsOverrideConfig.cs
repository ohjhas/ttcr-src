using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class DailyMissionsOverrideConfig
	{
		[SerializeField]
		private int id;

		[SerializeField]
		private string dMG;

		private DailyMissionGroup? dailyMissionGroup;

		[SerializeField]
		private string dMT;

		private DailyMissionType? dailyMissionType;

		[SerializeField]
		private string t;

		[SerializeField]
		private int dMGo;

		[SerializeField]
		private string iT;

		private ItemType? itemtype;

		[SerializeField]
		private int iI;

		[SerializeField]
		private int iA;

		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public DailyMissionGroup DailyMissionGroup
		{
			get
			{
				DailyMissionGroup? dailyMissionGroup = this.dailyMissionGroup;
				return (!dailyMissionGroup.HasValue) ? ((DailyMissionGroup)Enum.Parse(typeof(DailyMissionGroup), dMG)) : dailyMissionGroup.Value;
			}
			set
			{
				dailyMissionGroup = value;
			}
		}

		public DailyMissionType DailyMissionType
		{
			get
			{
				DailyMissionType? dailyMissionType = this.dailyMissionType;
				return (!dailyMissionType.HasValue) ? ((DailyMissionType)Enum.Parse(typeof(DailyMissionType), dMT)) : dailyMissionType.Value;
			}
			set
			{
				dailyMissionType = value;
			}
		}

		public string Title
		{
			get
			{
				return t;
			}
			set
			{
				t = value;
			}
		}

		public int DailyMissionGoal
		{
			get
			{
				return dMGo;
			}
			set
			{
				dMGo = value;
			}
		}

		public ItemType ItemType
		{
			get
			{
				ItemType? itemType = itemtype;
				return (!itemType.HasValue) ? ((ItemType)Enum.Parse(typeof(ItemType), iT)) : itemType.Value;
			}
			set
			{
				itemtype = value;
			}
		}

		public int ItemIndex
		{
			get
			{
				return iI;
			}
			set
			{
				iI = value;
			}
		}

		public int ItemAmount
		{
			get
			{
				return iA;
			}
			set
			{
				iA = value;
			}
		}

		public DailyMissionsBalancingData ConvertToBalancingData()
		{
			DailyMissionsBalancingData dailyMissionsBalancingData = new DailyMissionsBalancingData();
			dailyMissionsBalancingData.Missionid = id;
			dailyMissionsBalancingData.Dailymissiongoal = DailyMissionGoal;
			dailyMissionsBalancingData.DAILYMISSIONGROUP = DailyMissionGroup;
			dailyMissionsBalancingData.DAILYMISSIONTYPE = DailyMissionType;
			dailyMissionsBalancingData.ITEMTYPE = ItemType;
			dailyMissionsBalancingData.Itemamount = ItemAmount;
			dailyMissionsBalancingData.Itemindex = ItemIndex;
			dailyMissionsBalancingData.Title = Title;
			return dailyMissionsBalancingData;
		}
	}
}
