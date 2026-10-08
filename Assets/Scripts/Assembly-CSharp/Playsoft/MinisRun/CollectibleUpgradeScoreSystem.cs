using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CollectibleUpgradeScoreSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public CollectibleUpgradeScoreSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isCollectibleUpgradeScore;
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
				float upgradeScoreDuration = SettingsService.GetSetting<SettingsPowerupsSO>().UpgradeScoreDuration;
				float upgradeScoreDistance = SettingsService.GetSetting<SettingsPowerupsSO>().UpgradeScoreDistance;
				gameContext.playerEntity.ReplaceUpgradeScore(upgradeScoreDuration, upgradeScoreDistance);
			}
		}
	}
}
