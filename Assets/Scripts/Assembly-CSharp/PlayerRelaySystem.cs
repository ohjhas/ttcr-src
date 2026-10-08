using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class PlayerRelaySystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	private readonly StateContext stateContext;

	public PlayerRelaySystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
		stateContext = contexts.state;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.PlayerRelay);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isPlayer;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		GameEntity playerEntity = gameContext.playerEntity;
		playerEntity.ReplaceInitialTrigger("Relay");
		DestroyPreviousView(playerEntity);
		GetNewAsset(playerEntity);
		ResetDeathFlagsAndStamina(playerEntity);
		gameContext.worldEntity.ReplaceWaitToStart(0.25f);
		AddBoost(playerEntity);
	}

	private void DestroyPreviousView(GameEntity player)
	{
		Object.Destroy(player.view.gameObject, 3f);
	}

	private void GetNewAsset(GameEntity player)
	{
		SettingsSO settingsSO = SettingsService.SettingsSO;
		int amount = stateContext.GetEntitiesWithName("SecondCharSelectData").SingleEntity().integer.Amount;
		player.RemoveAsset();
		player.AddAsset("Characters/Ingame/" + settingsSO.CharSettings[amount].IngameAssetName);
		player.ReplaceSetupAbility(amount);
		string name = "CharUpgradeData" + amount;
		StateEntity upgradeData = stateContext.GetEntitiesWithName(name).SingleEntity();
		List<CharactersUpgradesBalancingData> list = stateContext.balancingDataEntity.characterUpgradesBalancingData.Map[amount];
		CharactersUpgradesBalancingData charactersUpgradesBalancingData = list.Find((CharactersUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount);
		SettingsService.SettingsSO.StartingStamina = charactersUpgradesBalancingData.Stamina;
	}

	private void ResetDeathFlagsAndStamina(GameEntity player)
	{
		player.isDead = false;
		gameContext.playerEntity.isUnderTheScreen = false;
		player.ReplaceStamina(SettingsService.SettingsSO.StartingStamina * 0.5f);
	}

	private void AddBoost(GameEntity player)
	{
		float num = SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration * 0.5f;
		float b = ((!player.hasInvulnerability) ? 0f : player.invulnerability.TimeLeft);
		float newTimeLeft = Mathf.Max(num, b);
		player.ReplaceSpeedUp(num, 1f);
		player.ReplaceInvulnerability(newTimeLeft);
		player.ReplaceOnClouds(num);
	}
}
