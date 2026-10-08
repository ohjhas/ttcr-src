using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class BuyRefillCharacterSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public BuyRefillCharacterSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIRefillChar;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				MakeRefillTransaction(entity.uIRefillChar.CharIndex, entity.uIActionFinishedCallback.Callback);
				entity.isDestroyed = true;
			}
		}

		private void MakeRefillTransaction(int charId, Action callback)
		{
			CharactersStaticBalancingData charactersStaticBalancingData = stateContext.balancingDataEntity.characterStaticBalancingData.Map[charId];
			int refillcosthc = charactersStaticBalancingData.Refillcosthc;
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.isTransactionBegin = true;
			stateEntity.AddHardCurrencyPrice(refillcosthc);
			stateEntity.AddBoughtEnergyRefill(charId);
			stateEntity.AddTransactionCallback(callback);
		}
	}
}
