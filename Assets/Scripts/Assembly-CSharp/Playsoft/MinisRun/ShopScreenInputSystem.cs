using System.Collections.Generic;
using Entitas;
using O7SDK.O7ADSDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ShopScreenInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public ShopScreenInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.ShopScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUIChestBought)
				{
					if (entity.isUIFreeChestOpened)
					{
						OpenFreeChest(entity.uIChestBought.ChestId);
					}
					else
					{
						BuyChest(entity.uIChestBought.ChestId);
					}
				}
				else if (entity.hasUISoftCurrencyPackBought)
				{
					BuySoftCurrencyPack(entity.uISoftCurrencyPackBought.PackId, entity.uISoftCurrencyPackBought.origin);
					uiContext.CreateEntity().isRefreshScreen = true;
				}
				else if (entity.hasUIIAPBought)
				{
					BuyHardCurrnecyPack(entity.uIIAPBought.productId, entity.uIIAPBought.origin);
					uiContext.CreateEntity().isRefreshScreen = true;
				}
				entity.isDestroyed = true;
			}
		}

		private void OpenFreeChest(int chestId)
		{
			ChestData chestData = GetChestData(chestId);
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.AddSoftCurrnecyPrice(chestData.PriceSC);
			stateEntity.AddHardCurrencyPrice(chestData.PriceHC);
			stateEntity.AddBoughtChest(chestData, ChestType.Video);
			stateEntity.AddTransactionImage(chestData.AssetName);
			stateEntity.AddTransactionName(chestData.Name, chestData.ContentDescription);
			O7ADRewardAdManager manager = stateContext.o7SdkEntity.o7SdkRewardedAdManager.Manager;
			if (stateContext.o7SdkEntity != null && stateContext.o7SdkEntity.isO7SdkRewardAdFetched && manager.IsVideoLoaded())
			{
				stateEntity.isO7SdkRewardAdsShow = true;
				stateEntity.AddO7SdkRewardOfferType(O7SdkRewardOfferType.Chest_Reward);
			}
			else
			{
				stateEntity.isTransactionBegin = true;
				Contexts.sharedInstance.state.CreateEntity().isOpenFreeChest = true;
			}
		}

		private void BuyChest(int chestId)
		{
			ChestData chestData = GetChestData(chestId);
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.AddSoftCurrnecyPrice(chestData.PriceSC);
			stateEntity.AddHardCurrencyPrice(chestData.PriceHC);
			stateEntity.AddBoughtChest(chestData, ChestType.Shop);
			stateEntity.AddTransactionImage(chestData.AssetName);
			stateEntity.AddTransactionName(chestData.Name, chestData.ContentDescription);
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIDisplayTransactionConfrimation(stateEntity);
		}

		private ChestData GetChestData(int chestId)
		{
			return stateContext.balancingDataEntity.gatchaBalancingData.Map[chestId];
		}

		private void BuySoftCurrencyPack(int packId, RectTransform origin)
		{
			ShopSoftCurrencyPacksBalancingData shopSoftCurrencyPacksBalancingData = stateContext.balancingDataEntity.shopSoftCurrencyPacksData.Map[packId];
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.AddHardCurrencyPrice(shopSoftCurrencyPacksBalancingData.Price);
			stateEntity.AddBoughtSoftCurrency(shopSoftCurrencyPacksBalancingData.Amount);
			stateEntity.AddTransactionImage(shopSoftCurrencyPacksBalancingData.Image);
			stateEntity.AddTransactionName(shopSoftCurrencyPacksBalancingData.Name, string.Empty);
			if (origin != null)
			{
				stateEntity.AddTransactionOrigin(origin);
			}
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIDisplayTransactionConfrimation(stateEntity);
		}

		private void BuyHardCurrnecyPack(string packId, RectTransform origin)
		{
			LogIapRequest(packId);
			if (stateContext.o7SdkEntity == null)
			{
				UIEntity uIEntity = uiContext.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUIDisplayExplanation(ExplanationType.IapNotAvailable);
			}
			else
			{
				Debug.Log("BuyHardCurrnecyPack()" + packId);
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.AddO7SdkIapBuyRequest(packId, origin);
			}
		}

		private void LogIapRequest(string productID)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.AddLogO7SdkIapBuyRequest(productID);
		}
	}
}
