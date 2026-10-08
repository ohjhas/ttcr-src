using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ConsumeVideoTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public ConsumeVideoTransactionSystem(Contexts contexts)
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
			return entity.isTransaction && !entity.isTransactionComplete && entity.isO7SdkRewardAdsShow;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isO7SdkRewardAdsShow)
				{
					if (Contexts.sharedInstance.state.o7SdkEntity.hasO7SdkRewardAwarded)
					{
						Contexts.sharedInstance.state.o7SdkEntity.RemoveO7SdkRewardAwarded();
					}
					if (entity.hasBoughtChestPrepared)
					{
						Contexts.sharedInstance.state.CreateEntity().isOpenFreeChest = true;
					}
				}
			}
		}
	}
}
