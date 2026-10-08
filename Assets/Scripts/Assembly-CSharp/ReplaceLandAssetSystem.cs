using System.Collections.Generic;
using Entitas;
using UnityEngine;

public sealed class ReplaceLandAssetSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	public ReplaceLandAssetSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Asset.Added());
	}

	protected override bool Filter(GameEntity entity)
	{
		return true;
	}

	protected override void Execute(List<GameEntity> entities)
	{
		string text = ((!gameContext.worldEntity.hasNewWorldSkin) ? gameContext.worldEntity.missionData.MissionData.Worldskin : gameContext.worldEntity.newWorldSkin.WorldSuffix);
		foreach (GameEntity entity in entities)
		{
			string text2 = entity.asset.name + text;
			GameObject gameObject = Resources.Load<GameObject>(text2);
			if (gameObject != null)
			{
				entity.asset.name = text2;
			}
		}
	}
}
