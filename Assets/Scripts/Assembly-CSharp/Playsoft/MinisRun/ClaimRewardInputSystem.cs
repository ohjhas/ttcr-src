using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ClaimRewardInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		public ClaimRewardInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUIClaimReward;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			bool flag = false;
			foreach (UIEntity entity in entities)
			{
				flag = flag || entity.uIClaimReward.shouldRefreshScreen;
				ClaimReward(entity.uIClaimReward.data, entity.uIClaimReward.origin);
				entity.isDestroyed = true;
			}
			if (flag)
			{
				uiContext.RefreshScreen();
			}
		}

		private void ClaimReward(RewardData data, RectTransform origin)
		{
			switch (data.Type)
			{
			case ItemType.Currency:
				ClaimCurrency(data, origin);
				break;
			case ItemType.Chest:
				ClaimChest(data, origin);
				break;
			}
		}

		private void ClaimCurrency(RewardData data, RectTransform origin)
		{
			if (data.Index == ProjectConstants.SOFT_CURRENCY_ID)
			{
				StateEntity stateEntity = CreateTransaction();
				stateEntity.AddBoughtSoftCurrency(data.Amount);
				stateEntity.AddTransactionOrigin(origin);
			}
			else if (data.Index == ProjectConstants.HARD_CURRENCY_ID)
			{
				StateEntity stateEntity2 = CreateTransaction();
				stateEntity2.AddBoughtHardCurrency(data.Amount);
				stateEntity2.AddTransactionOrigin(origin);
			}
		}

		private void ClaimChest(RewardData data, RectTransform origin)
		{
			ChestData newChest = Contexts.sharedInstance.state.balancingDataEntity.gatchaBalancingData.Map[data.Index];
			StateEntity stateEntity = CreateTransaction();
			stateEntity.AddBoughtChest(newChest, ChestType.LevelUp);
		}

		private StateEntity CreateTransaction()
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.isTransactionBegin = true;
			return stateEntity;
		}
	}
}
