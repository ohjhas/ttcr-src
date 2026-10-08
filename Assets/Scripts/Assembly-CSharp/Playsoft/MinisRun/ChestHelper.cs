using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public static class ChestHelper
	{
		public static int GetChestIdIfAvailable(bool hasPlayerThreeStars, bool isForceChestReward)
		{
			Dictionary<int, MissionChestsBalancingData> map = Contexts.sharedInstance.state.balancingDataEntity.missionChestsBalancingData.Map;
			int worldIndex = SettingsService.SettingsSO.WorldIndex;
			if (!Contexts.sharedInstance.state.GetEntitiesWithName("ChestData" + SettingsService.SettingsSO.WorldIndex).SingleEntity().boolean.Value && map.ContainsKey(worldIndex) && (hasPlayerThreeStars || isForceChestReward))
			{
				return map[worldIndex].Chestid;
			}
			return -1;
		}

		public static Sprite GetChestSprite()
		{
			Dictionary<int, MissionChestsBalancingData> map = Contexts.sharedInstance.state.balancingDataEntity.missionChestsBalancingData.Map;
			int worldIndex = SettingsService.SettingsSO.WorldIndex;
			if (!map.ContainsKey(worldIndex))
			{
				return null;
			}
			int chestid = map[worldIndex].Chestid;
			Dictionary<int, ChestData> map2 = Contexts.sharedInstance.state.balancingDataEntity.gatchaBalancingData.Map;
			ChestData chestData = map2[chestid];
			return SettingsService.AssetsSettingsSO.GetSprite(chestData.AssetName);
		}
	}
}
