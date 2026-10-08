using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ResultScreenInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public ResultScreenInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.WinChestOnResultScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUIChestWon)
				{
					WinChest(entity.uIChestWon.ChestId);
				}
				entity.isDestroyed = true;
			}
		}

		private void WinChest(int chestId)
		{
			ChestData chestData = GetChestData(chestId);
			GiveChestReward(chestData);
		}

		private ChestData GetChestData(int chestId)
		{
			return stateContext.balancingDataEntity.gatchaBalancingData.Map[chestId];
		}

		private void GiveChestReward(ChestData chestData)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.AddBoughtChest(chestData, ChestType.Race);
			stateEntity.AddTransactionImage(chestData.AssetName);
			stateEntity.AddTransactionName(chestData.Name, string.Empty);
			stateEntity.isTransactionBegin = true;
		}
	}
}
