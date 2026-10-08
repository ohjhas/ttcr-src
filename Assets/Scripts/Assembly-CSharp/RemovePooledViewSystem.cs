using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public sealed class RemovePooledViewSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	public RemovePooledViewSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Destroyed);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasView && entity.isPoolable;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			if (ShouldBeStored(entity))
			{
				PushIntoPool(entity);
				continue;
			}
			destroyView(entity.view);
			entity.RemoveView();
			entity.isPoolable = false;
		}
	}

	private bool ShouldBeStored(GameEntity e)
	{
		string name = e.asset.name;
		if (name.Contains("_"))
		{
			string value = ((!gameContext.worldEntity.hasNewWorldSkin) ? gameContext.worldEntity.missionData.MissionData.Worldskin : gameContext.worldEntity.newWorldSkin.WorldSuffix);
			string text = name.Remove(0, name.LastIndexOf('_'));
			return text.Equals(value);
		}
		return true;
	}

	private void PushIntoPool(GameEntity e)
	{
		Dictionary<string, Stack<GameObject>> pools = gameContext.poolContainer.Pools;
		string name = e.asset.name;
		GameObject gameObject = e.view.gameObject;
		if (!pools.ContainsKey(name))
		{
			pools.Add(name, new Stack<GameObject>());
		}
		Stack<GameObject> stack = pools[name];
		stack.Push(gameObject);
		IPoolDespawnedListener component = gameObject.GetComponent<IPoolDespawnedListener>();
		if (component != null)
		{
			component.OnDespawned(e);
		}
		gameObject.SetActive(false);
		if (gameObject.GetComponent<EntityLink>() != null)
		{
			gameObject.Unlink();
		}
	}

	private void destroyView(ViewComponent viewComponent)
	{
		GameObject gameObject = viewComponent.gameObject;
		gameObject.Unlink();
		Object.Destroy(gameObject);
	}
}
