using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LevelEndReachedSystem : ReactiveSystem<InputEntity>
	{
		private GameContext gameContext;

		private IGroup<GameEntity> levelFinishedListeners;

		public LevelEndReachedSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
			levelFinishedListeners = gameContext.GetGroup(GameMatcher.LevelFinishedListener);
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.hasCollision && entity.collision.to != null && entity.collision.from != null;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.Collision);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			foreach (InputEntity entity in entities)
			{
				GameEntity gameEntity = entity.collision.from as GameEntity;
				GameEntity gameEntity2 = entity.collision.to as GameEntity;
				if (gameEntity.isPlayer && !gameEntity.hasLevelFinished && gameEntity2.hasLevelEnd)
				{
					gameEntity.AddLevelFinished(gameEntity2.levelEnd.DistanceToEnd);
					InformUI();
				}
			}
		}

		private void InformUI()
		{
			GameEntity[] entities = levelFinishedListeners.GetEntities();
			foreach (GameEntity gameEntity in entities)
			{
				gameEntity.levelFinishedListener.listener.OnLevelFinished();
			}
		}
	}
}
