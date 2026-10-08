using System;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSChestIcon
	{
		[SerializeField]
		private GameObject Container;

		[SerializeField]
		private Image ChestIcon;

		private bool chestAvailable;

		public void Setup(bool _chestAvailable, ChestData? chestData)
		{
			chestAvailable = false;
			if (_chestAvailable && chestData.HasValue)
			{
				chestAvailable = true;
				ChestIcon.sprite = SettingsService.AssetsSettingsSO.GetSprite(chestData.Value.AssetName);
			}
			Container.SetActive(chestAvailable);
		}

		public void SetState(bool isLevelUnlocked)
		{
			Container.SetActive(chestAvailable && isLevelUnlocked);
		}
	}
}
