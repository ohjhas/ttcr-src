using System;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class ShopChestEntry : ShopEntry, IFreeChestStateListener
	{
		private enum ChestEntryType
		{
			Soft = 0,
			Hard = 1,
			Free = 2
		}

		[SerializeField]
		private Button freeBuyButton;

		[SerializeField]
		private CountDownTimerView freeTimerView;

		private ChestEntryType chestType;

		private Action<int> pressCallback;

		public int Id { get; private set; }

		public void SetData(int index, ChestData data)
		{
			if (data.PriceSC != 0)
			{
				chestType = ChestEntryType.Soft;
			}
			else if (data.PriceHC != 0)
			{
				chestType = ChestEntryType.Hard;
			}
			else if (data.WaitTime != 0)
			{
				chestType = ChestEntryType.Free;
			}
			Id = index;
			buyButton.gameObject.SetActive(false);
			freeBuyButton.gameObject.SetActive(false);
			freeTimerView.gameObject.SetActive(false);
			buyButton.onClick.RemoveAllListeners();
			freeBuyButton.onClick.RemoveAllListeners();
			titleText.text = data.Name;
			freeBuyButton.onClick.AddListener(BuyClicked);
			contentImage.sprite = SettingsService.AssetsSettingsSO.GetSprite(data.AssetName);
			contentImage.SetNativeSize();
			switch (chestType)
			{
			case ChestEntryType.Soft:
				buyButton.gameObject.SetActive(true);
				costText.text = string.Format("{1}{0}", NumbersHelper.GetNumberSplitedBySpaces(data.PriceSC), "<sprite name=InlinedIcons_5>");
				break;
			case ChestEntryType.Hard:
				buyButton.gameObject.SetActive(true);
				costText.text = string.Format("{1}{0}", NumbersHelper.GetNumberSplitedBySpaces(data.PriceHC), "<sprite name=InlinedIcons_1>");
				break;
			}
		}

		public void SetState(FreeChestData freeChestData, Action<int> onEntryPressed)
		{
			pressCallback = onEntryPressed;
			buyButton.onClick.RemoveAllListeners();
			buyButton.interactable = true;
			buyButton.onClick.AddListener(BuyClicked);
			if (chestType == ChestEntryType.Free)
			{
				FreeChestStateChanged(freeChestData);
			}
		}

		public void FreeChestStateChanged(FreeChestData data)
		{
			if (chestType != ChestEntryType.Free)
			{
				return;
			}
			if (data.IsLocked)
			{
				if (freeBuyButton != null)
				{
					freeBuyButton.gameObject.SetActive(false);
				}
				if (freeTimerView != null)
				{
					freeTimerView.gameObject.SetActive(true);
				}
				if (freeTimerView != null)
				{
					freeTimerView.Setup(data.UnlockTime);
				}
			}
			else
			{
				if (freeBuyButton != null)
				{
					freeBuyButton.gameObject.SetActive(true);
				}
				if (freeTimerView != null)
				{
					freeTimerView.gameObject.SetActive(false);
				}
			}
		}

		private void Start()
		{
			Contexts.sharedInstance.uI.CreateEntity().AddFreeChestStateListener(this);
		}

		private void BuyClicked()
		{
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.isShopScreen = true;
			uIEntity.AddUIChestBought(Id);
			if (chestType == ChestEntryType.Free)
			{
				uIEntity.isUIFreeChestOpened = true;
			}
			if (pressCallback != null)
			{
				pressCallback(Id);
			}
		}
	}
}
