using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CollectStarsCheckExperienceSystem : ReactiveSystem<StateEntity>
	{
		private const PlayerExperienceType EXPERIENCE_TYPE = PlayerExperienceType.CollectStars;

		private readonly StateContext stateContext;

		public CollectStarsCheckExperienceSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.LevelCompleted.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasLevelCompleted;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity e in entities)
			{
				List<PlayerLevelExperienceAmountData> source = stateContext.balancingDataEntity.playerLevelExperienceAmountData.Map[PlayerExperienceType.CollectStars];
				int newStars = e.levelCompleted.NewStars;
				if (newStars == 0)
				{
					break;
				}
				PlayerLevelExperienceAmountData playerLevelExperienceAmountData = source.FirstOrDefault((PlayerLevelExperienceAmountData x) => x.Level == e.levelCompleted.LevelNumber);
				if (playerLevelExperienceAmountData.ExpAmount == 0)
				{
					playerLevelExperienceAmountData = source.OrderByDescending((PlayerLevelExperienceAmountData x) => x.Level).FirstOrDefault();
				}
				stateContext.CreateEntity().AddGrantPlayerExperience(playerLevelExperienceAmountData.ExpAmount * newStars);
			}
		}
	}
}
