using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayerSpecialRunStartSystem : ReactiveSystem<GameEntity>
	{
		private readonly GameContext gameContext;

		public PlayerSpecialRunStartSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer && !entity.isAbilityRun;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.SizeIncreased.AddedOrRemoved(), GameMatcher.Magnetized.AddedOrRemoved(), GameMatcher.ScoreMultiplier.AddedOrRemoved(), GameMatcher.UpgradeScore.AddedOrRemoved(), GameMatcher.SpeedUp.AddedOrRemoved(), GameMatcher.AbilityRun.Removed());
		}

		protected override void Execute(List<GameEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			playerEntity.isSpecialRun = playerEntity.hasSizeIncreased || playerEntity.hasScoreMultiplier || playerEntity.hasMagnetized || playerEntity.hasUpgradeScore || playerEntity.hasSpeedUp;
		}
	}
}
