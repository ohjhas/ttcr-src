using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class GrantBoughtChestTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public GrantBoughtChestTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionValidated.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction && !entity.isTransactionGranted && entity.hasBoughtChestPrepared;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasBoughtChestPrepared)
				{
					GrantChest(entity.boughtChestPrepared.PreparedChestRewards);
				}
			}
		}

		private void GrantChest(List<GeneratedReward> rewardsData)
		{
			foreach (GeneratedReward rewardsDatum in rewardsData)
			{
				SaveCurrentReward(rewardsDatum);
			}
		}

		private void SaveCurrentReward(GeneratedReward reward)
		{
			CheckCharPartsReward(reward);
			CheckCompPartsReward(reward);
			CheckCurrencyReward(reward);
		}

		private void CheckCharPartsReward(GeneratedReward reward)
		{
			if (reward.Type == ItemType.Character)
			{
				int num = reward.Index;
				Dictionary<int, CharactersStaticBalancingData> map = stateContext.balancingDataEntity.characterStaticBalancingData.Map;
				if (num < 0)
				{
					num = DataProviderHelper.GetCharIndexFromName(reward.OptionalName);
				}
				else
				{
					reward.OptionalName = map[num].Name;
				}
				if (map.ContainsKey(num))
				{
					IncreaseCharPartsAmount(num, reward.Amount);
					reward.IsNew = UnlockCharIfNeeded(num, map[num].Unlocked);
				}
			}
		}

		private void CheckCompPartsReward(GeneratedReward reward)
		{
			if (reward.Type == ItemType.Companion)
			{
				int num = reward.Index;
				Dictionary<int, CompanionsStaticBalancingData> map = stateContext.balancingDataEntity.companionsStaticBalancingData.Map;
				if (num < 0)
				{
					num = DataProviderHelper.GetCompIndexFromName(reward.OptionalName);
				}
				else
				{
					reward.OptionalName = map[num].Name;
				}
				if (map.ContainsKey(num))
				{
					IncreaseCompPartsAmount(num, reward.Amount);
					reward.IsNew = UnlockCompIfNeeded(num, map[num].Unlocked);
				}
			}
		}

		private void CheckCurrencyReward(GeneratedReward reward)
		{
			if (reward.Type == ItemType.Currency && reward.Index == ProjectConstants.HARD_CURRENCY_ID)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
				int newAmount = stateEntity.integer.Amount + reward.Amount;
				stateEntity.ReplaceInteger(newAmount);
				stateEntity.isToSave = true;
			}
			else if (reward.Type == ItemType.Currency && reward.Index == ProjectConstants.SOFT_CURRENCY_ID)
			{
				StateEntity stateEntity2 = stateContext.GetEntitiesWithName("Coins").SingleEntity();
				int newAmount2 = stateEntity2.integer.Amount + reward.Amount;
				stateEntity2.ReplaceInteger(newAmount2);
				stateEntity2.isToSave = true;
			}
		}

		private void IncreaseCharPartsAmount(int charId, int rewardAmount)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("CharPartsData" + charId).SingleEntity();
			int newAmount = stateEntity.integer.Amount + rewardAmount;
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddLabel("CharPartsData" + charId);
			stateEntity2.AddInteger(newAmount);
			stateEntity2.AddName("CharPartsDataSend");
		}

		private void IncreaseCompPartsAmount(int compId, int rewardAmount)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("CompPartsData" + compId).SingleEntity();
			int newAmount = stateEntity.integer.Amount + rewardAmount;
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddLabel("CompPartsData" + compId);
			stateEntity2.AddInteger(newAmount);
			stateEntity2.AddName("CompPartsDataSend");
		}

		private bool UnlockCharIfNeeded(int charId, bool unlockedByDefault)
		{
			string name = "CharUnlockData" + charId;
			StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
			if (!stateEntity.boolean.Value && !unlockedByDefault)
			{
				UIEntity uIEntity = uiContext.CreateEntity();
				uIEntity.AddUICharacterUnlocked(charId);
				return true;
			}
			return false;
		}

		private bool UnlockCompIfNeeded(int compId, bool unlockedByDefault)
		{
			string name = "CompUnlockData" + compId;
			StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
			if (!stateEntity.boolean.Value && !unlockedByDefault)
			{
				UIEntity uIEntity = uiContext.CreateEntity();
				uIEntity.AddUICompanionUnlocked(compId);
				return true;
			}
			return false;
		}
	}
}
