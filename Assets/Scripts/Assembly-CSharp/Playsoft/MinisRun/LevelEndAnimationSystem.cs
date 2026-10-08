using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class LevelEndAnimationSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly InputContext inputContext;

		public LevelEndAnimationSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			inputContext = contexts.input;
		}

		public void Execute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (playerEntity != null && !playerEntity.isDead && playerEntity.hasLevelFinished && playerEntity.isCameraStop)
			{
				float speed = CalculateSpeed();
				MovePlayer(playerEntity, speed);
				if (playerEntity.levelFinished.DistanceToEnd > 0f)
				{
					UpdateDistanceToEnd(playerEntity, speed);
				}
			}
		}

		private float CalculateSpeed()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			if (worldEntity.world.Speed < SettingsService.SettingsSO.BaseSpeed)
			{
				worldEntity.world.Speed = Mathf.Lerp(worldEntity.world.Speed, SettingsService.SettingsSO.BaseSpeed, Time.deltaTime * SettingsService.SettingsSO.HitRecoverSpeed);
			}
			float num = ((!gameContext.playerEntity.hasAbilityFasterMovement) ? 1f : gameContext.playerEntity.abilityFasterMovement.Bonus);
			float num2 = worldEntity.world.Speed * num;
			return num2 * Time.smoothDeltaTime;
		}

		private void MovePlayer(GameEntity player, float speed)
		{
			player.view.gameObject.transform.position += Vector3.right * speed;
		}

		private void UpdateDistanceToEnd(GameEntity player, float speed)
		{
			player.levelFinished.DistanceToEnd -= speed;
			if (player.levelFinished.DistanceToEnd <= 0f)
			{
				InputEntity inputEntity = inputContext.CreateEntity();
				inputEntity.isInput = true;
				inputEntity.isLevelEndAnimationFinished = true;
			}
		}
	}
}
