using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerReviveSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public PlayerReviveSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.PlayerRevive);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			playerEntity.ReplaceInitialTrigger("ReviveFall");
			ResetDeathFlagsAndStamina(playerEntity);
			RefreshAsset(playerEntity);
			AddBoost(playerEntity);
			playerEntity.isPlayerRevive = false;
		}

		private void RefreshAsset(GameEntity player)
		{
			string name = player.asset.name;
			player.RemoveAsset();
			player.AddAsset(name);
		}

		private void ResetDeathFlagsAndStamina(GameEntity player)
		{
			player.isDead = false;
			gameContext.playerEntity.isUnderTheScreen = false;
			RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
			if (player.stamina.Value < (float)restoreSettingsBalancingData.Map[RestoreSettings.ReviveMinStaminaAmount])
			{
				player.ReplaceStamina(restoreSettingsBalancingData.Map[RestoreSettings.ReviveMinStaminaAmount]);
			}
		}

		private void AddBoost(GameEntity player)
		{
			float num = SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration * 0.75f;
			float b = ((!player.hasInvulnerability) ? 0f : player.invulnerability.TimeLeft);
			float newTimeLeft = Mathf.Max(num, b);
			player.ReplaceInvulnerability(newTimeLeft);
			player.ReplaceOnClouds(num);
			if (!player.hasPlayerBlinking || player.playerBlinking.TimeLeft < num)
			{
				int numberOfCycles = SettingsService.GetSetting<SettingsVisualFeedbackSO>().NumberOfCycles;
				player.ReplacePlayerBlinking(num, num, numberOfCycles);
			}
		}
	}
}
