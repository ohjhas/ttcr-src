using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class GrantBoughtCurrencyTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public GrantBoughtCurrencyTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction && !entity.isTransactionGranted && (entity.hasBoughtSoftCurrency || entity.hasBoughtHardCurrency);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasBoughtSoftCurrency)
				{
					if (entity.hasTransactionOrigin)
					{
						AnimateCurrencyUpdate(ProjectConstants.SOFT_CURRENCY_ID, entity.boughtSoftCurrency.Amount, entity.transactionOrigin.Origin);
					}
					GrantSoftCurrency(entity.boughtSoftCurrency.Amount);
				}
				if (entity.hasBoughtHardCurrency)
				{
					if (entity.hasTransactionOrigin)
					{
						AnimateCurrencyUpdate(ProjectConstants.HARD_CURRENCY_ID, entity.boughtHardCurrency.Amount, entity.transactionOrigin.Origin);
					}
					GrantHardCurrency(entity.boughtHardCurrency.Amount);
				}
			}
		}

		private void AnimateCurrencyUpdate(int itemIndex, int itemAmount, RectTransform origin)
		{
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.isUIAction = true;
			uIEntity.AddUIShowCurrencyUpdate(itemIndex, itemAmount, origin, false);
		}

		private void GrantSoftCurrency(int amount)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("Coins").SingleEntity();
			int newAmount = stateEntity.integer.Amount + amount;
			stateEntity.ReplaceInteger(newAmount);
			stateEntity.isToSave = true;
		}

		private void GrantHardCurrency(int amount)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
			int newAmount = stateEntity.integer.Amount + amount;
			stateEntity.ReplaceInteger(newAmount);
			stateEntity.isToSave = true;
		}
	}
}
