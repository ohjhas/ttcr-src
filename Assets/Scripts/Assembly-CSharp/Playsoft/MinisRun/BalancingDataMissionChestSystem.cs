using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataMissionChestSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string chestBalancingPath = "BalancingData/MissionChestsBalancing";

		public BalancingDataMissionChestSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasMissionChestsBalancingData)
			{
				AddMissionChestsData(balancingDataEntity);
			}
		}

		private void AddMissionChestsData(StateEntity balancingEntity)
		{
			balancingEntity.AddMissionChestsBalancingData(new Dictionary<int, MissionChestsBalancingData>());
			MissionChestsBalancing missionChestsBalancing = Resources.Load("BalancingData/MissionChestsBalancing") as MissionChestsBalancing;
			MissionChestsBalancingData[] dataArray = missionChestsBalancing.DataArray;
			foreach (MissionChestsBalancingData missionChestsBalancingData in dataArray)
			{
				balancingEntity.missionChestsBalancingData.Map.Add(missionChestsBalancingData.Levelindex, missionChestsBalancingData);
			}
		}
	}
}
