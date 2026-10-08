using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UnlockChapterCheckExperienceSystem : ReactiveSystem<StateEntity>
	{
		private const PlayerExperienceType EXPERIENCE_TYPE = PlayerExperienceType.UnlockChapter;

		private readonly StateContext stateContext;

		public UnlockChapterCheckExperienceSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.NewLevelUnlocked.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasNewLevelUnlocked;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				List<PlayerLevelExperienceAmountData> source = stateContext.balancingDataEntity.playerLevelExperienceAmountData.Map[PlayerExperienceType.UnlockChapter];
				Dictionary<int, MissionsBalancingData> map = stateContext.balancingDataEntity.missionBalancingData.Map;
				MissionsBalancingData newLevel = map[entity.newLevelUnlocked.LevelNumber];
				MissionsBalancingData missionsBalancingData = map[entity.newLevelUnlocked.LevelNumber - 1];
				if (newLevel.Chapter == missionsBalancingData.Chapter)
				{
					continue;
				}
				PlayerLevelExperienceAmountData playerLevelExperienceAmountData = source.FirstOrDefault((PlayerLevelExperienceAmountData x) => x.Level == newLevel.Chapter);
				if (playerLevelExperienceAmountData.ExpAmount == 0)
				{
					playerLevelExperienceAmountData = source.OrderByDescending((PlayerLevelExperienceAmountData x) => x.Level).FirstOrDefault();
				}
				stateContext.CreateEntity().AddGrantPlayerExperience(playerLevelExperienceAmountData.ExpAmount);
			}
		}
	}
}
