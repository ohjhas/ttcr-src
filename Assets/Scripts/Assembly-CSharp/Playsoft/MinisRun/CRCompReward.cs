using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CRCompReward : CRRewardView
	{
		[Header("Extension")]
		[SerializeField]
		private RectTransform regularParent;

		[SerializeField]
		private RectTransform newParent;

		[SerializeField]
		private Image SliderFill;

		[SerializeField]
		private TextMeshProUGUI CompNameLabel;

		[SerializeField]
		private GameObject UpgradeArrow;

		private int neededParts;

		protected override float maxIconScale
		{
			get
			{
				return 1f;
			}
		}

		public void SetData(GeneratedReward reward, CSCompanionData companion, RectTransform chestPos)
		{
			neededParts = companion.GetNextUpgradeCostParts();
			CompNameLabel.text = companion.Name;
			SetCounterValue(0f);
			SetupFieldsAndOverlay(reward, companion.OwnedParts);
			if (reward.IsNew)
			{
				CreateCompanion(companion, reward.IsNew);
			}
			else
			{
				CreateCompCard(companion, reward.IsNew);
			}
			SetupInitialIconPosAndScale(chestPos);
		}

		public void DestroyCompCard()
		{
			if (iconTransform != null)
			{
				Object.Destroy(iconTransform.gameObject);
			}
		}

		private void CreateCompCard(CSCompanionData companion, bool isNew)
		{
			DestroyCompCard();
			SettingsSO settingsSO = SettingsService.SettingsSO;
			RectTransform companionCardUIAsset = Contexts.sharedInstance.uI.GetCompanionCardUIAsset(companion.Id);
			RectTransform rectTransform = Object.Instantiate(companionCardUIAsset);
			rectTransform.SetParent((!isNew) ? regularParent : newParent);
			rectTransform.pivot = new Vector2(0.5f, 0f);
			rectTransform.anchoredPosition3D = Vector3.zero;
			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.GetComponent<CRRewardCard>().SetData(companion.Rarity > Rarity.Epic);
			iconTransform = rectTransform;
		}

		private void CreateCompanion(CSCompanionData companion, bool isNew)
		{
			DestroyCompCard();
			SettingsSO settingsSO = SettingsService.SettingsSO;
			RectTransform companionUIAsset = Contexts.sharedInstance.uI.GetCompanionUIAsset(companion.Id);
			RectTransform rectTransform = Object.Instantiate(companionUIAsset);
			rectTransform.SetParent((!isNew) ? regularParent : newParent);
			rectTransform.pivot = new Vector2(0.5f, 0f);
			rectTransform.anchoredPosition3D = Vector3.zero;
			rectTransform.sizeDelta = Vector2.zero;
			iconTransform = rectTransform;
		}

		protected override void SetCounterValue(float animFactor)
		{
			currentAmount = Mathf.Lerp(initialAmount, targetAmount, animFactor);
			CounterLabel.text = NumbersHelper.GetNumberSplitedBySpaces((long)currentAmount) + "/" + NumbersHelper.GetNumberSplitedBySpaces(neededParts);
			SliderFill.fillAmount = currentAmount / (float)neededParts;
			UpgradeArrow.SetActive(currentAmount >= (float)neededParts);
		}
	}
}
