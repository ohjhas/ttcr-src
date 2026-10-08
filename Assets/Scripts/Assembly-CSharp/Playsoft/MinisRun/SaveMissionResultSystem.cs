using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class SaveMissionResultSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		public SaveMissionResultSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.MissionResult);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer && entity.hasMissionResult;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				int bestResultAmount = GetBestResultAmount();
				int savedStars = GetSavedStars();
				int bestStars = GetBestStars(savedStars);
				SaveStarsResult(bestStars);
				SaveResultValue(bestResultAmount);
				stateContext.CreateEntity().AddLevelCompleted(SettingsService.SettingsSO.WorldIndex, bestStars, bestResultAmount, bestStars - savedStars);
			}
		}

		private void SaveStarsResult(int stars)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("MissionDataSend");
			stateEntity.AddInteger(stars);
		}

		private void SaveResultValue(int resultValue)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("ResultValueSend");
			stateEntity.AddInteger(resultValue);
		}

		private int GetSavedStars()
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("MissionData" + SettingsService.SettingsSO.WorldIndex).SingleEntity();
			return (stateEntity != null) ? stateEntity.integer.Amount : 0;
		}

		private int GetBestStars(int savedStars)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("MissionData" + SettingsService.SettingsSO.WorldIndex).SingleEntity();
			GameEntity playerEntity = gameContext.playerEntity;
			int num = ((!playerEntity.hasMissionResult) ? savedStars : Mathf.Max(savedStars, playerEntity.missionResult.Stars));
			stateEntity.ReplaceInteger(num);
			return num;
		}

		private int GetBestResultAmount()
		{
			string name = "ResultValue" + SettingsService.SettingsSO.WorldIndex;
			StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
			int num = ((stateEntity != null && stateEntity.hasInteger) ? stateEntity.integer.Amount : 0);
			GameEntity playerEntity = gameContext.playerEntity;
			int num2 = ((!playerEntity.hasMissionResult) ? num : Mathf.Max(num, playerEntity.missionResult.ResultValue));
			stateEntity.ReplaceInteger(num2);
			return num2;
		}
	}
}
