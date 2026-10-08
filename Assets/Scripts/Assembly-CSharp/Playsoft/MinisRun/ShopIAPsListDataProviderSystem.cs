using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ShopIAPsListDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public ShopIAPsListDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUIShopIAPsListRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			List<IAPsData> newIAPsDataList = CreateResponseDataList();
			foreach (UIEntity entity in entities)
			{
				entity.AddUIShopIAPsListResponse(newIAPsDataList);
			}
		}

		private List<IAPsData> CreateResponseDataList()
		{
			List<IAPsData> list = new List<IAPsData>();
			ShopIAPsBalancingDataComponent shopIAPsBalancingData = stateContext.balancingDataEntity.shopIAPsBalancingData;
			Dictionary<string, string> dictionary = null;
			if (stateContext.o7SdkEntity != null && stateContext.o7SdkEntity.o7SdkIapItemPriceData != null && stateContext.o7SdkEntity.o7SdkIapItemPriceData.pricesDictionary != null)
			{
				dictionary = stateContext.o7SdkEntity.o7SdkIapItemPriceData.pricesDictionary;
			}
			foreach (KeyValuePair<int, ShopIAPsBalancingData> item in shopIAPsBalancingData.Map)
			{
				string price = "---";
				if (dictionary != null && dictionary.ContainsKey(item.Value.Productid))
				{
					price = dictionary[item.Value.Productid];
				}
				list.Add(new IAPsData
				{
					Id = item.Value.ID,
					Name = item.Value.Name,
					Amount = item.Value.Amount,
					Price = price,
					Image = item.Value.Image,
					ProductId = item.Value.Productid
				});
			}
			return list;
		}
	}
}
