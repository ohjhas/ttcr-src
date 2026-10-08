using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class MovePositionSystem : ReactiveSystem<GameEntity>
{
	public MovePositionSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.View));
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasPosition && entity.hasView;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			Vector3 translation = entity.position.Value - entity.view.gameObject.transform.position;
			entity.view.gameObject.transform.Translate(translation, Space.World);
		}
	}
}
