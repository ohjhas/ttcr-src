using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ShopSoftCurrencyPacksListDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public ShopSoftCurrencyPacksListDataProviderSystem(Contexts contexts)
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
			return entity.isUIShopSoftCurrencyPacksRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			List<SoftCurrencyPacksData> newSoftCurrencyPacksDataList = CreateResponseDataList();
			foreach (UIEntity entity in entities)
			{
				entity.AddUIShopSoftCurrencyPacksResponse(newSoftCurrencyPacksDataList);
			}
		}

		private List<SoftCurrencyPacksData> CreateResponseDataList()
		{
			List<SoftCurrencyPacksData> list = new List<SoftCurrencyPacksData>();
			ShopSoftCurrencyPacksDataComponent shopSoftCurrencyPacksData = stateContext.balancingDataEntity.shopSoftCurrencyPacksData;
			foreach (KeyValuePair<int, ShopSoftCurrencyPacksBalancingData> item in shopSoftCurrencyPacksData.Map)
			{
				list.Add(new SoftCurrencyPacksData
				{
					Id = item.Value.ID,
					Name = item.Value.Name,
					Amount = item.Value.Amount,
					Price = item.Value.Price,
					Image = item.Value.Image
				});
			}
			return list;
		}
	}
}
