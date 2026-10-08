using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TransactionValidatedSystem : ReactiveSystem<StateEntity>
	{
		private readonly UIContext uiContext;

		public TransactionValidatedSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasBoughtChest)
				{
					BaseDialog newDialog = uiContext.OpenDialog<ChestRevealScreen>();
					UIEntity uIEntity = uiContext.CreateEntity();
					uIEntity.isUITotalCoinsRequest = true;
					uIEntity.isUITotalGemsRequest = true;
					uIEntity.isUICharListRequest = true;
					uIEntity.isUICompanionListRequest = true;
					uIEntity.AddUIChestRewardRequest(entity.boughtChest.Chest, entity.boughtChestPrepared.PreparedChestRewards);
					uIEntity.AddSetup(newDialog);
				}
			}
		}
	}
}
