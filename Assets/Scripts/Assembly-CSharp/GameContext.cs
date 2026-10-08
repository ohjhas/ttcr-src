using System;
using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class GameContext : Context<GameEntity>
{
	public GameEntity companionEntity
	{
		get
		{
			return GetGroup(GameMatcher.Companion).GetSingleEntity();
		}
	}

	public bool isCompanion
	{
		get
		{
			return companionEntity != null;
		}
		set
		{
			GameEntity gameEntity = companionEntity;
			if (value != (gameEntity != null))
			{
				if (value)
				{
					CreateEntity().isCompanion = true;
				}
				else
				{
					gameEntity.Destroy();
				}
			}
		}
	}

	public GameEntity ingameBgEntity
	{
		get
		{
			return GetGroup(GameMatcher.IngameBg).GetSingleEntity();
		}
	}

	public IngameBgComponent ingameBg
	{
		get
		{
			return ingameBgEntity.ingameBg;
		}
	}

	public bool hasIngameBg
	{
		get
		{
			return ingameBgEntity != null;
		}
	}

	public GameEntity playerEntity
	{
		get
		{
			return GetGroup(GameMatcher.Player).GetSingleEntity();
		}
	}

	public bool isPlayer
	{
		get
		{
			return playerEntity != null;
		}
		set
		{
			GameEntity gameEntity = playerEntity;
			if (value != (gameEntity != null))
			{
				if (value)
				{
					CreateEntity().isPlayer = true;
				}
				else
				{
					gameEntity.Destroy();
				}
			}
		}
	}

	public GameEntity poolContainerEntity
	{
		get
		{
			return GetGroup(GameMatcher.PoolContainer).GetSingleEntity();
		}
	}

	public PoolContainerComponent poolContainer
	{
		get
		{
			return poolContainerEntity.poolContainer;
		}
	}

	public bool hasPoolContainer
	{
		get
		{
			return poolContainerEntity != null;
		}
	}

	public GameEntity worldEntity
	{
		get
		{
			return GetGroup(GameMatcher.World).GetSingleEntity();
		}
	}

	public WorldComponent world
	{
		get
		{
			return worldEntity.world;
		}
	}

	public bool hasWorld
	{
		get
		{
			return worldEntity != null;
		}
	}

	public GameContext()
		: base(148, 0, new ContextInfo("Game", GameComponentsLookup.componentNames, GameComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => new SafeAERC(entity)))
	{
	}

	public GameEntity SetIngameBg(bgParallaxBehaviour newBg)
	{
		if (hasIngameBg)
		{
			throw new EntitasException(string.Concat("Could not set IngameBg!\n", this, " already has an entity with IngameBgComponent!"), "You should check if the context already has a ingameBgEntity before setting it or use context.ReplaceIngameBg().");
		}
		GameEntity gameEntity = CreateEntity();
		gameEntity.AddIngameBg(newBg);
		return gameEntity;
	}

	public void ReplaceIngameBg(bgParallaxBehaviour newBg)
	{
		GameEntity gameEntity = ingameBgEntity;
		if (gameEntity == null)
		{
			gameEntity = SetIngameBg(newBg);
		}
		else
		{
			gameEntity.ReplaceIngameBg(newBg);
		}
	}

	public void RemoveIngameBg()
	{
		ingameBgEntity.Destroy();
	}

	public GameEntity SetPoolContainer(Dictionary<string, Stack<GameObject>> newPools)
	{
		if (hasPoolContainer)
		{
			throw new EntitasException(string.Concat("Could not set PoolContainer!\n", this, " already has an entity with PoolContainerComponent!"), "You should check if the context already has a poolContainerEntity before setting it or use context.ReplacePoolContainer().");
		}
		GameEntity gameEntity = CreateEntity();
		gameEntity.AddPoolContainer(newPools);
		return gameEntity;
	}

	public void ReplacePoolContainer(Dictionary<string, Stack<GameObject>> newPools)
	{
		GameEntity gameEntity = poolContainerEntity;
		if (gameEntity == null)
		{
			gameEntity = SetPoolContainer(newPools);
		}
		else
		{
			gameEntity.ReplacePoolContainer(newPools);
		}
	}

	public void RemovePoolContainer()
	{
		poolContainerEntity.Destroy();
	}

	public GameEntity SetWorld(float newDistance, float newSpeed, List<LevelElementData> newLevelElements)
	{
		if (hasWorld)
		{
			throw new EntitasException(string.Concat("Could not set World!\n", this, " already has an entity with WorldComponent!"), "You should check if the context already has a worldEntity before setting it or use context.ReplaceWorld().");
		}
		GameEntity gameEntity = CreateEntity();
		gameEntity.AddWorld(newDistance, newSpeed, newLevelElements);
		return gameEntity;
	}

	public void ReplaceWorld(float newDistance, float newSpeed, List<LevelElementData> newLevelElements)
	{
		GameEntity gameEntity = worldEntity;
		if (gameEntity == null)
		{
			gameEntity = SetWorld(newDistance, newSpeed, newLevelElements);
		}
		else
		{
			gameEntity.ReplaceWorld(newDistance, newSpeed, newLevelElements);
		}
	}

	public void RemoveWorld()
	{
		worldEntity.Destroy();
	}
}
