using System.Collections.Generic;
using Entitas;
using I2.Loc;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataShopSoftCurrencyPacksSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string BALANCING_DATA_PATH = "BalancingData/ShopSoftCurrencyPacksBalancing";

		public BalancingDataShopSoftCurrencyPacksSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasShopSoftCurrencyPacksData)
			{
				AddBalancingData(balancingDataEntity);
			}
		}

		private void AddBalancingData(StateEntity balancingEntity)
		{
			balancingEntity.AddShopSoftCurrencyPacksData(new Dictionary<int, ShopSoftCurrencyPacksBalancingData>());
			ShopSoftCurrencyPacksBalancing shopSoftCurrencyPacksBalancing = Resources.Load("BalancingData/ShopSoftCurrencyPacksBalancing") as ShopSoftCurrencyPacksBalancing;
			ShopSoftCurrencyPacksBalancingData[] dataArray = shopSoftCurrencyPacksBalancing.DataArray;
			foreach (ShopSoftCurrencyPacksBalancingData shopSoftCurrencyPacksBalancingData in dataArray)
			{
				if (balancingEntity.shopSoftCurrencyPacksData.Map.ContainsKey(shopSoftCurrencyPacksBalancingData.ID))
				{
					Debug.LogError(string.Format("[Balancing] Entry with id {0} already exists", shopSoftCurrencyPacksBalancingData.ID));
					continue;
				}
				shopSoftCurrencyPacksBalancingData.Name = ScriptLocalization.Get(shopSoftCurrencyPacksBalancingData.Name);
				balancingEntity.shopSoftCurrencyPacksData.Map.Add(shopSoftCurrencyPacksBalancingData.ID, shopSoftCurrencyPacksBalancingData);
			}
		}
	}
}
