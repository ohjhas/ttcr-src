using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class RestableAreaHitSystem : ReactiveSystem<GameEntity>
	{
		public RestableAreaHitSystem(Contexts contexts)
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
					if (gameEntity.hasRestableArea && gameEntity.restableArea.MoveVector != Vector2.zero && !entity.hasResting)
					{
						entity.AddResting(gameEntity);
					}
				}
			}
		}
	}
}
