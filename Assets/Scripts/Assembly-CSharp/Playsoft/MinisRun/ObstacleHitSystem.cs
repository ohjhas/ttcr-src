using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ObstacleHitSystem : ReactiveSystem<InputEntity>
	{
		public ObstacleHitSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
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
				if (gameEntity.isPlayer && gameEntity2.isObstacle && !gameEntity2.isKillZone && (gameEntity.hasSizeIncreased || gameEntity.hasSpeedUp) && !gameEntity2.isFlyAway)
				{
					gameEntity2.isFlyAway = true;
				}
			}
		}
	}
}
