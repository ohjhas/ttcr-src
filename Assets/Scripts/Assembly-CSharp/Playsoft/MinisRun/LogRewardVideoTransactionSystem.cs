using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogRewardVideoTransactionSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		private readonly IGroup<StateEntity> rewardsGroup;

		public LogRewardVideoTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			rewardsGroup = stateContext.GetGroup(StateMatcher.O7SdkRewardAdsShow);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionGranted);
		}

		protected override bool Filter(StateEntity entity)
		{
			return Contexts.sharedInstance.state.o7SdkEntity.hasO7SdkRewardAwarded && entity.isO7SdkRewardAdsShow && entity.hasO7SdkRewardOfferType;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity[] entities2 = rewardsGroup.GetEntities();
			foreach (StateEntity stateEntity in entities2)
			{
				LogRewardVideo(stateEntity.o7SdkRewardOfferType.offerType);
			}
		}

		private void LogRewardVideo(O7SdkRewardOfferType type)
		{
			string newP = type.ToString().ToLower().Replace('_', '-');
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("complete", "video-offers", null, newP, 0.0, null, null, null, Contexts.sharedInstance.state.o7SdkEntity.o7SdkRewardAwarded.ProviderID, false);
		}
	}
}
