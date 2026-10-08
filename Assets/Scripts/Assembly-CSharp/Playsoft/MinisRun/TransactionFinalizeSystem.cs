using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TransactionFinalizeSystem : ReactiveSystem<StateEntity>, ITearDownSystem, ISystem
	{
		private readonly StateContext stateContext;

		public TransactionFinalizeSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void TearDown()
		{
			StateEntity[] entities = stateContext.GetEntities(StateMatcher.Transaction);
			StateEntity[] array = entities;
			foreach (StateEntity stateEntity in array)
			{
				stateEntity.Destroy();
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionGranted.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction && !entity.isTransactionComplete;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				entity.isTransactionComplete = true;
				if (entity.hasTransactionCallback)
				{
					InvokeCallbackIfExists(entity.transactionCallback.Callback);
				}
			}
		}

		private void InvokeCallbackIfExists(Action callback)
		{
			if (callback != null)
			{
				callback();
			}
		}
	}
}
