using System.Collections.Generic;
using Entitas;
using I2.Loc;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataCharactersSystem : IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string characterUpgradesBalancingPath = "BalancingData/CharactersUpgradesBalancing";

		public const string characterStaticBalancingPath = "BalancingData/CharactersStaticBalancing";

		public BalancingDataCharactersSystem(Contexts contexts)
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
			if (!balancingDataEntity.hasCharacterStaticBalancingData)
			{
				AddCharacterStaticData(balancingDataEntity);
			}
		}

		private void LoadUpgradesBalancingData()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasCharacterUpgradesBalancingData)
			{
				AddCharacterUpgradesData(balancingDataEntity);
			}
		}

		private void AddCharacterStaticData(StateEntity balancingEntity)
		{
			balancingEntity.AddCharacterStaticBalancingData(new Dictionary<int, CharactersStaticBalancingData>());
			CharactersStaticBalancing charactersStaticBalancing = Resources.Load("BalancingData/CharactersStaticBalancing") as CharactersStaticBalancing;
			CharactersStaticBalancingData[] dataArray = charactersStaticBalancing.DataArray;
			foreach (CharactersStaticBalancingData charactersStaticBalancingData in dataArray)
			{
				charactersStaticBalancingData.Name = ScriptLocalization.Get(charactersStaticBalancingData.Name);
				charactersStaticBalancingData.Welcomemessage = ScriptLocalization.Get(charactersStaticBalancingData.Welcomemessage);
				balancingEntity.characterStaticBalancingData.Map.Add(charactersStaticBalancingData.ID, charactersStaticBalancingData);
			}
		}

		private void AddCharacterUpgradesData(StateEntity balancingEntity)
		{
			balancingEntity.AddCharacterUpgradesBalancingData(new Dictionary<int, List<CharactersUpgradesBalancingData>>());
			CharactersUpgradesBalancing charactersUpgradesBalancing = Resources.Load("BalancingData/CharactersUpgradesBalancing") as CharactersUpgradesBalancing;
			CharactersUpgradesBalancingData[] dataArray = charactersUpgradesBalancing.DataArray;
			foreach (CharactersUpgradesBalancingData charactersUpgradesBalancingData in dataArray)
			{
				if (!balancingEntity.characterUpgradesBalancingData.Map.ContainsKey(charactersUpgradesBalancingData.ID))
				{
					balancingEntity.characterUpgradesBalancingData.Map.Add(charactersUpgradesBalancingData.ID, new List<CharactersUpgradesBalancingData>());
				}
				balancingEntity.characterUpgradesBalancingData.Map[charactersUpgradesBalancingData.ID].Add(charactersUpgradesBalancingData);
			}
		}
	}
}
