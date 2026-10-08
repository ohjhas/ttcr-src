using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerAbilityGiantSpeedupSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext _gameContext;

		private readonly InputContext inputContext;

		private float _timer;

		private bool _isActive;

		public PlayerAbilityGiantSpeedupSystem(Contexts contexts)
		{
			_gameContext = contexts.game;
			inputContext = contexts.input;
			_isActive = false;
			_timer = 0f;
		}

		public void Execute()
		{
			if (!CanExecute())
			{
				return;
			}
			GameEntity playerEntity = _gameContext.playerEntity;
			SettingsPowerupsSO setting = SettingsService.GetSetting<SettingsPowerupsSO>();
			_timer += Time.deltaTime;
			if (_isActive)
			{
				if (_timer >= setting.SpeedUpDuration)
				{
					_isActive = false;
					playerEntity.ReplaceAbilityChargingProgress(0f, 0f);
					_timer = 0f;
					playerEntity.isAbilityRun = false;
				}
				else
				{
					playerEntity.ReplaceAbilityChargingProgress(1f, setting.SpeedUpDuration - _timer);
				}
				return;
			}
			float num = _timer / playerEntity.abilityChargingTime.ChargingTime;
			playerEntity.ReplaceAbilityChargingProgress(Mathf.Clamp01(num), (!(num < 1f)) ? setting.SpeedUpDuration : 0f);
			if (_timer >= playerEntity.abilityChargingTime.ChargingTime)
			{
				float speedUpDuration = setting.SpeedUpDuration;
				float b = ((!playerEntity.hasInvulnerability) ? 0f : playerEntity.invulnerability.TimeLeft);
				float newTimeLeft = Mathf.Max(speedUpDuration, b);
				playerEntity.ReplaceInvulnerability(newTimeLeft);
				_isActive = true;
				playerEntity.isAbilityRun = true;
				_timer = 0f;
				playerEntity.ReplaceSizeIncreased(speedUpDuration);
				playerEntity.ReplaceSpeedUp(speedUpDuration, playerEntity.abilityGiantSpeedup.Factor);
				playerEntity.ReplaceOnClouds(speedUpDuration);
				Jump(playerEntity);
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = _gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.hasAbilityGiantSpeedup && !playerEntity.isDead;
		}

		private void Jump(GameEntity player)
		{
			GameObject gameObject = player.view.gameObject;
			float jumpForce = SettingsService.SettingsSO.JumpForce;
			Rigidbody2D component = gameObject.GetComponent<Rigidbody2D>();
			component.velocity = new Vector2(Mathf.Max(component.velocity.x * 0.37f, 0f), Mathf.Max(component.velocity.y * 0.37f, 0f));
			component.AddForce(gameObject.transform.up * jumpForce, ForceMode2D.Impulse);
			InputEntity inputEntity = inputContext.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isInputJumpPerformed = true;
		}
	}
}
