using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WinScreenChest
	{
		private const string ANIMATION_SHOW_CHEST_DROP_TRIGGER = "ShowChestDrop";

		[SerializeField]
		private Image chestImage;

		private int chestIndex;

		private Animator screenAnimator;

		public void Init(WinScreen screen, Animator animator, int chestId, bool isChestWon)
		{
			chestIndex = chestId;
			screenAnimator = animator;
			SetChest(chestIndex);
			if (isChestWon)
			{
				SettingsResultScreenSO setting = SettingsService.GetSetting<SettingsResultScreenSO>();
				screen.StartCoroutine(WinChest(0.7f + setting.CounterAnimTime + 0.5f));
			}
		}

		public void ShowChestOpen()
		{
			SetChest(chestIndex, true);
		}

		private void SetChest(int chestId, bool open = false)
		{
			if (chestId != -1)
			{
				Dictionary<int, ChestData> map = Contexts.sharedInstance.state.balancingDataEntity.gatchaBalancingData.Map;
				ChestData chestData = map[chestId];
				chestImage.sprite = SettingsService.AssetsSettingsSO.GetSprite(chestData.AssetName + ((!open) ? string.Empty : "_open"));
				chestImage.SetNativeSize();
			}
		}

		private IEnumerator WinChest(float delay = 0f)
		{
			yield return new WaitForSecondsRealtime(delay);
			SettingsResultScreenSO so = SettingsService.GetSetting<SettingsResultScreenSO>();
			yield return new WaitForSecondsRealtime(so.MissionProgressBarOnThreeStarsAnimationScaleDownDuration);
			screenAnimator.SetTrigger("ShowChestDrop");
			yield return new WaitForSecondsRealtime(1.5f);
			UIEntity action = Contexts.sharedInstance.uI.CreateEntity();
			action.isWinChestOnResultScreen = true;
			action.AddUIChestWon(chestIndex);
			Contexts.sharedInstance.game.playerEntity.isMissionChestGranted = true;
			yield return new WaitForSecondsRealtime(0.5f);
			SetChest(chestIndex, true);
		}
	}
}
