using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class RestableAreaMoveSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		public RestableAreaMoveSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Execute()
		{
			if (IsPlayerAvailable() && gameContext.playerEntity.hasResting)
			{
				GameEntity gameEntity = gameContext.playerEntity.resting.restableArea as GameEntity;
				Vector3 position = gameContext.playerEntity.view.gameObject.transform.position;
				Vector3 value = gameEntity.position.Value;
				Vector3 b = value + (Vector3)gameEntity.restableArea.MoveVector;
				float num = (position.x - value.x) / (b.x - value.x);
				gameEntity.view.gameObject.transform.position = Vector3.Lerp(value, b, num);
				if (num >= 1f)
				{
					gameEntity.ReplaceRestableArea(gameEntity.restableArea.MoveVector, true);
					gameContext.playerEntity.RemoveResting();
				}
			}
		}

		private bool IsPlayerAvailable()
		{
			return gameContext.playerEntity != null && gameContext.playerEntity.hasView && !gameContext.playerEntity.isDead;
		}
	}
}
