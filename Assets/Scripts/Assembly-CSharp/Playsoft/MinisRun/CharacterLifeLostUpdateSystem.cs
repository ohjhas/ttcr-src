using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CharacterLifeLostUpdateSystem : ReactiveSystem<InputEntity>
	{
		private readonly StateContext stateContext;

		public CharacterLifeLostUpdateSystem(Contexts contexts)
			: base((IContext<InputEntity>)contexts.input)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
		{
			return context.CreateCollector(InputMatcher.PlayerLost);
		}

		protected override bool Filter(InputEntity entity)
		{
			return true;
		}

		protected override void Execute(List<InputEntity> entities)
		{
			int charIndex = GetCharIndex();
			ResetTimerIfNeeded(charIndex);
			RemoveLifes(charIndex, entities.Count);
		}

		private int GetCharIndex()
		{
			HashSet<StateEntity> entitiesWithName = stateContext.GetEntitiesWithName("FirstCharSelectData");
			int result = 0;
			if (entitiesWithName.Count > 0)
			{
				StateEntity stateEntity = entitiesWithName.SingleEntity();
				result = stateEntity.integer.Amount;
			}
			return result;
		}

		private void RemoveLifes(int charId, int lifesToRemove)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("CharLifesData" + charId).SingleEntity();
			int newAmount = stateEntity.integer.Amount - lifesToRemove;
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddName("CharLifesDataSend");
			stateEntity2.AddLabel(stateEntity.name.Name);
			stateEntity2.AddInteger(newAmount);
		}

		private void ResetTimerIfNeeded(int charId)
		{
			string name = "CharUpgradeData" + charId;
			string name2 = "CharLifesData" + charId;
			List<CharactersUpgradesBalancingData> list = stateContext.balancingDataEntity.characterUpgradesBalancingData.Map[charId];
			StateEntity upgradeData = stateContext.GetEntitiesWithName(name).SingleEntity();
			StateEntity stateEntity = stateContext.GetEntitiesWithName(name2).SingleEntity();
			CharactersUpgradesBalancingData charactersUpgradesBalancingData = list.Find((CharactersUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount);
			int lifes = charactersUpgradesBalancingData.Lifes;
			int amount = stateEntity.integer.Amount;
			if (lifes == amount)
			{
				ResetTimer(charId);
			}
		}

		private void ResetTimer(int charId)
		{
			string newLabel = "CharLifesRefillData" + charId;
			DateTime currentTime = stateContext.currentRealTimeEntity.currentRealTime.CurrentTime;
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("CharLifesRefillDataSend");
			stateEntity.AddLabel(newLabel);
			stateEntity.AddDateTime(currentTime);
		}
	}
}
