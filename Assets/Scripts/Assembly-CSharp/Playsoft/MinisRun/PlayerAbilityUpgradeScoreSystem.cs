using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerAbilityUpgradeScoreSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext _gameContext;

		private float _timer;

		private bool _isActive;

		public PlayerAbilityUpgradeScoreSystem(Contexts contexts)
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
				if (!_gameContext.playerEntity.hasUpgradeSingleScore || _gameContext.playerEntity.upgradeSingleScore.Duration <= 0f)
				{
					_isActive = false;
					_gameContext.playerEntity.ReplaceAbilityChargingProgress(0f, 0f);
					_timer = 0f;
					_gameContext.playerEntity.isAbilityRun = false;
				}
				else
				{
					_gameContext.playerEntity.ReplaceAbilityChargingProgress(1f, SettingsService.GetSetting<SettingsPowerupsSO>().UpgradeScoreDuration - _timer);
				}
				return;
			}
			float num = _timer / _gameContext.playerEntity.abilityChargingTime.ChargingTime;
			_gameContext.playerEntity.ReplaceAbilityChargingProgress(Mathf.Clamp01(num), (!(num < 1f)) ? _gameContext.playerEntity.abilityDuration.Duration : 0f);
			if (_timer >= _gameContext.playerEntity.abilityChargingTime.ChargingTime)
			{
				float duration = _gameContext.playerEntity.abilityDuration.Duration;
				float distance = _gameContext.playerEntity.abilityUpgradeScore.Distance;
				_gameContext.playerEntity.ReplaceUpgradeSingleScore(duration, distance);
				_isActive = true;
				_gameContext.playerEntity.isAbilityRun = true;
				_timer = 0f;
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.hasAbilityUpgradeScore && !playerEntity.isDead;
		}
	}
}
