using Entitas;
using UnityEngine;

public class StaminaDecreaseSystem : IExecuteSystem, ISystem
{
	private readonly GameContext gameContext;

	private readonly StateContext stateContext;

	private float WaitToStart = 0.5f;

	public StaminaDecreaseSystem(Contexts contexts)
	{
		gameContext = contexts.game;
		stateContext = contexts.state;
	}

	public void Execute()
	{
		WaitToStart -= Time.deltaTime;
		if (WaitToStart >= 0f)
		{
			return;
		}
		GameEntity playerEntity = gameContext.playerEntity;
		if (!playerEntity.isDead && !playerEntity.isGodMode && !playerEntity.hasLevelFinished)
		{
			GameEntity worldEntity = gameContext.worldEntity;
			float num = Mathf.Clamp01(worldEntity.world.Speed / SettingsService.SettingsSO.BaseSpeed);
			float num2 = stateContext.balancingDataEntity.staminaSettingsBalancingData.Map[StaminaSettings.StaminaDecreasingFactor];
			num *= Time.deltaTime * num2;
			float num3 = playerEntity.stamina.Value - num;
			playerEntity.ReplaceStamina(num3);
			if (num3 <= 0f)
			{
				playerEntity.isDead = true;
			}
		}
	}
}
