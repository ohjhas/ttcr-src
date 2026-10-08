using System.Collections.Generic;
using Entitas;

namespace O7SDK
{
	public class O7SdkRewardAdCloseSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly IGroup<StateEntity> rewardsGroup;

		public O7SdkRewardAdCloseSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
			rewardsGroup = stateContext.GetGroup(StateMatcher.O7SdkRewardAdsShow);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkRewardTransaction);
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity[] entities2 = rewardsGroup.GetEntities();
			foreach (StateEntity stateEntity in entities2)
			{
				stateEntity.isTransactionBegin = true;
			}
			foreach (StateEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
		}
	}
}
