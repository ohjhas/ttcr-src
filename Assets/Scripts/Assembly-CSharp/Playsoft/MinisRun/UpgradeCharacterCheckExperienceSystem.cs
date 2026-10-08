using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UpgradeCharacterCheckExperienceSystem : ReactiveSystem<StateEntity>
	{
		private const PlayerExperienceType EXPERIENCE_TYPE = PlayerExperienceType.UpgradeCharacter;

		private readonly StateContext stateContext;

		public UpgradeCharacterCheckExperienceSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.UpgradeCharacter.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasUpgradeCharacter;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				List<PlayerLevelExperienceAmountData> source = stateContext.balancingDataEntity.playerLevelExperienceAmountData.Map[PlayerExperienceType.UpgradeCharacter];
				string name = "CharUpgradeData" + entity.upgradeCharacter.CharacterId;
				StateEntity upgradeData = stateContext.GetEntitiesWithName(name).SingleEntity();
				PlayerLevelExperienceAmountData playerLevelExperienceAmountData = source.FirstOrDefault((PlayerLevelExperienceAmountData x) => x.Level == upgradeData.integer.Amount);
				if (playerLevelExperienceAmountData.ExpAmount == 0)
				{
					playerLevelExperienceAmountData = source.OrderByDescending((PlayerLevelExperienceAmountData x) => x.Level).FirstOrDefault();
				}
				stateContext.CreateEntity().AddGrantPlayerExperience(playerLevelExperienceAmountData.ExpAmount);
			}
		}
	}
}
