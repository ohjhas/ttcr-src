using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CRCurrencyReward : CRRewardView
	{
		[Header("Extension")]
		[SerializeField]
		private Image CurrencyIcon;

		[SerializeField]
		private Image CurrencyCounterIcon;

		private float iconScale;

		protected override float maxIconScale
		{
			get
			{
				return iconScale;
			}
		}

		public void SetData(GeneratedReward reward, int ownedAmount, RectTransform chestPos)
		{
			SetCounterValue(0f);
			SetupFieldsAndOverlay(reward, ownedAmount);
			SetupCurrencyIcon(reward, chestPos);
			SetupInitialIconPosAndScale(chestPos);
		}

		private void SetupCurrencyIcon(GeneratedReward reward, RectTransform chestPos)
		{
			string text = ((reward.Index != ProjectConstants.HARD_CURRENCY_ID) ? "SCIcon" : "HCIcon");
			iconScale = ((reward.Index != ProjectConstants.HARD_CURRENCY_ID) ? 1f : 0.9f);
			CurrencyCounterIcon.transform.localScale = Vector3.one * iconScale;
			Image currencyIcon = CurrencyIcon;
			Sprite sprite = SettingsService.AssetsSettingsSO.GetSprite(text);
			CurrencyCounterIcon.sprite = sprite;
			currencyIcon.sprite = sprite;
			iconTransform = CurrencyIcon.transform;
		}

		protected override void SetCounterValue(float animFactor)
		{
			currentAmount = Mathf.Lerp(initialAmount, targetAmount, animFactor);
			CounterLabel.text = NumbersHelper.GetNumberSplitedBySpaces((long)currentAmount);
		}
	}
}
