using System;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class ExplanationPopup : BasePopupScreen
	{
		[SerializeField]
		private TextMeshProUGUI titleText;

		[SerializeField]
		private TextMeshProUGUI descriptionText;

		[SerializeField]
		private Button goToShopButton;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.ExplanationPopup;
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				Contexts.sharedInstance.uI.CreateEntity().isUIExplanationPopupShown = true;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			if (parameters == null || !parameters.hasUIDisplayExplanation)
			{
				return;
			}
			goToShopButton.gameObject.SetActive(false);
			goToShopButton.onClick.RemoveAllListeners();
			switch (parameters.uIDisplayExplanation.ExplanationType)
			{
			case ExplanationType.Parts:
				titleText.text = ScriptLocalization.explenation_not_enough_parts_title;
				descriptionText.text = ScriptLocalization.explenation_not_enough_parts_description;
				goToShopButton.gameObject.SetActive(true);
				goToShopButton.onClick.AddListener(delegate
				{
					OpenShop(ShopGroupType.Chests);
				});
				break;
			case ExplanationType.Coins:
				titleText.text = ScriptLocalization.explenation_not_enough_coins_title;
				descriptionText.text = string.Format(ScriptLocalization.explenation_not_enough_coins_description, "<sprite name=InlinedIcons_5>");
				goToShopButton.gameObject.SetActive(true);
				goToShopButton.onClick.AddListener(delegate
				{
					OpenShop(ShopGroupType.SoftCurrency);
				});
				break;
			case ExplanationType.Gems:
				titleText.text = ScriptLocalization.explenation_not_enough_gems_title;
				descriptionText.text = string.Format(ScriptLocalization.explenation_not_enough_gems_description, "<sprite name=InlinedIcons_1>");
				goToShopButton.gameObject.SetActive(true);
				goToShopButton.onClick.AddListener(delegate
				{
					OpenShop(ShopGroupType.HardCurrency);
				});
				break;
			case ExplanationType.Chapter:
				titleText.text = ScriptLocalization.explenation_not_enough_stars_title;
				descriptionText.text = string.Format(ScriptLocalization.explenation_not_enough_stars_description, "<sprite name=InlinedIcons_0>");
				break;
			case ExplanationType.IapFail:
				titleText.text = ScriptLocalization.iap_transaction_failed_title;
				descriptionText.text = ScriptLocalization.iap_transaction_failed_desc;
				break;
			case ExplanationType.IapCancel:
				titleText.text = ScriptLocalization.iap_transaction_cancelled_title;
				descriptionText.text = ScriptLocalization.iap_transaction_cancelled_desc;
				break;
			case ExplanationType.NoInternet:
				titleText.text = ScriptLocalization.no_internet_title;
				descriptionText.text = ScriptLocalization.no_internet_desc;
				break;
			case ExplanationType.IapNotAvailable:
				titleText.text = ScriptLocalization.iap_not_available_title;
				descriptionText.text = ScriptLocalization.iap_not_available_desc;
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}

		private void OpenShop(ShopGroupType type)
		{
			base.uiContext.CloseDialog(this);
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIOpenShop(type);
		}
	}
}
