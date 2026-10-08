using System.Collections.Generic;
using Entitas;

public sealed class CollisionCameraSizeSystem : ReactiveSystem<InputEntity>
{
	private readonly InputContext inputContext;

	public CollisionCameraSizeSystem(Contexts contexts)
		: base((IContext<InputEntity>)contexts.input)
	{
		inputContext = contexts.input;
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
			if (gameEntity.isPlayer && gameEntity2.hasChangeCameraSizeTrigger)
			{
				InputEntity inputEntity = inputContext.CreateEntity();
				inputEntity.isInput = true;
				inputEntity.AddChangeViewSize(gameEntity2.changeCameraSizeTrigger.Size);
			}
		}
	}
}
