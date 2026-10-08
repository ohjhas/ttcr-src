using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class GrantBoughtLevelTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public GrantBoughtLevelTransactionSystem(Contexts contexts)
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
			return entity.isTransaction && !entity.isTransactionGranted && (entity.hasBoughtCharacterUpgrade || entity.hasBoughtCompanionUpgrade);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasBoughtCharacterUpgrade)
				{
					GrantCharacterUpgrade(entity.boughtCharacterUpgrade.CharacterId);
				}
				if (entity.hasBoughtCompanionUpgrade)
				{
					GrantCompanionUpgrade(entity.boughtCompanionUpgrade.CompanionId);
				}
			}
		}

		private void GrantCharacterUpgrade(int characterId)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.AddUpgradeCharacter(characterId);
		}

		private void GrantCompanionUpgrade(int companionId)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.AddUpgradeCompanion(companionId);
		}
	}
}
