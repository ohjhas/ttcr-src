using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class BeginValidationTransactionSystem : ReactiveSystem<StateEntity>
	{
		private StateContext stateContext;

		public BeginValidationTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionBegin.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				bool flag = true;
				if (entity.hasSoftCurrnecyPrice)
				{
					flag = flag && ValidateSoftCurrency(entity.softCurrnecyPrice.Amount);
				}
				if (entity.hasHardCurrencyPrice)
				{
					flag = flag && ValidateHardCurrency(entity.hardCurrencyPrice.Amount);
				}
				if (entity.hasCharacterPartsPrice)
				{
					flag = flag && ValidateCharacterParts(entity.characterPartsPrice.CharacterId, entity.characterPartsPrice.Amount);
				}
				if (entity.hasCompanionPartsPrice)
				{
					flag = flag && ValidateCompanionParts(entity.companionPartsPrice.CompanionId, entity.companionPartsPrice.Amount);
				}
				if (entity.isO7SdkRewardAdsShow)
				{
					flag = flag && stateContext.o7SdkEntity.hasO7SdkRewardAwarded;
				}
				if (flag)
				{
					entity.isTransactionValidated = true;
				}
				else
				{
					entity.isTransactionNotValidated = true;
				}
				entity.isTransactionBegin = false;
			}
		}

		private bool ValidateSoftCurrency(int amount)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("Coins").SingleEntity();
			return stateEntity.hasInteger && stateEntity.integer.Amount >= amount;
		}

		private bool ValidateHardCurrency(int amount)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
			return stateEntity.hasInteger && stateEntity.integer.Amount >= amount;
		}

		private bool ValidateCharacterParts(int characterId, int amount)
		{
			string name = "CharPartsData" + characterId;
			StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
			return stateEntity.hasInteger && stateEntity.integer.Amount >= amount;
		}

		private bool ValidateCompanionParts(int companionId, int amount)
		{
			string name = "CompPartsData" + companionId;
			StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
			return stateEntity.hasInteger && stateEntity.integer.Amount >= amount;
		}
	}
}
