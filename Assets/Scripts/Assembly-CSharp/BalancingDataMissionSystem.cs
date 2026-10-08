using System.Collections.Generic;
using Entitas;
using I2.Loc;
using UnityEngine;

public class BalancingDataMissionSystem : IInitializeSystem, ISystem
{
	private readonly StateContext stateContext;

	public const string missionBalancingPath = "BalancingData/MissionsBalancing";

	public BalancingDataMissionSystem(Contexts contexts)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		StateEntity balancingDataEntity = stateContext.balancingDataEntity;
		if (!balancingDataEntity.hasMissionBalancingData)
		{
			AddMissionData(balancingDataEntity);
		}
	}

	private void AddMissionData(StateEntity balancingEntity)
	{
		balancingEntity.AddMissionBalancingData(new Dictionary<int, MissionsBalancingData>());
		MissionsBalancing missionsBalancing = Resources.Load("BalancingData/MissionsBalancing") as MissionsBalancing;
		MissionsBalancingData[] dataArray = missionsBalancing.DataArray;
		foreach (MissionsBalancingData missionsBalancingData in dataArray)
		{
			missionsBalancingData.Levelname = ScriptLocalization.Get(missionsBalancingData.Levelname);
			balancingEntity.missionBalancingData.Map.Add(missionsBalancingData.Levelindex, missionsBalancingData);
		}
	}
}
