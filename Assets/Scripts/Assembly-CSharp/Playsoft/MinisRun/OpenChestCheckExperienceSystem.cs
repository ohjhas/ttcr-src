using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class OpenChestCheckExperienceSystem : ReactiveSystem<StateEntity>
	{
		private const PlayerExperienceType EXPERIENCE_TYPE = PlayerExperienceType.OpenChest;

		private readonly StateContext stateContext;

		public OpenChestCheckExperienceSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasBoughtChest;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				List<PlayerLevelExperienceAmountData> source = stateContext.balancingDataEntity.playerLevelExperienceAmountData.Map[PlayerExperienceType.OpenChest];
				int chestId = entity.boughtChest.Chest.Id;
				PlayerLevelExperienceAmountData playerLevelExperienceAmountData = source.FirstOrDefault((PlayerLevelExperienceAmountData x) => x.Level == chestId);
				if (playerLevelExperienceAmountData.ExpAmount == 0)
				{
					playerLevelExperienceAmountData = source.OrderByDescending((PlayerLevelExperienceAmountData x) => x.Level).FirstOrDefault();
				}
				stateContext.CreateEntity().AddGrantPlayerExperience(playerLevelExperienceAmountData.ExpAmount);
			}
		}
	}
}
