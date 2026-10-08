using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Playsoft.MinisRun;
using UnityEngine;

public sealed class DestroyWorldSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	private readonly UIContext uiContext;

	public DestroyWorldSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
		uiContext = contexts.uI;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Dead);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.hasWorld;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		GameEntity[] entities2 = gameContext.GetEntities();
		foreach (GameEntity gameEntity in entities2)
		{
			if (gameEntity.hasView)
			{
				try
				{
					gameEntity.view.gameObject.Unlink();
				}
				catch (Exception ex)
				{
					Debug.LogError("Blocker = " + ex.ToString());
				}
			}
		}
		string newName = (gameContext.worldEntity.isReplayLevel ? ProjectConstants.INGAME_SCENE_NAME : ((!gameContext.worldEntity.isReturnToHomeLevel) ? ProjectConstants.WORLD_SELECT_SCENE_NAME : ProjectConstants.HOME_SCENE_NAME));
		UIEntity uIEntity = uiContext.CreateEntity();
		uIEntity.AddChangeScene(newName, false);
	}
}
