using System.Collections.Generic;
using Entitas;
using I2.Loc;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataShopIAPsSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string BALANCING_DATA_PATH = "BalancingData/ShopIAPsBalancing";

		public BalancingDataShopIAPsSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasShopIAPsBalancingData)
			{
				AddBalancingData(balancingDataEntity);
			}
		}

		private void AddBalancingData(StateEntity balancingEntity)
		{
			balancingEntity.AddShopIAPsBalancingData(new Dictionary<int, ShopIAPsBalancingData>());
			ShopIAPsBalancing shopIAPsBalancing = Resources.Load("BalancingData/ShopIAPsBalancing") as ShopIAPsBalancing;
			ShopIAPsBalancingData[] dataArray = shopIAPsBalancing.DataArray;
			foreach (ShopIAPsBalancingData shopIAPsBalancingData in dataArray)
			{
				if (balancingEntity.shopIAPsBalancingData.Map.ContainsKey(shopIAPsBalancingData.ID))
				{
					Debug.LogError(string.Format("[Balancing] Entry with id {0} already exists", shopIAPsBalancingData.ID));
					continue;
				}
				shopIAPsBalancingData.Name = ScriptLocalization.Get(shopIAPsBalancingData.Name);
				balancingEntity.shopIAPsBalancingData.Map.Add(shopIAPsBalancingData.ID, shopIAPsBalancingData);
			}
		}
	}
}
