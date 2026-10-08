using System.Collections.Generic;
using Entitas;

public sealed class SetViewPositionSystem : ReactiveSystem<GameEntity>
{
	public SetViewPositionSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.View);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasView && entity.hasPosition;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			entity.view.gameObject.transform.position = entity.position.Value;
		}
	}
}
