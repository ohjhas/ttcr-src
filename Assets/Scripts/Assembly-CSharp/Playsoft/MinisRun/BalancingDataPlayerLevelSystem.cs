using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataPlayerLevelSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string PLAYER_LEVELS_BALANCING_PATH = "BalancingData/PlayerLevelBalancing";

		public const string PLAYER_LEVELS_REWARDS_BALANCING_PATH = "BalancingData/PlayerLevelRewardsBalancing";

		public BalancingDataPlayerLevelSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasPlayerLevelBalancingData)
			{
				AddPlayerLevelsData(balancingDataEntity);
			}
		}

		private void AddPlayerLevelsData(StateEntity balancingEntity)
		{
			balancingEntity.AddPlayerLevelBalancingData(new Dictionary<int, PlayerLevelData>());
			PlayerLevelBalancing playerLevelBalancing = Resources.Load("BalancingData/PlayerLevelBalancing") as PlayerLevelBalancing;
			PlayerLevelRewardsBalancing playerLevelRewardsBalancing = Resources.Load("BalancingData/PlayerLevelRewardsBalancing") as PlayerLevelRewardsBalancing;
			PlayerLevelBalancingData[] dataArray = playerLevelBalancing.DataArray;
			foreach (PlayerLevelBalancingData entry in dataArray)
			{
				List<RewardData> list = new List<RewardData>();
				IEnumerable<PlayerLevelRewardsBalancingData> enumerable = playerLevelRewardsBalancing.DataArray.Where((PlayerLevelRewardsBalancingData x) => x.Level == entry.Level);
				foreach (PlayerLevelRewardsBalancingData item in enumerable)
				{
					list.Add(new RewardData
					{
						Amount = item.Amount,
						Index = item.Itemindex,
						Type = item.ITEMTYPE
					});
				}
				balancingEntity.playerLevelBalancingData.Map.Add(entry.Level, new PlayerLevelData
				{
					LevelNumber = entry.Level,
					RequiredExperience = entry.Requiredxp,
					Rewards = list
				});
			}
		}
	}
}
