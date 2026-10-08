using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ShopIAPEntry : ShopEntry
	{
		private int id;

		private string productId;

		private Action<int> pressCallback;

		public void SetData(int index, IAPsData data)
		{
			id = index;
			titleText.text = string.Format("{1}{0}", NumbersHelper.GetNumberSplitedBySpaces(data.Amount), "<sprite name=InlinedIcons_1>");
			productId = data.ProductId;
			buyButton.onClick.RemoveAllListeners();
			buyButton.onClick.AddListener(BuyClicked);
			contentImage.sprite = SettingsService.AssetsSettingsSO.GetSprite(data.Image);
			costText.text = data.Price.ToString();
		}

		public void SetState(Action<int> pressCallback)
		{
			this.pressCallback = pressCallback;
			buyButton.interactable = true;
		}

		private void BuyClicked()
		{
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isShopScreen = true;
			uIEntity.AddUIIAPBought(id, buyButton.GetComponent<RectTransform>(), productId);
			if (pressCallback != null)
			{
				pressCallback(id);
			}
		}

		private void BuyNotPossibleClicked()
		{
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIDisplayExplanation(ExplanationType.Gems);
		}
	}
}
