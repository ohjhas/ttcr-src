using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class RewardEntry : BaseUI
	{
		[SerializeField]
		private Image rewardImage;

		[SerializeField]
		private TextMeshProUGUI rewardAmount;

		[SerializeField]
		private Sprite softCurrencyRewardIcon;

		[SerializeField]
		private Sprite hardCurrencyRewardIcon;

		private const float ChestScale = 1f;

		private const float CurrencyScale = 1.7f;

		public void SetUp(RewardData rewardData)
		{
			rewardImage.sprite = GetRewardImage(rewardData);
			rewardAmount.text = string.Format("{0}{1}", (rewardData.Type != ItemType.Chest) ? string.Empty : "X", NumbersHelper.GetNumberSplitedBySpaces(rewardData.Amount));
			SetImageScale(rewardData);
		}

		private Sprite GetRewardImage(RewardData rewardData)
		{
			switch (rewardData.Type)
			{
			case ItemType.Chest:
			{
				ChestData chestData = Contexts.sharedInstance.state.balancingDataEntity.gatchaBalancingData.Map[rewardData.Index];
				return SettingsService.AssetsSettingsSO.GetSprite(chestData.AssetName);
			}
			case ItemType.Currency:
				return (rewardData.Index != 0) ? hardCurrencyRewardIcon : softCurrencyRewardIcon;
			default:
				return null;
			}
		}

		private void SetImageScale(RewardData rewardData)
		{
			switch (rewardData.Type)
			{
			case ItemType.Chest:
				rewardImage.transform.localScale = 1f * Vector3.one;
				break;
			case ItemType.Currency:
				rewardImage.transform.localScale = 1.7f * Vector3.one;
				break;
			}
		}
	}
}
