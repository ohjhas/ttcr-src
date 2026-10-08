using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ConsumePartsPriceTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public ConsumePartsPriceTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionGranted.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction && !entity.isTransactionComplete && (entity.hasCharacterPartsPrice || entity.hasCompanionPartsPrice);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasCharacterPartsPrice)
				{
					ConsumeCharacterPartsPrice(entity.characterPartsPrice.CharacterId, entity.characterPartsPrice.Amount);
				}
				if (entity.hasCompanionPartsPrice)
				{
					ConsumeCompanionPartsPrice(entity.companionPartsPrice.CompanionId, entity.companionPartsPrice.Amount);
				}
			}
		}

		private void ConsumeCharacterPartsPrice(int characterId, int amount)
		{
			string name = "CharPartsData" + characterId;
			StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
			int newAmount = stateEntity.integer.Amount - amount;
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddLabel("CharPartsData" + characterId);
			stateEntity2.AddName("CharPartsDataSend");
			stateEntity2.AddInteger(newAmount);
		}

		private void ConsumeCompanionPartsPrice(int companionId, int amount)
		{
			string name = "CompPartsData" + companionId;
			StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
			int newAmount = stateEntity.integer.Amount - amount;
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddLabel("CompPartsData" + companionId);
			stateEntity2.AddName("CompPartsDataSend");
			stateEntity2.AddInteger(newAmount);
		}
	}
}
