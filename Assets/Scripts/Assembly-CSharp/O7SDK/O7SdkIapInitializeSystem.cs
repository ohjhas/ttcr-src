using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace O7SDK
{
	public class O7SdkIapInitializeSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKPurchaseManager purchaseManager;

		private O7SDKDataManager dataManager;

		public O7SdkIapInitializeSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			purchaseManager = stateContext.o7SdkEntity.o7SdkPurchaseManager.Manager;
			dataManager = stateContext.o7SdkEntity.o7SdkDataManager.Manager;
			CheckAndroidBillingAvailableIfNeeded();
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkIapItemsFetchRequest.Added(), StateMatcher.O7SdkReadyToLoadGame.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isO7SdkIapItemsFetchRequest)
				{
					entity.isDestroyed = true;
					string iAPuPacks = dataManager.GetIAPuPacks();
					if (!string.IsNullOrEmpty(iAPuPacks))
					{
						List<IapPacks> list = O7SDKTypesMarshaling.ToList<IapPacks>(iAPuPacks);
						stateContext.o7SdkEntity.ReplaceO7SdkIapPacks(list);
						stateContext.o7SdkEntity.ReplaceO7SdkIapItemPriceData(CreateAndroidPacksDictionary(list));
					}
				}
				else
				{
					CheckiOSBillingAvailableIfNeeded();
				}
			}
		}

		private Dictionary<string, string> CreateiOSPacksDictionary(List<IapPacks> iapPacksFromServer)
		{
			List<ValidIapPacks> list = ((!stateContext.o7SdkEntity.hasO7SdkValidIapPacks) ? O7SDKTypesMarshaling.ToList<ValidIapPacks>(purchaseManager.GetValidIAPProducts()) : stateContext.o7SdkEntity.o7SdkValidIapPacks.iapValidPacks);
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (ValidIapPacks pack in list)
			{
				if (iapPacksFromServer.Exists((IapPacks x) => x.id == pack.id))
				{
					dictionary.Add(pack.id, pack.formattedPrice);
				}
			}
			return dictionary;
		}

		private Dictionary<string, string> CreateAndroidPacksDictionary(List<IapPacks> iapPacksFromServer)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			foreach (IapPacks item in iapPacksFromServer)
			{
				string price = purchaseManager.GetPrice(item.id);
				if (price != null)
				{
					dictionary.Add(item.id, price);
				}
			}
			return dictionary;
		}

		private void CheckAndroidBillingAvailableIfNeeded()
		{
			Debug.Log("CheckBillingAvailableIfNeeded()");
			List<string> list = new List<string>();
			ShopIAPsBalancingDataComponent shopIAPsBalancingData = stateContext.balancingDataEntity.shopIAPsBalancingData;
			foreach (ShopIAPsBalancingData value in shopIAPsBalancingData.Map.Values)
			{
				list.Add(value.Productid);
			}
			string listOfAllBillableItems = string.Join(",", list.ToArray());
			purchaseManager.CheckBillingAvailable(listOfAllBillableItems);
		}

		private void CheckiOSBillingAvailableIfNeeded()
		{
		}
	}
}
