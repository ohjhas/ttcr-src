using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TransactionNotValidatedSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uiContext;

		public TransactionNotValidatedSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionNotValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasHardCurrencyPrice && entity.hardCurrencyPrice.Amount > 0)
				{
					UIEntity uIEntity = uiContext.CreateEntity();
					uIEntity.isUIAction = true;
					uIEntity.AddUIDisplayExplanation(ExplanationType.Gems);
				}
				else if (entity.hasSoftCurrnecyPrice && entity.softCurrnecyPrice.Amount > 0)
				{
					UIEntity uIEntity2 = uiContext.CreateEntity();
					uIEntity2.isUIAction = true;
					uIEntity2.AddUIDisplayExplanation(ExplanationType.Coins);
				}
				else if (entity.isO7SdkRewardAdsShow)
				{
					entity.isO7SdkRewardNotAwarded = true;
				}
			}
		}
	}
}
