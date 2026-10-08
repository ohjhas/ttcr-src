using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerKeepPositionSystem : IExecuteSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly Transform MovingTransform;

		public PlayerKeepPositionSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			MovingTransform = Camera.main.transform.parent;
		}

		public void Execute()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			float num = worldEntity.world.Distance - MovingTransform.position.x;
			if (!Mathf.Approximately(num, 0f) && IsPlayerNeedAdjustement())
			{
				Transform transform = gameContext.playerEntity.view.gameObject.transform;
				transform.localPosition = new Vector3(Mathf.Max(SettingsService.SettingsSO.PlayerCameraOffset.x, transform.localPosition.x - num * 0.5f), transform.localPosition.y, transform.localPosition.z);
			}
		}

		private bool IsPlayerNeedAdjustement()
		{
			return gameContext.playerEntity != null && !gameContext.playerEntity.isDead && gameContext.playerEntity.hasView && gameContext.playerEntity.hasRaccoonIntro && gameContext.playerEntity.raccoonIntro.Step == RaccoonIntroStep.HUDRevealed && gameContext.worldEntity != null && !gameContext.worldEntity.isPaused && !gameContext.playerEntity.hasLevelFinished && gameContext.playerEntity.view.gameObject.transform.localPosition.x > SettingsService.SettingsSO.PlayerCameraOffset.x;
		}
	}
}
