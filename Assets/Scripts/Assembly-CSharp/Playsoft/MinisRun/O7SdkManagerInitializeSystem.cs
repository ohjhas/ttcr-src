using System.Collections.Generic;
using Entitas;
using O7SDK;
using O7SDK.O7ADSDK;

namespace Playsoft.MinisRun
{
	public class O7SdkManagerInitializeSystem : IInitializeSystem, ITearDownSystem, ISystem
	{
		private readonly StateContext stateContext;

		public O7SdkManagerInitializeSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (stateContext.o7SdkEntity == null)
			{
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isO7Sdk = true;
				stateEntity.AddO7SdkData(false, null);
				stateEntity.AddO7SdkManager(new O7SDKManager());
				stateEntity.AddO7SdkBannerAdManager(new O7ADBannerAdManager());
				stateEntity.AddO7SdkInterstitialAdManager(new O7ADInterstitialAdManager());
				stateEntity.AddO7SdkRewardedAdManager(new O7ADRewardAdManager());
				stateEntity.AddO7SdkCrossPromoManager(new O7SDKCrossPromoManager());
				stateEntity.AddO7SdkVideoGalleryManager(new O7ADVideoGalleryAdManager());
				stateEntity.AddO7SdkEventManager(new O7SDKEventManager());
				stateEntity.AddO7SdkDataManager(new O7SDKDataManager());
				stateEntity.AddO7SdkPurchaseManager(new O7SDKPurchaseManager());
				stateEntity.AddO7SdkIapPacks(new List<IapPacks>());
				stateEntity.AddO7SdkIapItemPriceData(new Dictionary<string, string>());
				stateEntity.AddO7SdkUtilityManager(new O7SDKUtilityManager());
				stateEntity.AddO7SdkAgeGateManager(new O7SDKGateManager());
				stateEntity.AddO7SdkUserStateManager(new O7SDKRestoreManager());
				stateEntity.AddO7SdkGdprManager(new O7SDKGdprManager());
				CheckGridState(stateContext.o7SdkEntity);
			}
		}

		public void TearDown()
		{
			stateContext.o7SdkEntity.o7SdkManager.Manager.OnGridIsDownloaded -= OnGridIsDownloaded;
			stateContext.o7SdkEntity.o7SdkManager.Manager.OnReadyToLoadGame -= OnReadyToLoadGame;
		}

		private void CheckGridState(StateEntity entity)
		{
			entity.isO7SdkGridDownloaded = entity.o7SdkManager.Manager.IsGridDownloaded;
			entity.isO7SdkReadyToLoadGame = entity.o7SdkManager.Manager.IsReadyToLoadGame;
			entity.o7SdkManager.Manager.OnGridIsDownloaded += OnGridIsDownloaded;
			entity.o7SdkManager.Manager.OnReadyToLoadGame += OnReadyToLoadGame;
			if (entity.isO7SdkReadyToLoadGame)
			{
				entity.ReplaceUDIDHash(entity.o7SdkDataManager.Manager.GetUDIDHash());
			}
			if (entity.isO7SdkGridDownloaded)
			{
				string privacyURL = entity.o7SdkDataManager.Manager.GetPrivacyURL(false);
				entity.AddO7SdkPrivacyURL(privacyURL);
			}
		}

		private void OnGridIsDownloaded(bool isGridDownloaded)
		{
			stateContext.o7SdkEntity.isO7SdkGridDownloaded = isGridDownloaded;
			if (isGridDownloaded)
			{
				string privacyURL = stateContext.o7SdkEntity.o7SdkDataManager.Manager.GetPrivacyURL(false);
				stateContext.o7SdkEntity.AddO7SdkPrivacyURL(privacyURL);
			}
		}

		private void OnReadyToLoadGame()
		{
			stateContext.o7SdkEntity.isO7SdkReadyToLoadGame = stateContext.o7SdkEntity.o7SdkManager.Manager.IsReadyToLoadGame;
			stateContext.o7SdkEntity.ReplaceUDIDHash(stateContext.o7SdkEntity.o7SdkDataManager.Manager.GetUDIDHash());
		}
	}
}
