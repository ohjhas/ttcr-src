using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataRestoreSettingsSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string restoreBalancingPath = "BalancingData/RestoreBalancing";

		public BalancingDataRestoreSettingsSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasRestoreSettingsBalancingData)
			{
				AddRestoreSettingsData(balancingDataEntity);
			}
		}

		private void AddRestoreSettingsData(StateEntity balancingEntity)
		{
			balancingEntity.AddRestoreSettingsBalancingData(new Dictionary<RestoreSettings, int>());
			RestoreSettingsBalancing restoreSettingsBalancing = Resources.Load("BalancingData/RestoreBalancing") as RestoreSettingsBalancing;
			RestoreSettingsBalancingData[] dataArray = restoreSettingsBalancing.DataArray;
			foreach (RestoreSettingsBalancingData restoreSettingsBalancingData in dataArray)
			{
				balancingEntity.restoreSettingsBalancingData.Map.Add(restoreSettingsBalancingData.RESTORESETTINGS, restoreSettingsBalancingData.Amount);
			}
		}
	}
}
