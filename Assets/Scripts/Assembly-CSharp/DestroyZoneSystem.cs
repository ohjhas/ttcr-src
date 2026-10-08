using System.Collections.Generic;
using Entitas;

public sealed class DestroyZoneSystem : ReactiveSystem<InputEntity>, ICleanupSystem, ISystem
{
	private InputContext inputContext;

	private readonly IGroup<InputEntity> collisions;

	public DestroyZoneSystem(Contexts contexts)
		: base((IContext<InputEntity>)contexts.input)
	{
		inputContext = contexts.input;
		collisions = inputContext.GetGroup(InputMatcher.Collision);
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
			if (gameEntity != null && gameEntity.isDestroyZoned && gameEntity2 != null)
			{
				gameEntity2.isDestroyed = true;
			}
		}
	}

	public void Cleanup()
	{
		InputEntity[] entities = collisions.GetEntities();
		foreach (InputEntity inputEntity in entities)
		{
			inputEntity.Destroy();
		}
	}
}
