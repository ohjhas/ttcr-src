using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class BalancingDataEconomySystem : IInitializeSystem, ISystem
{
	private readonly StateContext stateContext;

	public const string initBalancingPath = "BalancingData/InitConfigBalancing";

	public BalancingDataEconomySystem(Contexts contexts)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		if (!stateContext.isBalancingData)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isBalancingData = true;
			AddInitConfig(stateEntity);
		}
	}

	private void AddInitConfig(StateEntity balancingEntity)
	{
		balancingEntity.AddInitConfigData(new Dictionary<EnumSettings, int>());
		InitConfigBalancing initConfigBalancing = Resources.Load("BalancingData/InitConfigBalancing") as InitConfigBalancing;
		InitConfigBalancingData[] dataArray = initConfigBalancing.DataArray;
		foreach (InitConfigBalancingData initConfigBalancingData in dataArray)
		{
			balancingEntity.initConfigData.Map.Add(initConfigBalancingData.ENUMSETTINGS, initConfigBalancingData.Amount);
		}
	}
}
