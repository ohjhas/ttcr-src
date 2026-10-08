using System.Collections.Generic;
using Entitas;

namespace O7SDK
{
	public class O7SdkRateMeSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKUtilityManager utilityManager;

		public O7SdkRateMeSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			utilityManager = stateContext.o7SdkEntity.o7SdkUtilityManager.Manager;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkRateMeNow.Added(), StateMatcher.O7SdkRateMeLater.Added(), StateMatcher.O7SdkRateMeDontLike.Added(), StateMatcher.O7SdkRateMeClosed.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkRateMeNow || entity.isO7SdkRateMeLater || entity.isO7SdkRateMeDontLike || entity.isO7SdkRateMeClosed;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				SaveRateMeShownFrequency();
				if (entity.isO7SdkRateMeNow)
				{
					utilityManager.RequestRateThisAppPopup();
					MarkThatPlayerAnswered();
				}
				if (entity.isO7SdkRateMeLater)
				{
				}
				if (entity.isO7SdkRateMeDontLike)
				{
					MarkThatPlayerAnswered();
				}
				if (entity.isO7SdkRateMeClosed)
				{
				}
				entity.isDestroyed = true;
			}
		}

		private void SaveRateMeShownFrequency()
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("RateMeShown").SingleEntity();
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddName("RateMeShownSend");
			stateEntity2.AddInteger(stateEntity.integer.Amount + 1);
		}

		private void MarkThatPlayerAnswered()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("RateMeDontLikeSend");
			stateEntity.AddBoolean(true);
		}
	}
}
