using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DailyMissionCountTokenSystem : ReactiveSystem<GameEntity>, IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		private bool isMissionActive;

		public DailyMissionCountTokenSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			gameContext = contexts.game;
		}

		public void Initialize()
		{
			GameEntity worldEntity = gameContext.worldEntity;
			isMissionActive = worldEntity.hasDailyMissionCollect;
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.hasCollectibleData;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.Collected);
		}

		protected override void Execute(List<GameEntity> entities)
		{
			if (isMissionActive)
			{
				CountTokens(entities);
			}
		}

		private void CountTokens(List<GameEntity> entities)
		{
			foreach (GameEntity entity in entities)
			{
				Dictionary<CollectibleType, KeyValuePair<DailyMissionGroup, int>> missionDict = gameContext.worldEntity.dailyMissionCollect.MissionDict;
				if (missionDict.ContainsKey(entity.collectibleData.Type))
				{
					missionDict[entity.collectibleData.Type] = new KeyValuePair<DailyMissionGroup, int>(missionDict[entity.collectibleData.Type].Key, missionDict[entity.collectibleData.Type].Value + 1);
				}
			}
		}
	}
}
