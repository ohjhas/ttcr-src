using System.Collections.Generic;
using Entitas;
using I2.Loc;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataCompanionsSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string companionUpgradesBalancingPath = "BalancingData/CompanionsUpgradesBalancing";

		public const string companionStaticBalancingPath = "BalancingData/CompanionsStaticBalancing";

		public BalancingDataCompanionsSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			LoadStaticBalancingData();
			LoadUpgradesBalancingData();
		}

		private void LoadStaticBalancingData()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasCompanionsStaticBalancingData)
			{
				AddCompanionsStaticData(balancingDataEntity);
			}
		}

		private void LoadUpgradesBalancingData()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasCompanionsUpgradesBalancingData)
			{
				AddCompanionsUpgradesData(balancingDataEntity);
			}
		}

		private void AddCompanionsStaticData(StateEntity balancingEntity)
		{
			balancingEntity.AddCompanionsStaticBalancingData(new Dictionary<int, CompanionsStaticBalancingData>());
			CompanionsStaticBalancing companionsStaticBalancing = Resources.Load("BalancingData/CompanionsStaticBalancing") as CompanionsStaticBalancing;
			CompanionsStaticBalancingData[] dataArray = companionsStaticBalancing.DataArray;
			foreach (CompanionsStaticBalancingData companionsStaticBalancingData in dataArray)
			{
				companionsStaticBalancingData.Name = ScriptLocalization.Get(companionsStaticBalancingData.Name);
				balancingEntity.companionsStaticBalancingData.Map.Add(companionsStaticBalancingData.ID, companionsStaticBalancingData);
			}
		}

		private void AddCompanionsUpgradesData(StateEntity balancingEntity)
		{
			balancingEntity.AddCompanionsUpgradesBalancingData(new Dictionary<int, List<CompanionsUpgradesBalancingData>>());
			CompanionsUpgradesBalancing companionsUpgradesBalancing = Resources.Load("BalancingData/CompanionsUpgradesBalancing") as CompanionsUpgradesBalancing;
			CompanionsUpgradesBalancingData[] dataArray = companionsUpgradesBalancing.DataArray;
			foreach (CompanionsUpgradesBalancingData companionsUpgradesBalancingData in dataArray)
			{
				if (!balancingEntity.companionsUpgradesBalancingData.Map.ContainsKey(companionsUpgradesBalancingData.ID))
				{
					balancingEntity.companionsUpgradesBalancingData.Map.Add(companionsUpgradesBalancingData.ID, new List<CompanionsUpgradesBalancingData>());
				}
				balancingEntity.companionsUpgradesBalancingData.Map[companionsUpgradesBalancingData.ID].Add(companionsUpgradesBalancingData);
			}
		}
	}
}
