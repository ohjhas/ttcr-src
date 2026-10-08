using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class FlyAwayObjSystem : ReactiveSystem<GameEntity>
	{
		private GameContext gameContext;

		public FlyAwayObjSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isFlyAway;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.FlyAway.Added());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				AddHitForce(entity);
				int score = AddScore();
				ShowGainedPoints(entity, score);
				entity.isDisableColliders = true;
				entity.isFadeOut = true;
			}
		}

		private void AddHitForce(GameEntity entity)
		{
			Rigidbody2D componentInChildren = entity.view.gameObject.GetComponentInChildren<Rigidbody2D>();
			componentInChildren.isKinematic = false;
			float x = SettingsService.SettingsSO.MaxHitForce.x;
			float y = SettingsService.SettingsSO.MaxHitForce.y;
			Vector2 force = new Vector2(Random.Range(x * 0.5f, x), Random.Range(-0.5f * y, y));
			componentInChildren.AddForce(force);
			float maxHitTorque = SettingsService.SettingsSO.MaxHitTorque;
			componentInChildren.AddTorque(Random.Range(maxHitTorque * 0.5f, maxHitTorque));
		}

		private int AddScore()
		{
			int value = gameContext.worldEntity.score.Value;
			float num = ((!gameContext.playerEntity.hasScoreMultiplier) ? 1f : gameContext.playerEntity.scoreMultiplier.Factor);
			int num2 = Mathf.RoundToInt((float)SettingsService.SettingsSO.DestroyedObstacleScore * num);
			value += num2;
			gameContext.worldEntity.ReplaceScore(value);
			return num2;
		}

		private void ShowGainedPoints(GameEntity entity, int score)
		{
			GameEntity gameEntity = gameContext.CreateEntity();
			Vector3 position = gameContext.playerEntity.view.gameObject.transform.position;
			position += entity.view.gameObject.transform.position;
			position *= 0.5f;
			gameEntity.AddPosition(position);
			gameEntity.AddAsset("GainedScoreCanvas");
			gameEntity.isMovable = true;
			gameEntity.isPoolable = true;
			gameEntity.AddGainedScore(score);
		}
	}
}
