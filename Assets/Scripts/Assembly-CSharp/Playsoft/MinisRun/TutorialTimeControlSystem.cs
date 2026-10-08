using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class TutorialTimeControlSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private StateEntity tutorialData;

		private float lastTime;

		private float lastDistance;

		private bool isStopingTime;

		private TutorialSettingsSO tutorialSettings;

		public TutorialTimeControlSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			tutorialSettings = SettingsService.GetSetting<TutorialSettingsSO>();
		}

		public void Execute()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (playerEntity.hasTutorialPlayStep && playerEntity.tutorialPlayStep.StopTime)
			{
				float num = playerEntity.tutorialPlayStep.EndPosX + SettingsService.GetSetting<TutorialSettingsSO>().SlowDownMargin;
				if (playerEntity.view.gameObject.transform.position.x <= num)
				{
					lastDistance = gameContext.worldEntity.world.Distance;
				}
			}
			if (ShouldStopTime())
			{
				isStopingTime = true;
			}
			if (isStopingTime)
			{
				SlowTime();
			}
			lastTime = gameContext.worldEntity.worldTimeScale.scale;
			lastDistance = gameContext.worldEntity.world.Distance;
		}

		private void SlowTime()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (gameContext.worldEntity.worldTimeScale.scale > lastTime)
			{
				isStopingTime = false;
				return;
			}
			if (playerEntity.hasTutorialPlayStep && playerEntity.tutorialPlayStep.StopTime)
			{
				float num = playerEntity.tutorialPlayStep.EndPosX + SettingsService.GetSetting<TutorialSettingsSO>().SlowDownMargin;
				float time = Mathf.InverseLerp(playerEntity.tutorialPlayStep.StartPosX + SettingsService.GetSetting<TutorialSettingsSO>().SlowDownThresholdOffset, num, playerEntity.view.gameObject.transform.position.x);
				if (playerEntity.view.gameObject.transform.position.x > num && !playerEntity.isSliding)
				{
					gameContext.worldEntity.world.Distance = lastDistance;
				}
				gameContext.worldEntity.ReplaceWorldTimeScale(Mathf.Lerp(1f, 0f, tutorialSettings.SlowDownCurve.Evaluate(time)));
			}
			else
			{
				gameContext.worldEntity.ReplaceWorldTimeScale(1f);
				isStopingTime = false;
			}
			if (playerEntity.hasJumping && playerEntity.jumping.Quantity > 0)
			{
				gameContext.worldEntity.ReplaceWorldTimeScale(1f);
				isStopingTime = false;
			}
			if (gameContext.worldEntity.worldTimeScale.scale < 0.01f)
			{
				gameContext.worldEntity.ReplaceWorldTimeScale(0f);
				isStopingTime = false;
			}
		}

		private bool ShouldStopTime()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (isStopingTime)
			{
				return false;
			}
			if (gameContext.worldEntity.isPaused)
			{
				return false;
			}
			if (gameContext.worldEntity.worldTimeScale.scale < 0.01f)
			{
				return false;
			}
			if (!playerEntity.hasView || playerEntity.isDead)
			{
				return false;
			}
			if (!playerEntity.hasTutorialPlayStep || !playerEntity.tutorialPlayStep.StopTime)
			{
				return false;
			}
			if (playerEntity.hasJumping && playerEntity.jumping.Quantity > 0)
			{
				return false;
			}
			return true;
		}
	}
}
