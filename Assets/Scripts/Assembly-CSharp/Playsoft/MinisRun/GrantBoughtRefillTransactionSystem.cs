using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class GrantBoughtRefillTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public GrantBoughtRefillTransactionSystem(Contexts contexts)
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
			return entity.isTransaction && !entity.isTransactionGranted && entity.hasBoughtEnergyRefill;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				RefillCharacterEnergy(entity.boughtEnergyRefill.CharacterId);
			}
		}

		private void RefillCharacterEnergy(int charId)
		{
			DateTime currentTime = stateContext.currentRealTimeEntity.currentRealTime.CurrentTime;
			StateEntity lastRefillEntity = stateContext.GetEntitiesWithName("CharLifesRefillData" + charId).SingleEntity();
			StateEntity currentLifesData = stateContext.GetEntitiesWithName("CharLifesData" + charId).SingleEntity();
			List<CharactersUpgradesBalancingData> list = stateContext.balancingDataEntity.characterUpgradesBalancingData.Map[charId];
			StateEntity upgradeData = stateContext.GetEntitiesWithName("CharUpgradeData" + charId).SingleEntity();
			CharactersUpgradesBalancingData charactersUpgradesBalancingData = list.Find((CharactersUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount);
			RefillLives(currentLifesData, charactersUpgradesBalancingData.Lifes);
			ResetTimer(lastRefillEntity, currentTime);
		}

		private void RefillLives(StateEntity currentLifesData, int maxLifes)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("CharLifesDataSend");
			stateEntity.AddLabel(currentLifesData.name.Name);
			stateEntity.AddInteger(maxLifes);
		}

		private void ResetTimer(StateEntity lastRefillEntity, DateTime currentTime)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("CharLifesRefillDataSend");
			stateEntity.AddLabel(lastRefillEntity.name.Name);
			stateEntity.AddDateTime(currentTime);
		}
	}
}
