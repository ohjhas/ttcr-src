using System.Collections.Generic;
using Entitas;

public sealed class CollectibleHitSystem : ReactiveSystem<InputEntity>
{
	public CollectibleHitSystem(Contexts contexts)
		: base((IContext<InputEntity>)contexts.input)
	{
	}

	protected override bool Filter(InputEntity entity)
	{
		return entity.hasCollision && entity.collision.to != null && ((GameEntity)entity.collision.to).isCollectible && entity.collision.from != null && ((GameEntity)entity.collision.from).isPlayer;
	}

	protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
	{
		return context.CreateCollector(InputMatcher.Collision);
	}

	protected override void Execute(List<InputEntity> entities)
	{
		foreach (InputEntity entity in entities)
		{
			GameEntity gameEntity = entity.collision.to as GameEntity;
			GameEntity gameEntity2 = entity.collision.from as GameEntity;
			gameEntity.isDestroyed = true;
			gameEntity.isCollected = true;
		}
	}
}
