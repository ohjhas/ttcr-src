using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class SwingingObjSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private IGroup<GameEntity> swingingObjects;

		public SwingingObjSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			swingingObjects = gameContext.GetGroup(GameMatcher.SwingingData);
		}

		public void Execute()
		{
			GameEntity[] entities = swingingObjects.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView && !gameEntity.isFlyAway && gameContext.playerEntity.hasView)
				{
					Quaternion quaternion = Quaternion.Euler(0f, 0f, gameEntity.swingingData.StartEndRotation.x);
					Quaternion quaternion2 = Quaternion.Euler(0f, 0f, gameEntity.swingingData.StartEndRotation.y);
					float num = CalculateAnimFactor(gameEntity, quaternion, quaternion2);
					if (!gameContext.playerEntity.isDead)
					{
						LerpToFinalPosition(gameEntity, quaternion, quaternion2);
						SaveFactorDifference(gameEntity, quaternion, quaternion2, num);
					}
					else
					{
						float t = num + gameEntity.swingingData.AverageSpeed;
						gameEntity.view.gameObject.transform.rotation = Quaternion.Lerp(quaternion, quaternion2, t);
					}
				}
			}
		}

		private float CalculateAnimFactor(GameEntity e, Quaternion startRotation, Quaternion endRotation)
		{
			return Quaternion.Angle(startRotation, e.view.gameObject.transform.rotation) / Quaternion.Angle(startRotation, endRotation);
		}

		private void LerpToFinalPosition(GameEntity e, Quaternion startRotation, Quaternion endRotation)
		{
			Transform transform = gameContext.playerEntity.view.gameObject.transform;
			float num = e.view.gameObject.transform.position.x - transform.position.x;
			float num2 = (e.swingingData.StartThreshold - e.swingingData.EndThreshold) * 0.5f;
			float t = Mathf.Clamp01((num2 - num) / num2);
			e.view.gameObject.transform.rotation = Quaternion.Lerp(startRotation, endRotation, t);
		}

		private void SaveFactorDifference(GameEntity e, Quaternion startRotation, Quaternion endRotation, float lastAnimFactor)
		{
			float num = CalculateAnimFactor(e, startRotation, endRotation);
			float num2 = num - lastAnimFactor;
			float newAverageSpeed = e.swingingData.AverageSpeed + (num2 - e.swingingData.AverageSpeed) * 0.2f;
			e.ReplaceSwingingData(e.swingingData.Anchor, e.swingingData.StartEndRotation, e.swingingData.StartThreshold, e.swingingData.EndThreshold, newAverageSpeed);
		}
	}
}
