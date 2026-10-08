using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace O7SDK
{
	public class O7SdkPurchaseSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ITearDownSystem, ISystem
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		private O7SDKPurchaseManager purchaseManager;

		private RectTransform originRectTransform;

		public O7SdkPurchaseSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
			uiContext = context.uI;
		}

		public void Initialize()
		{
			purchaseManager = stateContext.o7SdkEntity.o7SdkPurchaseManager.Manager;
			purchaseManager.OnConfigurationSuccess += OnPurchaseAvailable;
			purchaseManager.OnConfigurationFailed += OnConfigurationFailed;
			purchaseManager.OnPurchaseSucceeded += OnPurchaseSucceeded;
			purchaseManager.OnPurchaseFailed += OnPurchaseFailed;
			purchaseManager.OnPurchasesRestored += OnPurchaseRestored;
			purchaseManager.OnLoadedValidIAPProducts += OnValidIapItemsLoaded;
		}

		public void TearDown()
		{
			purchaseManager = stateContext.o7SdkEntity.o7SdkPurchaseManager.Manager;
			purchaseManager.OnConfigurationSuccess -= OnPurchaseAvailable;
			purchaseManager.OnConfigurationFailed -= OnConfigurationFailed;
			purchaseManager.OnPurchaseSucceeded -= OnPurchaseSucceeded;
			purchaseManager.OnPurchaseFailed -= OnPurchaseFailed;
			purchaseManager.OnPurchasesRestored -= OnPurchaseRestored;
			purchaseManager.OnLoadedValidIAPProducts -= OnValidIapItemsLoaded;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkIapBuyRequest.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasO7SdkIapBuyRequest;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (Application.internetReachability == NetworkReachability.NotReachable)
				{
					ShowErrorPopUp(ExplanationType.NoInternet);
					entity.isDestroyed = true;
					break;
				}
				if (purchaseManager.IsPurchasingAvailable())
				{
					purchaseManager.StartPurchasing(entity.o7SdkIapBuyRequest.productId);
					originRectTransform = entity.o7SdkIapBuyRequest.transform;
					ShowLoading();
				}
				else
				{
					ShowErrorPopUp(ExplanationType.IapNotAvailable);
					entity.isDestroyed = true;
				}
			}
		}

		private void OnPurchaseAvailable()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkIapItemsFetchRequest = true;
		}

		private void OnConfigurationFailed()
		{
		}

		private void OnPurchaseFailed(string jsonString)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			if (jsonString.Contains("CANCELED"))
			{
				stateEntity.AddO7SdkIapBuyFail(jsonString);
			}
			else
			{
				stateEntity.AddO7SdkIapBuyError(jsonString);
			}
		}

		private void OnPurchaseRestored(string jsonString)
		{
		}

		private void OnPurchaseSucceeded(string jsonString)
		{
			PurchaseData purchaseData = JsonUtility.FromJson<PurchaseData>(jsonString);
			if (purchaseData.justRestore)
			{
				purchaseManager.ConsumePurchase(purchaseData.itemId);
			}
			else if (purchaseData.purchaseState == PurchaseState.Purchased)
			{
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.ReplaceO7SdkIapBuySuccess(purchaseData.itemId, originRectTransform);
			}
		}

		private void OnValidIapItemsLoaded(string json)
		{
			if (string.IsNullOrEmpty(json))
			{
				return;
			}
			try
			{
				List<ValidIapPacks> newIapValidPacks = O7SDKTypesMarshaling.ToList<ValidIapPacks>(json);
				stateContext.o7SdkEntity.AddO7SdkValidIapPacks(newIapValidPacks);
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isO7SdkIapItemsFetchRequest = true;
			}
			catch (Exception ex)
			{
				Debug.LogWarning("[O7SdkPurchaseSystem] iap items not loaded: " + ex.Message);
			}
		}

		private void ShowErrorPopUp(ExplanationType type)
		{
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIDisplayExplanation(type);
		}

		private void ShowLoading()
		{
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddLoadingSpinnerShow(true);
		}
	}
}
