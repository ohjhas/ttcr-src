using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ShopSoftCurrencyEntry : ShopEntry
	{
		private int id;

		public void SetData(int index, SoftCurrencyPacksData data)
		{
			id = index;
			titleText.text = string.Format("{1}{0}", NumbersHelper.GetNumberSplitedBySpaces(data.Amount), "<sprite name=InlinedIcons_5>");
			contentImage.sprite = SettingsService.AssetsSettingsSO.GetSprite(data.Image);
			costText.text = string.Format("{1}{0}", NumbersHelper.GetNumberSplitedBySpaces(data.Price), "<sprite name=InlinedIcons_1>");
		}

		public void SetState()
		{
			buyButton.interactable = true;
			buyButton.onClick.RemoveAllListeners();
			buyButton.onClick.AddListener(BuyClicked);
		}

		private void BuyClicked()
		{
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isShopScreen = true;
			uIEntity.AddUISoftCurrencyPackBought(id, buyButton.GetComponent<RectTransform>());
		}
	}
}
