using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class UpgradeSingleScorePowerupSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private InputContext inputContext;

		private IGroup<GameEntity> scoreTokens;

		public UpgradeSingleScorePowerupSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			inputContext = contexts.input;
			scoreTokens = gameContext.GetGroup(GameMatcher.CollectibleScore);
		}

		public void Execute()
		{
			if (CanExecute())
			{
				ConvertScoreTokens();
				CalculateRemainingTime();
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.hasUpgradeSingleScore && !playerEntity.isDead;
		}

		private void ConvertScoreTokens()
		{
			float minRange = gameContext.playerEntity.view.gameObject.transform.position.x + gameContext.playerEntity.upgradeSingleScore.Distance * 0.5f;
			float maxRange = gameContext.playerEntity.view.gameObject.transform.position.x + gameContext.playerEntity.upgradeSingleScore.Distance;
			GameEntity[] entities = scoreTokens.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView && gameEntity.isScoreToken && !gameEntity.isImprovedScoreToken && IsInEffectRange(gameEntity, minRange, maxRange))
				{
					string assetName = gameEntity.asset.name.Replace("LevelElements/", string.Empty);
					LevelElement correctLevelElement = GetCorrectLevelElement(assetName);
					if (correctLevelElement != null)
					{
						LevelElementData levelElementData = new LevelElementData(correctLevelElement.Data);
						levelElementData.Position = gameEntity.position.Value + SettingsService.GetSetting<SettingsPowerupsSO>().ImprovedScorePosOffest;
						levelElementData.AssetName = correctLevelElement.name;
						levelElementData.Collectible.IsImprovedScoreToken = true;
						gameContext.CreateLevelElement(levelElementData);
						AddParticleEffects(levelElementData.Position);
					}
					gameEntity.isImprovedScoreToken = true;
					gameContext.playerEntity.upgradeSingleScore.Duration = 0f;
					break;
				}
			}
		}

		private void AddParticleEffects(Vector3 position)
		{
			InputEntity inputEntity = inputContext.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.AddCreateParticles(position);
		}

		private void CalculateRemainingTime()
		{
			gameContext.playerEntity.upgradeSingleScore.Duration -= Time.deltaTime;
			if (gameContext.playerEntity.upgradeSingleScore.Duration <= 0f)
			{
				gameContext.playerEntity.RemoveUpgradeSingleScore();
			}
		}

		private LevelElement GetCorrectLevelElement(string assetName)
		{
			return SettingsService.AssetsSettingsSO.GetLevelElement(assetName);
		}

		private bool IsInEffectRange(GameEntity entity, float minRange, float maxRange)
		{
			return entity.hasPosition && entity.position.Value.x > minRange && entity.position.Value.x < maxRange;
		}
	}
}
