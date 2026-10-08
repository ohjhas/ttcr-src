using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class GiveDailyMissionsDailyRewardSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public GiveDailyMissionsDailyRewardSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUIGiveDailyMissionsDailyReward;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				GivePlayerTheChest();
				UpdateChestState();
				entity.isDestroyed = true;
			}
		}

		private void GivePlayerTheChest()
		{
			int key = stateContext.balancingDataEntity.initConfigData.Map[EnumSettings.DailyMissionChestId];
			ChestData newChest = stateContext.balancingDataEntity.gatchaBalancingData.Map[key];
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.AddBoughtChest(newChest, ChestType.Daily);
			stateEntity.AddTransactionImage(newChest.AssetName);
			stateEntity.AddTransactionName(newChest.Name, newChest.ContentDescription);
			stateEntity.isTransactionBegin = true;
		}

		private void UpdateChestState()
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("DailyMissionDataSend");
			stateEntity.AddDailyMissionChestStateUpdate(true);
		}
	}
}
