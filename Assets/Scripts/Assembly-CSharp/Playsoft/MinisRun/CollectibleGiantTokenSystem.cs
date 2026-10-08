using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CollectibleGiantTokenSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public CollectibleGiantTokenSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isCollectibleGiantToken;
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
				float sizeIncreaseDuration = SettingsService.GetSetting<SettingsPowerupsSO>().SizeIncreaseDuration;
				float b = ((!playerEntity.hasInvulnerability) ? 0f : playerEntity.invulnerability.TimeLeft);
				float newTimeLeft = Mathf.Max(sizeIncreaseDuration, b);
				gameContext.playerEntity.ReplaceSizeIncreased(sizeIncreaseDuration);
				gameContext.playerEntity.ReplaceInvulnerability(newTimeLeft);
			}
		}
	}
}
