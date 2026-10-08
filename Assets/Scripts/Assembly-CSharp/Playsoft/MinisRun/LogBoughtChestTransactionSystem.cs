using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogBoughtChestTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public LogBoughtChestTransactionSystem(Contexts contexts)
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
				int hardCurrencyPrice = (entity.hasHardCurrencyPrice ? entity.hardCurrencyPrice.Amount : 0);
				int softCurrencyPrice = (entity.hasSoftCurrnecyPrice ? entity.softCurrnecyPrice.Amount : 0);
				LogChestData(entity.boughtChest.Type, entity.boughtChest.Chest, entity.boughtChestPrepared.PreparedChestRewards, hardCurrencyPrice, softCurrencyPrice);
			}
		}

		private void LogChestData(ChestType chestType, ChestData boughtChest, List<GeneratedReward> rewards, int hardCurrencyPrice, int softCurrencyPrice)
		{
			GeneratedReward generatedReward = rewards.FirstOrDefault((GeneratedReward x) => x.Type == ItemType.Currency && x.Index == ProjectConstants.SOFT_CURRENCY_ID);
			GeneratedReward generatedReward2 = rewards.FirstOrDefault((GeneratedReward x) => x.Type == ItemType.Currency && x.Index == ProjectConstants.HARD_CURRENCY_ID);
			int softCurrencyReward = ((generatedReward != null && generatedReward.Amount > 0) ? generatedReward.Amount : 0);
			int hardCurrencyReward = ((generatedReward2 != null && generatedReward2.Amount > 0) ? generatedReward2.Amount : 0);
			string rarityName = O7SdkEventsConstants.GetRarityName(chestType, boughtChest.Rarity);
			LogHardCurrencyPrice(hardCurrencyPrice, rarityName, hardCurrencyReward);
			LogSoftCurrencyPrice(softCurrencyPrice, rarityName, softCurrencyReward, hardCurrencyPrice);
			LogHardCurrencyReward(hardCurrencyReward, rarityName);
			LogSoftCurrencyReward(softCurrencyReward, rarityName);
		}

		private void LogHardCurrencyPrice(int hardCurrencyPrice, string chestRarityName, int hardCurrencyReward)
		{
			if (hardCurrencyPrice > 0)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
				SendEvent("diamonds", chestRarityName, -hardCurrencyPrice, stateEntity.integer.Amount - hardCurrencyReward);
			}
		}

		private void LogSoftCurrencyPrice(int softCurrencyPrice, string chestRarityName, int softCurrencyReward, int hardCurrencyPrice)
		{
			if (softCurrencyPrice > 0 || (softCurrencyPrice <= 0 && hardCurrencyPrice <= 0))
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("Coins").SingleEntity();
				SendEvent("coins", chestRarityName, -softCurrencyPrice, stateEntity.integer.Amount - softCurrencyReward);
			}
		}

		private void LogHardCurrencyReward(int hardCurrencyReward, string chestRarityName)
		{
			if (hardCurrencyReward > 0)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
				SendEvent("diamonds", chestRarityName, hardCurrencyReward, stateEntity.integer.Amount);
			}
		}

		private void LogSoftCurrencyReward(int softCurrencyReward, string chestRarityName)
		{
			if (softCurrencyReward > 0)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("Coins").SingleEntity();
				SendEvent("coins", chestRarityName, softCurrencyReward, stateEntity.integer.Amount);
			}
		}

		private void SendEvent(string currencyId, string rarity, double currencyChange, double newCurrencyBalance)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			stateEntity.AddEventCustom("chest", "gc", currencyId, rarity, currencyChange, newCurrencyBalance, null, null, null, false);
		}
	}
}
