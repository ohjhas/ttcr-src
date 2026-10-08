using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public sealed class AddPoolableViewSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ISystem
{
	private readonly Transform poolableContainer = new GameObject("Pool").transform;

	private readonly GameContext gameContext;

	public AddPoolableViewSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
	}

	public void Initialize()
	{
		GameEntity gameEntity = gameContext.CreateEntity();
		gameEntity.AddPoolContainer(new Dictionary<string, Stack<GameObject>>());
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Poolable.Added());
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasAsset;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity entity in entities)
		{
			GameObject fromPool = GetFromPool(entity, entity.isLoadAssetAsync);
			if (fromPool != null)
			{
				entity.AddView(fromPool);
				entity.isResetPoolable = true;
				fromPool.transform.SetParent(poolableContainer, false);
				fromPool.Link(entity, gameContext);
			}
		}
	}

	private GameObject GetFromPool(GameEntity e, bool isLoadAsync)
	{
		Dictionary<string, Stack<GameObject>> pools = gameContext.poolContainer.Pools;
		if (pools.ContainsKey(e.asset.name))
		{
			Stack<GameObject> stack = pools[e.asset.name];
			if (stack.Count > 0)
			{
				GameObject gameObject = stack.Pop();
				gameObject.SetActive(true);
				return gameObject;
			}
		}
		if (isLoadAsync)
		{
			ResourceRequest assetRequest = Resources.LoadAsync<GameObject>(e.asset.name);
			GameEntity gameEntity = gameContext.CreateEntity();
			gameEntity.AddAsyncRequest(assetRequest, delegate
			{
				InstantiateObject(e, (GameObject)assetRequest.asset);
			});
			return null;
		}
		GameObject original = Resources.Load<GameObject>(e.asset.name);
		return UnityEngine.Object.Instantiate(original);
	}

	private void InstantiateObject(GameEntity e, GameObject asset)
	{
		GameObject gameObject = null;
		try
		{
			gameObject = UnityEngine.Object.Instantiate(asset);
		}
		catch (Exception)
		{
			Debug.Log("Cannot instantiate " + e.asset.name);
		}
		if (gameObject != null)
		{
			e.AddView(gameObject);
			e.isResetPoolable = true;
			gameObject.transform.SetParent(poolableContainer, false);
			gameObject.Link(e, gameContext);
		}
	}
}
