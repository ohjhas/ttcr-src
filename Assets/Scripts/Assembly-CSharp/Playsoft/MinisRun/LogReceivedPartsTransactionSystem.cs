using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogReceivedPartsTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public LogReceivedPartsTransactionSystem(Contexts contexts)
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
			return entity.isTransaction && entity.hasBoughtChestPrepared && entity.hasBoughtChest;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasHardCurrencyPrice || entity.hasSoftCurrnecyPrice)
				{
					LogReceivedParts(entity.boughtChestPrepared.PreparedChestRewards);
				}
			}
		}

		private void LogReceivedParts(List<GeneratedReward> rewardsData)
		{
			foreach (GeneratedReward rewardsDatum in rewardsData)
			{
				if (rewardsDatum.Type.Equals(ItemType.Character) || rewardsDatum.Type.Equals(ItemType.Companion))
				{
					string empty = string.Empty;
					string empty2 = string.Empty;
					int num = 0;
					int num2 = 0;
					int num3 = 0;
					int num4 = 0;
					if (rewardsDatum.Type.Equals(ItemType.Character))
					{
						num = ((rewardsDatum.Index < 0) ? DataProviderHelper.GetCharIndexFromName(rewardsDatum.OptionalName) : rewardsDatum.Index);
						CSCharacterData charData = DataProviderHelper.GetCharData(num);
						num2 = charData.OwnedParts + rewardsDatum.Amount;
						num3 = charData.GetNextUpgradeCostParts();
						num4 = charData.Level;
						empty = "character-info";
						empty2 = stateContext.balancingDataEntity.characterStaticBalancingData.Map[num].Name.ToLower();
					}
					else
					{
						num = ((rewardsDatum.Index < 0) ? DataProviderHelper.GetCompIndexFromName(rewardsDatum.OptionalName) : rewardsDatum.Index);
						CSCompanionData compData = DataProviderHelper.GetCompData(num);
						num2 = compData.OwnedParts + rewardsDatum.Amount;
						num3 = compData.GetNextUpgradeCostParts();
						num4 = compData.Level;
						empty = "companion-info";
						empty2 = stateContext.balancingDataEntity.companionsStaticBalancingData.Map[num].Name.ToLower();
					}
					StateEntity stateEntity = stateContext.CreateEntity();
					stateEntity.isO7SdkCall = true;
					stateEntity.AddEventCustom("parts-received", empty, null, empty2, num2, num3, num4.ToString(), null, null, false);
				}
			}
		}
	}
}
