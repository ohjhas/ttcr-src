using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class SpeedupPowerupSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		public SpeedupPowerupSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (CanExecute())
			{
				float num = CalculateCurrentSpeed();
				SetParticlesState(num);
				SetPlayerSpeed(num);
				CalculateRemainingTime();
			}
			else if (gameContext.playerEntity.hasView)
			{
				SetParticlesState(1f);
			}
			if (gameContext.playerEntity.hasSpeedUp && IsLevelFinished())
			{
				RemoveSpeedUp();
			}
		}

		private bool CanExecute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			return playerEntity.hasView && playerEntity.hasSpeedUp && !playerEntity.isDead && !playerEntity.hasLevelFinished;
		}

		private bool IsLevelFinished()
		{
			return gameContext.playerEntity.hasLevelFinished;
		}

		private void SetParticlesState(float currentSpeed)
		{
			ParticleSystem particleModule = GetParticleModule();
			if (particleModule != null)
			{
				Transform transform = gameContext.playerEntity.view.gameObject.transform;
				ParticleSystem.EmissionModule emission = particleModule.emission;
				emission.enabled = currentSpeed != 1f;
				emission.rateOverTime = 30f * transform.localScale.y;
				ParticleSystem.ShapeModule shape = particleModule.shape;
				float num = ((!gameContext.playerEntity.isSliding) ? 1.3f : 1f);
				float x = ((!gameContext.playerEntity.isSliding) ? 0f : (-0.3f));
				shape.radius = num * transform.localScale.y;
				shape.position = new Vector3(x, shape.position.y, shape.position.z);
			}
		}

		private ParticleSystem GetParticleModule()
		{
			GameObject gameObject = gameContext.playerEntity.view.gameObject;
			ParticleSystem[] componentsInChildren = gameObject.transform.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				if (componentsInChildren[i].name.Equals("boost"))
				{
					if (!componentsInChildren[i].isPlaying)
					{
						componentsInChildren[i].Play();
					}
					return componentsInChildren[i];
				}
			}
			return null;
		}

		private float CalculateCurrentSpeed()
		{
			float speedUpDuration = SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration;
			float duration = gameContext.playerEntity.speedUp.Duration;
			float num = speedUpDuration - duration;
			float num2 = num / speedUpDuration;
			float num3 = SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpCurve.Evaluate(num2) * gameContext.playerEntity.speedUp.Factor;
			num3 += 1f;
			return (!(num2 > 0.5f)) ? Mathf.Max(num3, gameContext.world.Speed / SettingsService.SettingsSO.BaseSpeed) : num3;
		}

		private void SetPlayerSpeed(float currentSpeed)
		{
			gameContext.world.Speed = SettingsService.SettingsSO.BaseSpeed * currentSpeed;
		}

		private void CalculateRemainingTime()
		{
			gameContext.playerEntity.speedUp.Duration -= Time.deltaTime;
			if (gameContext.playerEntity.speedUp.Duration <= 0f)
			{
				RemoveSpeedUp();
			}
		}

		private void RemoveSpeedUp()
		{
			gameContext.playerEntity.RemoveSpeedUp();
			SetPlayerSpeed(1f);
			SetParticlesState(1f);
		}
	}
}
