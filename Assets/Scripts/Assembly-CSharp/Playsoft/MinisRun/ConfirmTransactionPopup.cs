using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class ConfirmTransactionPopup : BasePopupScreen
	{
		[SerializeField]
		private Image transactionImage;

		[SerializeField]
		private TextMeshProUGUI descriptionText;

		[SerializeField]
		private TextMeshProUGUI questionText;

		[SerializeField]
		private Button regularBuyButton;

		[SerializeField]
		private TextMeshProUGUI priceText;

		private StateEntity transactionEntity;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.ConfirmTransactionPopup;
			}
		}

		public override void SetData(UIEntity parameters)
		{
			if (parameters == null || !parameters.hasUIDisplayTransactionConfrimation)
			{
				return;
			}
			transactionEntity = parameters.uIDisplayTransactionConfrimation.transaction;
			if (transactionEntity.hasTransactionImage)
			{
				transactionImage.sprite = SettingsService.AssetsSettingsSO.GetSprite(transactionEntity.transactionImage.ImageName);
			}
			else
			{
				transactionImage.gameObject.SetActive(false);
			}
			if (transactionEntity.hasTransactionName)
			{
				descriptionText.text = string.Empty;
				if (!string.IsNullOrEmpty(transactionEntity.transactionName.ContentDescription))
				{
					descriptionText.text = transactionEntity.transactionName.ContentDescription;
				}
				questionText.text = string.Format(ScriptLocalization.confirmation_are_sure, transactionEntity.transactionName.Name);
			}
			regularBuyButton.gameObject.SetActive(false);
			if (transactionEntity.hasHardCurrencyPrice && transactionEntity.hardCurrencyPrice.Amount > 0)
			{
				regularBuyButton.gameObject.SetActive(true);
				priceText.text = string.Format("{1}{0}", NumbersHelper.GetNumberSplitedBySpaces(transactionEntity.hardCurrencyPrice.Amount), "<sprite name=InlinedIcons_1>");
			}
			if (transactionEntity.hasSoftCurrnecyPrice && transactionEntity.softCurrnecyPrice.Amount > 0)
			{
				regularBuyButton.gameObject.SetActive(true);
				priceText.text = string.Format("{1}{0}", NumbersHelper.GetNumberSplitedBySpaces(transactionEntity.softCurrnecyPrice.Amount), "<sprite name=InlinedIcons_5>");
			}
			regularBuyButton.onClick.RemoveAllListeners();
			regularBuyButton.onClick.AddListener(BuyButtonClicked);
		}

		private void BuyButtonClicked()
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.isUIHideTransactionConfirmation = true;
			transactionEntity.isTransactionBegin = true;
		}
	}
}
