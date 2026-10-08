using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ReplaceChestForTutorialTransactionSystem : ReactiveSystem<StateEntity>
	{
		private StateContext stateContext;

		public ReplaceChestForTutorialTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasBoughtChest;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if ((stateContext.activeScreen.ScreenId != ScreenId.ShopScreen && stateContext.activeScreen.ScreenId != ScreenId.ConfirmTransactionPopup) || !TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.Shop))
				{
					break;
				}
				ChestData chest = entity.boughtChest.Chest;
				chest.RewardsData = stateContext.balancingDataEntity.gatchaBalancingData.Map[7].RewardsData;
				entity.ReplaceBoughtChest(chest, ChestType.Shop);
				stateContext.CreateEntity().AddPassLevelAction(TutorialOutgameLevel.Shop);
			}
		}
	}
}
