using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public sealed class RemoveViewSystem : ReactiveSystem<GameEntity>
{
	public RemoveViewSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Destroyed);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasView && !entity.isPoolable;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			destroyView(entity.view);
			entity.RemoveView();
		}
	}

	private void destroyView(ViewComponent viewComponent)
	{
		GameObject gameObject = viewComponent.gameObject;
		gameObject.Unlink();
		Object.Destroy(gameObject);
	}
}
