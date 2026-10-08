using System.Collections.Generic;
using Entitas;
using I2.Loc;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataChestsSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string chestsBalancingPath = "BalancingData/ChestsListBalancing";

		public const string rewardsBalancingPath = "BalancingData/ChestsContentBalancing";

		public BalancingDataChestsSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasGatchaBalancingData)
			{
				AddGatchaData(balancingDataEntity);
			}
		}

		private void AddGatchaData(StateEntity balancingEntity)
		{
			balancingEntity.AddGatchaBalancingData(new Dictionary<int, ChestData>());
			ChestsListBalancing chestsListBalancing = Resources.Load("BalancingData/ChestsListBalancing") as ChestsListBalancing;
			ChestsContentBalancing chestsContentBalancing = Resources.Load("BalancingData/ChestsContentBalancing") as ChestsContentBalancing;
			List<ChestsContentBalancingData> rewardsList = new List<ChestsContentBalancingData>(chestsContentBalancing.DataArray);
			ChestsListBalancingData[] dataArray = chestsListBalancing.DataArray;
			foreach (ChestsListBalancingData chestsListBalancingData in dataArray)
			{
				if (!balancingEntity.gatchaBalancingData.Map.ContainsKey(chestsListBalancingData.Chestid))
				{
					List<ChestRewardData> rewardsData = GetRewardsData(chestsListBalancingData.Chestid, rewardsList);
					if (rewardsData != null && rewardsData.Count > 0)
					{
						ChestData value = new ChestData
						{
							Id = chestsListBalancingData.Chestid,
							Name = ScriptLocalization.Get(chestsListBalancingData.Name),
							ContentDescription = ScriptLocalization.Get(chestsListBalancingData.Contentdescription),
							AssetName = chestsListBalancingData.Assetname,
							ItemType = chestsListBalancingData.ITEMTYPE,
							InitialWaitTime = chestsListBalancingData.Initialwaittime,
							WaitTime = chestsListBalancingData.Waittime,
							PriceSC = chestsListBalancingData.Pricesc,
							PriceHC = chestsListBalancingData.Pricehc,
							MinPartsAmount = chestsListBalancingData.Minpartsamount,
							MaxPartsAmount = chestsListBalancingData.Maxpartsamount,
							RewardsData = rewardsData,
							IsVisibleInShop = chestsListBalancingData.Shopvisibility,
							Rarity = chestsListBalancingData.RARITY
						};
						balancingEntity.gatchaBalancingData.Map.Add(chestsListBalancingData.Chestid, value);
					}
				}
			}
		}

		private List<ChestRewardData> GetRewardsData(int chestId, List<ChestsContentBalancingData> rewardsList)
		{
			List<ChestsContentBalancingData> list = rewardsList.FindAll((ChestsContentBalancingData ch) => ch.Chestid == chestId);
			rewardsList.RemoveAll((ChestsContentBalancingData ch) => ch.Chestid == chestId);
			List<ChestRewardData> list2 = new List<ChestRewardData>();
			for (int num = 0; num < list.Count; num++)
			{
				ChestRewardData item = new ChestRewardData
				{
					Group = list[num].GROUPTYPE,
					Type = list[num].ITEMTYPE,
					Rarity = list[num].RARITY,
					Index = list[num].Index,
					Probability = list[num].Probability,
					AmountMin = list[num].Amountmin,
					AmountMax = list[num].Amountmax
				};
				list2.Add(item);
			}
			return list2;
		}
	}
}
