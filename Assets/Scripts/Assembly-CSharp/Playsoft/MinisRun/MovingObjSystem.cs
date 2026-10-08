using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class MovingObjSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private IGroup<GameEntity> movingObjects;

		public MovingObjSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			movingObjects = gameContext.GetGroup(GameMatcher.MovableData);
		}

		public void Execute()
		{
			GameEntity[] entities = movingObjects.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView && !gameEntity.isFlyAway && gameContext.playerEntity.hasView)
				{
					Transform transform = gameEntity.view.gameObject.transform;
					Transform transform2 = gameContext.playerEntity.view.gameObject.transform;
					float num = transform.position.x - transform2.position.x;
					if (num <= gameEntity.movableData.StartThreshold)
					{
						Vector3 vector = gameEntity.position.Value + (Vector3)gameEntity.movableData.MoveVector;
						float t = (gameEntity.movableData.StartThreshold - num) / (gameEntity.movableData.StartThreshold - gameEntity.movableData.EndThreshold);
						transform.position = ((!(num < gameEntity.movableData.EndThreshold)) ? Vector3.Lerp(gameEntity.position.Value, vector, t) : vector);
					}
				}
			}
		}
	}
}
