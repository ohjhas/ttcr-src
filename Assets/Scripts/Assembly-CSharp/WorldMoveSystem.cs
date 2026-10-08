using Entitas;
using UnityEngine;

public class WorldMoveSystem : IExecuteSystem, ISystem
{
	private readonly GameContext gameContext;

	public WorldMoveSystem(Contexts contexts)
	{
		gameContext = contexts.game;
	}

	public void Execute()
	{
		GameEntity worldEntity = gameContext.worldEntity;
		worldEntity.waitToStart.Duration -= Time.deltaTime;
		if (worldEntity.waitToStart.Duration >= 0f)
		{
			return;
		}
		GameEntity playerEntity = gameContext.playerEntity;
		if (playerEntity != null && !playerEntity.isDead && !playerEntity.isCameraStop && (!playerEntity.hasRaccoonIntro || playerEntity.raccoonIntro.Step == RaccoonIntroStep.HUDRevealed))
		{
			if (worldEntity.world.Speed < SettingsService.SettingsSO.BaseSpeed)
			{
				worldEntity.world.Speed = Mathf.Lerp(worldEntity.world.Speed, SettingsService.SettingsSO.BaseSpeed, Time.deltaTime * SettingsService.SettingsSO.HitRecoverSpeed);
			}
			float num = ((!gameContext.playerEntity.hasAbilityFasterMovement) ? 1f : gameContext.playerEntity.abilityFasterMovement.Bonus);
			float num2 = worldEntity.world.Speed * num;
			float num3 = num2 * Time.smoothDeltaTime;
			worldEntity.world.Distance += num3;
		}
	}
}
