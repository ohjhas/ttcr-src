using System;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CSUpgradeButton : TwoStatesButton
	{
		[SerializeField]
		private TextMeshProUGUI _costText;

		[SerializeField]
		private TextMeshProUGUI _maxedLabel;

		private bool upgradeAvailable;

		public event Action OnUpgradePossibleClicked;

		public event Action OnUpgradeNotPossibleClicked;

		public void RefreshUpgradeButton(bool isNextUpgradeAvailable, int nextUpgradeCostSC, int ownedCoins)
		{
			if (isNextUpgradeAvailable)
			{
				_maxedLabel.gameObject.SetActive(false);
				Button.gameObject.SetActive(true);
				Button.onClick.RemoveAllListeners();
				if (ownedCoins >= nextUpgradeCostSC)
				{
					Button.onClick.AddListener(UpgradePossibleClicked);
				}
				else
				{
					Button.onClick.AddListener(UpgradeNotPossibleClicked);
				}
				_costText.text = NumbersHelper.GetNumberSplitedBySpaces(nextUpgradeCostSC);
			}
			else
			{
				Button.gameObject.SetActive(false);
				_maxedLabel.gameObject.SetActive(true);
			}
		}

		public void Disable()
		{
			SetInteractable(false);
		}

		private void UpgradePossibleClicked()
		{
			if (this.OnUpgradePossibleClicked != null)
			{
				this.OnUpgradePossibleClicked();
			}
		}

		private void UpgradeNotPossibleClicked()
		{
			if (this.OnUpgradeNotPossibleClicked != null)
			{
				this.OnUpgradeNotPossibleClicked();
			}
		}
	}
}
