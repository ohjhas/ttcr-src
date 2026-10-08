using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class WorldSlowDownTimeSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private float initialScaleTime
		{
			get
			{
				return SettingsService.SettingsSO.BaseSpeed;
			}
		}

		public WorldSlowDownTimeSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (!CanExecute())
			{
				return;
			}
			GameEntity worldEntity = gameContext.worldEntity;
			worldEntity.changeScaleTime.timer += Time.unscaledDeltaTime;
			if (worldEntity.changeScaleTime.timer <= worldEntity.changeScaleTime.transitionDuration)
			{
				float t = worldEntity.changeScaleTime.timer / worldEntity.changeScaleTime.transitionDuration;
				worldEntity.ReplaceWorldTimeScale(Mathf.Lerp(worldEntity.initialTimeScale.initialScaleTime, worldEntity.changeScaleTime.finalTimeScale, t));
			}
			else if (worldEntity.changeScaleTime.timer > worldEntity.changeScaleTime.slowDownDuration + worldEntity.changeScaleTime.transitionDuration)
			{
				float num = worldEntity.changeScaleTime.timer - (worldEntity.changeScaleTime.slowDownDuration + worldEntity.changeScaleTime.transitionDuration);
				float num2 = num / worldEntity.changeScaleTime.transitionDuration;
				worldEntity.ReplaceWorldTimeScale(Mathf.Lerp(worldEntity.changeScaleTime.finalTimeScale, worldEntity.initialTimeScale.initialScaleTime, num2));
				if (num2 >= 1f)
				{
					worldEntity.RemoveChangeScaleTime();
				}
			}
		}

		private bool CanExecute()
		{
			return gameContext.worldEntity != null && gameContext.worldEntity.hasChangeScaleTime && !gameContext.worldEntity.isPaused;
		}
	}
}
