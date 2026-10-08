using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerAbilitySpeedupSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext _gameContext;

		private float _timer;

		private bool _isActive;

		public PlayerAbilitySpeedupSystem(Contexts contexts)
		{
			_gameContext = contexts.game;
			_isActive = false;
			_timer = 0f;
		}

		public void Execute()
		{
			if (!CanExecute())
			{
				return;
			}
			_timer += Time.deltaTime;
			if (_isActive)
			{
				if (_timer >= SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration)
				{
					_isActive = false;
					_gameContext.playerEntity.ReplaceAbilityChargingProgress(0f, 0f);
					_timer = 0f;
					_gameContext.playerEntity.isAbilityRun = false;
				}
				else
				{
					_gameContext.playerEntity.ReplaceAbilityChargingProgress(1f, SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration - _timer);
				}
				return;
			}
			float num = _timer / _gameContext.playerEntity.abilityChargingTime.ChargingTime;
			_gameContext.playerEntity.ReplaceAbilityChargingProgress(Mathf.Clamp01(num), (!(num < 1f)) ? SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration : 0f);
			if (_timer >= _gameContext.playerEntity.abilityChargingTime.ChargingTime)
			{
				GameEntity playerEntity = _gameContext.playerEntity;
				float speedUpDuration = SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration;
				float b = ((!playerEntity.hasInvulnerability) ? 0f : playerEntity.invulnerability.TimeLeft);
				float newTimeLeft = Mathf.Max(speedUpDuration, b);
				playerEntity.ReplaceSpeedUp(speedUpDuration, playerEntity.abilitySpeedup.Multiplier);
				playerEntity.ReplaceInvulnerability(newTimeLeft);
				playerEntity.ReplaceOnClouds(speedUpDuration);
				_isActive = true;
				_gameContext.playerEntity.isAbilityRun = true;
				_timer = 0f;
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.hasAbilitySpeedup && !playerEntity.isDead;
		}
	}
}
