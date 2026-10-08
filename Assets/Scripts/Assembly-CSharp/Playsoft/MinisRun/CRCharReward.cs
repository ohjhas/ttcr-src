using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CRCharReward : CRRewardView
	{
		[Header("Extension")]
		[SerializeField]
		private RectTransform regularParent;

		[SerializeField]
		private RectTransform newParent;

		[SerializeField]
		private Image SliderFill;

		[SerializeField]
		private TextMeshProUGUI CharNameLabel;

		[SerializeField]
		private GameObject UpgradeArrow;

		private Animator runningCharAnimator;

		private CharacterAnimatorController characterAnimatorController;

		private int neededParts;

		private int charIndex;

		protected override float maxIconScale
		{
			get
			{
				return 1f;
			}
		}

		public void SetData(GeneratedReward reward, CSCharacterData character, RectTransform chestPos)
		{
			neededParts = character.GetNextUpgradeCostParts();
			CharNameLabel.text = character.Name;
			SetCounterValue(0f);
			SetupFieldsAndOverlay(reward, character.OwnedParts);
			if (reward.IsNew)
			{
				CreateCharacter(character, reward.IsNew);
				SetupInitialIconPosAndScale(chestPos);
				SetupAnimation(reward.IsNew);
			}
			else
			{
				CreateCharCard(character, reward.IsNew);
				SetupInitialIconPosAndScale(chestPos);
			}
		}

		public void DestroyCharCard()
		{
			if (iconTransform != null)
			{
				Object.Destroy(iconTransform.gameObject);
			}
		}

		private void CreateCharCard(CSCharacterData character, bool isNew)
		{
			DestroyCharCard();
			SettingsSO settingsSO = SettingsService.SettingsSO;
			charIndex = character.Id;
			RectTransform characterCardUIAsset = Contexts.sharedInstance.uI.GetCharacterCardUIAsset(character.Id);
			RectTransform rectTransform = Object.Instantiate(characterCardUIAsset);
			rectTransform.SetParent((!isNew) ? regularParent : newParent);
			rectTransform.pivot = new Vector2(0.5f, 0f);
			rectTransform.anchoredPosition3D = Vector3.zero;
			rectTransform.sizeDelta = Vector2.zero;
			rectTransform.GetComponent<CRRewardCard>().SetData(character.Rarity >= Rarity.Epic);
			iconTransform = rectTransform;
		}

		private void CreateCharacter(CSCharacterData character, bool isNew)
		{
			DestroyCharCard();
			SettingsSO settingsSO = SettingsService.SettingsSO;
			charIndex = character.Id;
			RectTransform characterUIAsset = Contexts.sharedInstance.uI.GetCharacterUIAsset(character.Id);
			RectTransform rectTransform = Object.Instantiate(characterUIAsset);
			rectTransform.SetParent((!isNew) ? regularParent : newParent);
			rectTransform.pivot = new Vector2(0.5f, 0f);
			rectTransform.anchoredPosition3D = Vector3.zero;
			rectTransform.sizeDelta = Vector2.zero;
			iconTransform = rectTransform;
			runningCharAnimator = rectTransform.GetComponent<Animator>();
			characterAnimatorController = rectTransform.GetComponent<CharacterAnimatorController>();
		}

		protected override void SetCounterValue(float animFactor)
		{
			currentAmount = Mathf.Lerp(initialAmount, targetAmount, animFactor);
			CounterLabel.text = NumbersHelper.GetNumberSplitedBySpaces((long)currentAmount) + "/" + NumbersHelper.GetNumberSplitedBySpaces(neededParts);
			SliderFill.fillAmount = currentAmount / (float)neededParts;
			UpgradeArrow.SetActive(currentAmount >= (float)neededParts);
		}

		private void SetupAnimation(bool isNew)
		{
			if (isNew)
			{
				if (characterAnimatorController != null)
				{
					characterAnimatorController.ForceAnimation(charIndex, CharAnimationType.JumpAndWin);
				}
				else
				{
					runningCharAnimator.Play("UIJumpAndWinAnim");
				}
			}
			else if (characterAnimatorController != null)
			{
				characterAnimatorController.ForceAnimation(charIndex, CharAnimationType.Idle);
			}
		}
	}
}
