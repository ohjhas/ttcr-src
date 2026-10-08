using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CSUpgradePart : MonoBehaviour
	{
		[SerializeField]
		private Button upgradeButton;

		[SerializeField]
		private Button getMorePartsButton;

		[SerializeField]
		private GameObject maxedLabel;

		[SerializeField]
		private TextMeshProUGUI costText;

		[SerializeField]
		private TextMeshProUGUI upgradeText;

		[SerializeField]
		private Material upgradeTextGreenMaterial;

		[SerializeField]
		private Material upgradeTextGrayMaterial;

		[SerializeField]
		private Material costTextGreenMaterial;

		[SerializeField]
		private Material costTextGrayMaterial;

		public void SetData(Action upgradeCallback, Action getPartsCallback, bool upgradeAvailable, bool enoughParts, int cost)
		{
			upgradeButton.gameObject.SetActive(false);
			getMorePartsButton.gameObject.SetActive(false);
			maxedLabel.SetActive(false);
			if (!upgradeAvailable)
			{
				maxedLabel.SetActive(true);
				return;
			}
			if (!enoughParts)
			{
				getMorePartsButton.gameObject.SetActive(TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.Shop));
				getMorePartsButton.onClick.RemoveAllListeners();
				getMorePartsButton.onClick.AddListener(delegate
				{
					getPartsCallback();
				});
				return;
			}
			upgradeButton.gameObject.SetActive(true);
			upgradeButton.onClick.RemoveAllListeners();
			upgradeButton.onClick.AddListener(delegate
			{
				upgradeCallback();
			});
			costText.text = string.Format("{0} {1}", "<sprite name=InlinedIcons_5>", NumbersHelper.GetNumberSplitedBySpaces(cost));
		}

		public void SetState(bool interactable)
		{
			upgradeButton.interactable = interactable;
			upgradeText.fontMaterial = ((!interactable) ? upgradeTextGrayMaterial : upgradeTextGreenMaterial);
			costText.fontMaterial = ((!interactable) ? costTextGrayMaterial : costTextGreenMaterial);
		}
	}
}
