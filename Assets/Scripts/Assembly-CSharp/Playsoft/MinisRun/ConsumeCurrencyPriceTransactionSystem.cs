using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ConsumeCurrencyPriceTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public ConsumeCurrencyPriceTransactionSystem(Contexts contexts)
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
			return entity.isTransaction && !entity.isTransactionComplete && (entity.hasHardCurrencyPrice || entity.hasSoftCurrnecyPrice);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasSoftCurrnecyPrice)
				{
					ConsumeSoftCurrency(entity.softCurrnecyPrice.Amount);
				}
				if (entity.hasHardCurrencyPrice)
				{
					ConsumeHardCurrency(entity.hardCurrencyPrice.Amount);
				}
			}
		}

		private void ConsumeSoftCurrency(int amount)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("Coins").SingleEntity();
			int newAmount = stateEntity.integer.Amount - amount;
			stateEntity.ReplaceInteger(newAmount);
			stateEntity.isToSave = true;
		}

		private void ConsumeHardCurrency(int amount)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
			int newAmount = stateEntity.integer.Amount - amount;
			stateEntity.ReplaceInteger(newAmount);
			stateEntity.isToSave = true;
		}
	}
}
