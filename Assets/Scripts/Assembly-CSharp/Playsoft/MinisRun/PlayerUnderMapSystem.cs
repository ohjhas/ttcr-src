using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PlayerUnderMapSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private InputContext inputContext;

		public PlayerUnderMapSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			inputContext = contexts.input;
		}

		public void Execute()
		{
			if (gameContext.playerEntity != null && gameContext.playerEntity.hasView && !gameContext.playerEntity.isDead)
			{
				Transform transform = gameContext.playerEntity.view.gameObject.transform;
				if (transform.position.y < SettingsService.SettingsSO.MinPositionY)
				{
					gameContext.playerEntity.isDead = true;
					gameContext.playerEntity.isUnderTheScreen = true;
					InputEntity inputEntity = inputContext.CreateEntity();
					inputEntity.isInput = true;
					inputEntity.ReplaceCollisionFeedback(true);
				}
			}
		}
	}
}
