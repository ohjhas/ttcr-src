using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataDailyMissionsSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string dialyMissionsBalancingPath = "BalancingData/DailyMissionsBalancing";

		public BalancingDataDailyMissionsSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasDailyMissionsBalancingData)
			{
				AddDailyMissionsStaticData(balancingDataEntity);
			}
		}

		private void AddDailyMissionsStaticData(StateEntity balancingEntity)
		{
			balancingEntity.AddDailyMissionsBalancingData(new Dictionary<int, DailyMissionsBalancingData>());
			DailyMissionsBalancing dailyMissionsBalancing = Resources.Load("BalancingData/DailyMissionsBalancing") as DailyMissionsBalancing;
			DailyMissionsBalancingData[] dataArray = dailyMissionsBalancing.DataArray;
			foreach (DailyMissionsBalancingData dailyMissionsBalancingData in dataArray)
			{
				balancingEntity.dailyMissionsBalancingData.Map.Add(dailyMissionsBalancingData.Missionid, dailyMissionsBalancingData);
			}
		}
	}
}
