using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CollectibleScoreMultiplierSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public CollectibleScoreMultiplierSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isCollectibleScoreMultiplier;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Collected);
		}

		protected override void Execute(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				GameEntity playerEntity = gameContext.playerEntity;
				float scoreMultiplierDuration = SettingsService.GetSetting<SettingsPowerupsSO>().ScoreMultiplierDuration;
				float scoreMultiplierFactor = SettingsService.GetSetting<SettingsPowerupsSO>().ScoreMultiplierFactor;
				gameContext.playerEntity.ReplaceScoreMultiplier(scoreMultiplierDuration, scoreMultiplierFactor);
			}
		}
	}
}
