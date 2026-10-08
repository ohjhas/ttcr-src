using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class BalancingDataTutorialOutgameUnlockLevelsSystem : IInitializeSystem, ISystem
	{
		public const string BALANCING_DATA_PATH = "BalancingData/TutorialOutgameUnlockLevelsBalancing";

		private readonly StateContext stateContext;

		public BalancingDataTutorialOutgameUnlockLevelsSystem(Contexts contexts)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			StateEntity balancingDataEntity = stateContext.balancingDataEntity;
			if (!balancingDataEntity.hasTutorialOutgameUnlockLevelsBalancingData)
			{
				AddAbilitiesData(balancingDataEntity);
			}
		}

		private void AddAbilitiesData(StateEntity balancingEntity)
		{
			balancingEntity.AddTutorialOutgameUnlockLevelsBalancingData(new Dictionary<TutorialOutgameLevel, TutorialOutgameUnlockLevelData>());
			TutorialOutgameUnlockLevelsBalancing tutorialOutgameUnlockLevelsBalancing = Resources.Load("BalancingData/TutorialOutgameUnlockLevelsBalancing") as TutorialOutgameUnlockLevelsBalancing;
			TutorialOutgameUnlockLevelsBalancingData[] dataArray = tutorialOutgameUnlockLevelsBalancing.DataArray;
			foreach (TutorialOutgameUnlockLevelsBalancingData tutorialOutgameUnlockLevelsBalancingData in dataArray)
			{
				balancingEntity.tutorialOutgameUnlockLevelsBalancingData.Map.Add(tutorialOutgameUnlockLevelsBalancingData.TUTORIALOUTGAMELEVEL, new TutorialOutgameUnlockLevelData
				{
					PassedLevelId = tutorialOutgameUnlockLevelsBalancingData.Passedleveltrigger
				});
			}
		}
	}
}
