using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DroppingObjHitSystem : ReactiveSystem<GameEntity>
	{
		public DroppingObjHitSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Grounded.Added());
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				if (entity.hasGrounded && entity.hasView)
				{
					GameEntity gameEntity = entity.grounded.ground as GameEntity;
					if (gameEntity.hasDropping && !gameEntity.dropping.Activated)
					{
						gameEntity.ReplaceDropping(true, gameEntity.dropping.Delay);
					}
				}
			}
		}
	}
}
