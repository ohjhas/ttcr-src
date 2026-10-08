using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class UpgradeScorePowerupSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private InputContext inputContext;

		private IGroup<GameEntity> scoreTokens;

		public UpgradeScorePowerupSystem(Contexts contexts)
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
			return playerEntity.hasView && playerEntity.hasUpgradeScore && !playerEntity.isDead;
		}

		private void ConvertScoreTokens()
		{
			float x = gameContext.playerEntity.view.gameObject.transform.position.x;
			float maxRange = gameContext.playerEntity.view.gameObject.transform.position.x + gameContext.playerEntity.upgradeScore.Distance;
			GameEntity[] entities = scoreTokens.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView && gameEntity.isScoreToken && !gameEntity.isImprovedScoreToken && IsInEffectRange(gameEntity, x, maxRange))
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
			gameContext.playerEntity.upgradeScore.Duration -= Time.deltaTime;
			if (gameContext.playerEntity.upgradeScore.Duration <= 0f)
			{
				gameContext.playerEntity.RemoveUpgradeScore();
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
