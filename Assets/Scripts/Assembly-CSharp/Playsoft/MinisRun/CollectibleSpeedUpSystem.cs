using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CollectibleSpeedUpSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public CollectibleSpeedUpSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isCollectibleSpeedUp;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Collected);
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			float speedUpDuration = SettingsService.GetSetting<SettingsPowerupsSO>().SpeedUpDuration;
			float b = ((!playerEntity.hasInvulnerability) ? 0f : playerEntity.invulnerability.TimeLeft);
			float newTimeLeft = Mathf.Max(speedUpDuration, b);
			playerEntity.ReplaceSpeedUp(speedUpDuration, 1f);
			playerEntity.ReplaceInvulnerability(newTimeLeft);
			playerEntity.ReplaceOnClouds(speedUpDuration);
		}
	}
}
