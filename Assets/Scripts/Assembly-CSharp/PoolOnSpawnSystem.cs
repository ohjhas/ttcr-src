using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class PoolOnSpawnSystem : ReactiveSystem<GameEntity>
{
	public PoolOnSpawnSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Poolable.Added());
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasView;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			GameObject gameObject = entity.view.gameObject;
			IPoolSpawnedListener[] components = gameObject.GetComponents<IPoolSpawnedListener>();
			IPoolSpawnedListener[] array = components;
			foreach (IPoolSpawnedListener poolSpawnedListener in array)
			{
				if (poolSpawnedListener != null)
				{
					poolSpawnedListener.OnSpawned(entity);
				}
			}
		}
	}
}
