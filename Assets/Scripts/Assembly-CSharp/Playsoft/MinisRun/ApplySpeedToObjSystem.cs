using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ApplySpeedToObjSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private IGroup<GameEntity> movingObjects;

		public ApplySpeedToObjSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			movingObjects = gameContext.GetGroup(GameMatcher.SpeedData);
		}

		public void Execute()
		{
			GameEntity[] entities = movingObjects.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView && CanObjectMove(gameEntity))
				{
					gameEntity.view.gameObject.transform.localPosition += Vector3.right * gameEntity.speedData.Speed * Time.smoothDeltaTime;
				}
			}
		}

		private bool CanObjectMove(GameEntity entity)
		{
			return (!entity.hasDropping || !entity.dropping.Activated) && !entity.isFlyAway;
		}
	}
}
