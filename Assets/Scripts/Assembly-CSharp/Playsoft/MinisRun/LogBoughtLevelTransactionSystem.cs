using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogBoughtLevelTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public LogBoughtLevelTransactionSystem(Contexts contexts)
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
			return entity.isTransaction && (entity.hasBoughtCharacterUpgrade || entity.hasBoughtCompanionUpgrade);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasSoftCurrnecyPrice)
				{
					if (entity.hasBoughtCharacterUpgrade)
					{
						LogCoinsSpent(true, entity.boughtCharacterUpgrade.CharacterId, entity.softCurrnecyPrice.Amount);
						LogCharacterUpgrade(entity.boughtCharacterUpgrade.CharacterId);
					}
					else
					{
						LogCoinsSpent(false, entity.boughtCompanionUpgrade.CompanionId, entity.softCurrnecyPrice.Amount);
						LogCompanionUpgrade(entity.boughtCompanionUpgrade.CompanionId);
					}
				}
			}
		}

		private void LogCoinsSpent(bool isCharUpgrade, int itemId, int softCurrencyPrice)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			string newEid = ((!isCharUpgrade) ? "companion-upgrade" : "character-upgrade");
			StateEntity stateEntity2 = stateContext.GetEntitiesWithName("Coins").SingleEntity();
			int amount = stateEntity2.integer.Amount;
			string newP = ((!isCharUpgrade) ? stateContext.balancingDataEntity.companionsStaticBalancingData.Map[itemId].Name.ToLower() : stateContext.balancingDataEntity.characterStaticBalancingData.Map[itemId].Name.ToLower());
			stateEntity.AddEventCustom(newEid, "gc", "coins", newP, softCurrencyPrice, amount, null, null, null, false);
		}

		private void LogCharacterUpgrade(int itemId)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			string newP = stateContext.balancingDataEntity.characterStaticBalancingData.Map[itemId].Name.ToLower();
			CSCharacterData charData = DataProviderHelper.GetCharData(itemId);
			stateEntity.AddEventCustom("character-upgrade", "character-info", null, newP, charData.OwnedParts, charData.GetNextUpgradeCostParts(), charData.Level.ToString(), null, null, false);
		}

		private void LogCompanionUpgrade(int itemId)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkCall = true;
			string newP = stateContext.balancingDataEntity.companionsStaticBalancingData.Map[itemId].Name.ToLower();
			CSCharacterData charData = DataProviderHelper.GetCharData(itemId);
			stateEntity.AddEventCustom("companion-upgrade", "companion-info", null, newP, charData.OwnedParts, charData.GetNextUpgradeCostParts(), charData.Level.ToString(), null, null, false);
		}
	}
}
