using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public sealed class AddViewSystem : ReactiveSystem<GameEntity>
{
	private readonly Transform viewContainer = new GameObject("Views").transform;

	private readonly Transform movableContainer = new GameObject("Movables").transform;

	private readonly Transform CameraTransform = Camera.main.transform.parent;

	private readonly GameContext _context;

	public AddViewSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		_context = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Asset.Added());
	}

	protected override bool Filter(GameEntity entity)
	{
		return !entity.isPoolable;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		foreach (GameEntity e in entities)
		{
			if (e.isLoadAssetAsync)
			{
				ResourceRequest assetRequest = Resources.LoadAsync<GameObject>(e.asset.name);
				GameEntity gameEntity = _context.CreateEntity();
				gameEntity.AddAsyncRequest(assetRequest, delegate
				{
					InstantiateObject(e, (GameObject)assetRequest.asset);
				});
			}
			else
			{
				GameObject asset = Resources.Load<GameObject>(e.asset.name);
				InstantiateObject(e, asset);
			}
		}
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
			if (e.isCameraAttached)
			{
				gameObject.transform.SetParent(CameraTransform, false);
			}
			else if (e.isMovable)
			{
				gameObject.transform.SetParent(movableContainer, false);
			}
			else
			{
				gameObject.transform.SetParent(viewContainer, false);
			}
			e.AddView(gameObject);
			gameObject.Link(e, _context);
		}
	}
}
