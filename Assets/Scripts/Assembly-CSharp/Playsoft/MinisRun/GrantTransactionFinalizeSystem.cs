using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class GrantTransactionFinalizeSystem : ReactiveSystem<StateEntity>
	{
		public GrantTransactionFinalizeSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction && !entity.isTransactionGranted;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				entity.isTransactionGranted = true;
				entity.isTransactionBegin = false;
			}
		}
	}
}
