using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerAbilityMagnetSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext _gameContext;

		private float _timer;

		private bool _isActive;

		public PlayerAbilityMagnetSystem(Contexts contexts)
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
			SettingsPowerupsSO setting = SettingsService.GetSetting<SettingsPowerupsSO>();
			if (_isActive)
			{
				if (_timer >= setting.MagnetDuration)
				{
					_isActive = false;
					_gameContext.playerEntity.ReplaceAbilityChargingProgress(0f, 0f);
					_timer = 0f;
					_gameContext.playerEntity.isAbilityRun = false;
				}
				else
				{
					_gameContext.playerEntity.ReplaceAbilityChargingProgress(1f, setting.MagnetDuration - _timer);
				}
				return;
			}
			float num = _timer / _gameContext.playerEntity.abilityChargingTime.ChargingTime;
			_gameContext.playerEntity.ReplaceAbilityChargingProgress(Mathf.Clamp01(num), (!(num < 1f)) ? setting.MagnetDuration : 0f);
			if (_timer >= _gameContext.playerEntity.abilityChargingTime.ChargingTime)
			{
				float magnetDuration = setting.MagnetDuration;
				_gameContext.playerEntity.ReplaceMagnetized(magnetDuration);
				_isActive = true;
				_gameContext.playerEntity.isAbilityRun = true;
				_timer = 0f;
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.isAbilityMagnetized && !playerEntity.isDead;
		}
	}
}
