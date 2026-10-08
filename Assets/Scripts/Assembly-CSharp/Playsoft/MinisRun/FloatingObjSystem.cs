using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class FloatingObjSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private IGroup<GameEntity> floatingObjects;

		private float currentTime;

		public FloatingObjSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			floatingObjects = gameContext.GetGroup(GameMatcher.Floating);
			currentTime = 0f;
		}

		public void Execute()
		{
			currentTime += Time.smoothDeltaTime;
			GameEntity[] entities = floatingObjects.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView && CanObjectFloat(gameEntity))
				{
					float num = ((!gameEntity.floating.HasLocalTimer) ? currentTime : gameEntity.floating.CurrentTime);
					float y = gameEntity.position.Value.y + Mathf.Sin(num * gameEntity.floating.Speed + gameEntity.floating.Offset) * gameEntity.floating.Amplitude;
					gameEntity.floating.CurrentTime += Time.smoothDeltaTime;
					Transform transform = gameEntity.view.gameObject.transform;
					transform.localPosition = new Vector3(transform.localPosition.x, y, transform.localPosition.z);
				}
			}
		}

		private bool CanObjectFloat(GameEntity entity)
		{
			return (!entity.hasDropping || !entity.dropping.Activated) && !entity.isFlyAway;
		}
	}
}
