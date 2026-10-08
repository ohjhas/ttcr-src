using System.Collections.Generic;
using Entitas;
using I2.Loc;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataAbilitiesSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string abilitiesBalancingPath = "BalancingData/AbilitiesBalancing";

		public BalancingDataAbilitiesSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasAbilitiesBalancingData)
			{
				AddAbilitiesData(balancingDataEntity);
			}
		}

		private void AddAbilitiesData(StateEntity balancingEntity)
		{
			balancingEntity.AddAbilitiesBalancingData(new Dictionary<AbilityType, AbilitiesBalancingData>());
			AbilitiesBalancing abilitiesBalancing = Resources.Load("BalancingData/AbilitiesBalancing") as AbilitiesBalancing;
			AbilitiesBalancingData[] dataArray = abilitiesBalancing.DataArray;
			foreach (AbilitiesBalancingData abilitiesBalancingData in dataArray)
			{
				abilitiesBalancingData.Name = ScriptLocalization.Get(abilitiesBalancingData.Name);
				abilitiesBalancingData.Description = ScriptLocalization.Get(abilitiesBalancingData.Description);
				balancingEntity.abilitiesBalancingData.Map.Add(abilitiesBalancingData.ABILITYTYPE, abilitiesBalancingData);
			}
		}
	}
}
