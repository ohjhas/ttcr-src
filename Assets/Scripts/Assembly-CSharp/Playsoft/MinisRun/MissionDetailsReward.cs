using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class MissionDetailsReward
	{
		[SerializeField]
		private GameObject container;

		[SerializeField]
		private Image chestIcon;

		public void SetUp(WSLevelData levelData, List<ChestData> chestsData)
		{
			ChestData? chestData = null;
			if (chestsData.Exists((ChestData ch) => ch.Id == levelData.ChestRewardId))
			{
				chestData = chestsData.Find((ChestData ch) => ch.Id == levelData.ChestRewardId);
			}
			if (chestData.HasValue && !levelData.ChestCollected)
			{
				container.SetActive(true);
				chestIcon.sprite = SettingsService.AssetsSettingsSO.GetSprite(chestData.Value.AssetName);
				chestIcon.SetNativeSize();
			}
			else
			{
				container.SetActive(false);
			}
		}

		public bool IsActive()
		{
			return container.activeSelf;
		}
	}
}
