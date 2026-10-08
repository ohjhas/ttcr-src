using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class GrantBoughtPlayerRestoreSystem : ReactiveSystem<StateEntity>
	{
		public GrantBoughtPlayerRestoreSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction && !entity.isTransactionGranted && (entity.hasBoughtPlayerResurect || entity.isBoughtPlayerRevive);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isBoughtPlayerRevive)
				{
					GrantPlayerRevive();
				}
				if (entity.hasBoughtPlayerResurect)
				{
					GrantPlayerResurect();
				}
			}
		}

		private void GrantPlayerRevive()
		{
			Contexts.sharedInstance.game.playerEntity.isPlayerRevive = true;
		}

		private void GrantPlayerResurect()
		{
			Contexts.sharedInstance.game.playerEntity.AddPlayerResurrect(0f);
		}
	}
}
