using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataStaminaSettingsSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string staminaBalancingPath = "BalancingData/StaminaSettingsBalancing";

		public BalancingDataStaminaSettingsSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasStaminaSettingsBalancingData)
			{
				AddStaminaSettingsData(balancingDataEntity);
			}
		}

		private void AddStaminaSettingsData(StateEntity balancingEntity)
		{
			balancingEntity.AddStaminaSettingsBalancingData(new Dictionary<StaminaSettings, float>());
			StaminaSettingsBalancing staminaSettingsBalancing = Resources.Load("BalancingData/StaminaSettingsBalancing") as StaminaSettingsBalancing;
			StaminaSettingsBalancingData[] dataArray = staminaSettingsBalancing.DataArray;
			foreach (StaminaSettingsBalancingData staminaSettingsBalancingData in dataArray)
			{
				balancingEntity.staminaSettingsBalancingData.Map.Add(staminaSettingsBalancingData.STAMINASETTINGS, staminaSettingsBalancingData.Amount);
			}
		}
	}
}
