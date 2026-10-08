using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class UnderMapObjSystem : IExecuteSystem, ISystem
	{
		private GameContext gameContext;

		private IGroup<GameEntity> flyingAwayObjects;

		public UnderMapObjSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			flyingAwayObjects = gameContext.GetGroup(GameMatcher.AnyOf(GameMatcher.FlyAway, GameMatcher.Dropping));
		}

		public void Execute()
		{
			GameEntity[] entities = flyingAwayObjects.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				if (gameEntity.hasView)
				{
					Rigidbody2D componentInChildren = gameEntity.view.gameObject.GetComponentInChildren<Rigidbody2D>();
					if (componentInChildren.transform.position.y <= SettingsService.SettingsSO.MinPositionY)
					{
						gameEntity.isDestroyed = true;
					}
				}
			}
		}
	}
}
