using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MissionResultSendSystem : ReactiveSystem<InputEntity>
	{
		private readonly GameContext gameContext;

		public MissionResultSendSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			gameContext = contexts.game;
		}

		protected override bool Filter(InputEntity entity)
		{
			return entity.isLevelEndAnimationFinished;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.LevelEndAnimationFinished);
		}

		protected override void Execute(List<InputEntity> entities)
		{
			GameEntity playerEntity = gameContext.playerEntity;
			GameEntity worldEntity = gameContext.worldEntity;
			if (worldEntity.hasMissionThresholds)
			{
				playerEntity.ReplaceMissionResult(worldEntity.missionCounter.Value, CalculateResult(worldEntity));
			}
		}

		private int CalculateResult(GameEntity world)
		{
			int value = world.missionCounter.Value;
			return (value >= world.missionThresholds.FirstStar) ? ((value < world.missionThresholds.SecondStar) ? 1 : ((value >= world.missionThresholds.ThirdStar) ? 3 : 2)) : 0;
		}
	}
}
