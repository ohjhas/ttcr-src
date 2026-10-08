using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class DailyMissionReward : MonoBehaviour
	{
		[Serializable]
		public class RewardLogicPrefabData
		{
			public ItemType rewardItemType;

			public int rewardItemId;

			public DailyMissionRewardLogic Prefab;
		}

		[SerializeField]
		private List<RewardLogicPrefabData> rewardLogicPrefabs;

		private DailyMissionData _data;

		private DailyMissionRewardLogic _logic;

		public void Init(DailyMissionData data)
		{
			_data = data;
			if (_data != null)
			{
				if (_logic == null)
				{
					SetUpLogic(data);
				}
				_logic.Init(_data);
			}
		}

		public void GetReward(DailyMissionData data)
		{
			if (data != null)
			{
				_logic.GetReward(data);
			}
		}

		private void SetUpLogic(DailyMissionData data)
		{
			for (int i = 0; i < rewardLogicPrefabs.Count; i++)
			{
				if (rewardLogicPrefabs[i].rewardItemType == data.MissionData.ITEMTYPE && rewardLogicPrefabs[i].rewardItemId == data.MissionData.Itemindex)
				{
					_logic = UnityEngine.Object.Instantiate(rewardLogicPrefabs[i].Prefab, base.transform);
					break;
				}
			}
		}
	}
}
