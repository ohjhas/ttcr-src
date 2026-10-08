using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

public sealed class CollectibleMagnetSystem : ReactiveSystem<GameEntity>
{
	private readonly GameContext gameContext;

	public CollectibleMagnetSystem(Contexts contexts)
		: base((IContext<GameEntity>)contexts.game)
	{
		gameContext = contexts.game;
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.isCollectibleMagnet;
	}

	protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
	{
		return context.CreateCollector(GameMatcher.Collected);
	}

	protected override void Execute(List<GameEntity> entities)
	{
		GameEntity playerEntity = gameContext.playerEntity;
		playerEntity.ReplaceMagnetized(SettingsService.GetSetting<SettingsPowerupsSO>().MagnetDuration);
	}
}
