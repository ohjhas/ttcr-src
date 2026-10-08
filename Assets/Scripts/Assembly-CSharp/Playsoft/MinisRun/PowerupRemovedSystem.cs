using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class PowerupRemovedSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public PowerupRemovedSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.SizeIncreased.Removed(), GameMatcher.SpeedUp.Removed());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			if (!playerEntity.hasSpeedUp && !playerEntity.hasSizeIncreased)
			{
				float b = ((!playerEntity.hasInvulnerability) ? 0f : playerEntity.invulnerability.TimeLeft);
				float num = Mathf.Max(SettingsService.SettingsSO.InvulnerabilityTime, b);
				playerEntity.ReplaceInvulnerability(num);
				int numberOfCycles = SettingsService.GetSetting<SettingsVisualFeedbackSO>().NumberOfCycles;
				playerEntity.ReplacePlayerBlinking(num, num, numberOfCycles);
			}
		}
	}
}
