using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataPlayerLevelExperienceAmountSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string BALANCING_DATA_PATH = "BalancingData/PlayerLevelExperienceAmountBalancing";

		public BalancingDataPlayerLevelExperienceAmountSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasPlayerLevelExperienceAmountData)
			{
				AddBalancingData(balancingDataEntity);
			}
		}

		private void AddBalancingData(StateEntity balancingEntity)
		{
			balancingEntity.AddPlayerLevelExperienceAmountData(new Dictionary<PlayerExperienceType, List<PlayerLevelExperienceAmountData>>());
			PlayerLevelExperienceAmountBalancing playerLevelExperienceAmountBalancing = Resources.Load("BalancingData/PlayerLevelExperienceAmountBalancing") as PlayerLevelExperienceAmountBalancing;
			foreach (object experienceType in Enum.GetValues(typeof(PlayerExperienceType)))
			{
				IEnumerable<PlayerLevelExperienceAmountBalancingData> enumerable = playerLevelExperienceAmountBalancing.DataArray.Where((PlayerLevelExperienceAmountBalancingData x) => x.PLAYEREXPERIENCETYPE == (PlayerExperienceType)experienceType);
				List<PlayerLevelExperienceAmountData> list = new List<PlayerLevelExperienceAmountData>();
				foreach (PlayerLevelExperienceAmountBalancingData item in enumerable)
				{
					list.Add(new PlayerLevelExperienceAmountData
					{
						ExpAmount = item.Expamount,
						Level = item.Level
					});
				}
				balancingEntity.playerLevelExperienceAmountData.Map.Add((PlayerExperienceType)experienceType, list);
			}
		}
	}
}
