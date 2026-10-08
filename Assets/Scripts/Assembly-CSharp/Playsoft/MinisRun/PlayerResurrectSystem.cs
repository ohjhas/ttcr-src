using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerResurrectSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		public PlayerResurrectSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (playerEntity.hasPlayerResurrect)
			{
				playerEntity.playerResurrect.Delay -= Time.deltaTime;
				if (playerEntity.playerResurrect.Delay <= 0f)
				{
					DischargeResurrectAbility();
					PlayReviveAnimation();
					ChangePlayerPositionIfNeeded();
					ApplyJumpForce();
					gameContext.worldEntity.ReplaceWaitToStart(0.25f);
					ResetDeathFlagsAndStamina(playerEntity);
					AddBoost(playerEntity);
					playerEntity.RemovePlayerResurrect();
				}
			}
		}

		private void PlayReviveAnimation()
		{
			Animator component = gameContext.playerEntity.view.gameObject.GetComponent<Animator>();
			component.SetTrigger("Revive");
		}

		private void ChangePlayerPositionIfNeeded()
		{
			GameObject gameObject = gameContext.playerEntity.view.gameObject;
			if (gameObject.transform.localPosition.y <= SettingsService.SettingsSO.CloudsYPosition)
			{
				gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x, SettingsService.SettingsSO.PlayerCameraOffset.y, gameObject.transform.localPosition.z);
			}
		}

		private void ApplyJumpForce()
		{
			GameObject gameObject = gameContext.playerEntity.view.gameObject;
			float num = SettingsService.SettingsSO.JumpForce * 1.25f;
			Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
			component.velocity = new Vector2(0f, 0f);
			component.AddForce(gameObject.transform.up * num, ForceMode2D.Impulse);
		}

		private void ResetDeathFlagsAndStamina(GameEntity player)
		{
			player.isDead = false;
			player.isUnderTheScreen = false;
			float num = SettingsService.SettingsSO.StartingStamina * GetStaminaFactor();
			if (num <= 0f)
			{
				RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
				num = restoreSettingsBalancingData.Map[RestoreSettings.ResurectStaminaAmount];
			}
			player.ReplaceStamina(num);
		}

		private float GetStaminaFactor()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			GameEntity companionEntity = gameContext.companionEntity;
			return playerEntity.hasAbilityResurrect ? playerEntity.abilityResurrect.StaminaFactor : ((companionEntity == null || !companionEntity.hasAbilityResurrect) ? 0f : companionEntity.abilityResurrect.StaminaFactor);
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

		private void DischargeResurrectAbility()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			GameEntity companionEntity = gameContext.companionEntity;
			if (playerEntity.hasAbilityResurrect && playerEntity.abilityChargingProgress.PercentCharged > 0f)
			{
				playerEntity.ReplaceAbilityChargingProgress(0f, 1f);
			}
			else if (companionEntity != null && companionEntity.hasAbilityResurrect && companionEntity.abilityChargingProgress.PercentCharged > 0f)
			{
				companionEntity.ReplaceAbilityChargingProgress(0f, 1f);
			}
		}
	}
}
