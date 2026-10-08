using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogIAPPurchaseDoneSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public LogIAPPurchaseDoneSystem(Contexts contexts)
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
			return entity.isTransaction && entity.hasBoughtHardCurrency;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				int amount = entity.boughtHardCurrency.Amount;
				string productId = ((!entity.hasTransactionName) ? null : entity.transactionName.Name);
				LogIAPData(amount, productId);
			}
		}

		private void LogIAPData(int hardCurrencyAmount, string productId)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
			int amount = stateEntity.integer.Amount;
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isO7SdkCall = true;
			stateEntity2.AddEventCustom("iap", "gc", "diamonds", productId, hardCurrencyAmount, amount, null, null, null, false);
		}
	}
}
