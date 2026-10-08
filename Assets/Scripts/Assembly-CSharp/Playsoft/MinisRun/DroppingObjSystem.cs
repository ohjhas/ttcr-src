using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class DroppingObjSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private IGroup<GameEntity> droppingObjects;

		public DroppingObjSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			droppingObjects = gameContext.GetGroup(GameMatcher.Dropping);
		}

		public void Execute()
		{
			GameEntity[] entities = droppingObjects.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView && gameEntity.hasDropping && gameEntity.dropping.Activated && !gameEntity.isFlyAway)
				{
					gameEntity.ReplaceDropping(true, gameEntity.dropping.Delay - Time.smoothDeltaTime);
					if (gameEntity.dropping.Delay <= 0f)
					{
						Transform transform = gameEntity.view.gameObject.transform;
						float delay = gameEntity.dropping.Delay;
						transform.position = gameEntity.position.Value + (Vector3)Physics2D.gravity * delay * delay * 0.5f;
					}
				}
			}
		}
	}
}
