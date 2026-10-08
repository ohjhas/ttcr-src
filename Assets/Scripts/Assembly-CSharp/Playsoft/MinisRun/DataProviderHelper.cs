using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public static class DataProviderHelper
	{
		public static CSCharacterData GetCharData(int charId)
		{
			StateContext state = Contexts.sharedInstance.state;
			List<CharactersUpgradesBalancingData> list = state.balancingDataEntity.characterUpgradesBalancingData.Map[charId];
			CharactersStaticBalancingData charactersStaticBalancingData = state.balancingDataEntity.characterStaticBalancingData.Map[charId];
			string name = "CharUpgradeData" + charId;
			string name2 = "CharPartsData" + charId;
			string name3 = "CharUnlockData" + charId;
			string name4 = "CharLifesData" + charId;
			string name5 = "CharLifesRefillData" + charId;
			StateEntity upgradeData = state.GetEntitiesWithName(name).SingleEntity();
			StateEntity stateEntity = state.GetEntitiesWithName(name2).SingleEntity();
			StateEntity stateEntity2 = state.GetEntitiesWithName(name3).SingleEntity();
			StateEntity stateEntity3 = state.GetEntitiesWithName(name4).SingleEntity();
			StateEntity stateEntity4 = state.GetEntitiesWithName(name5).SingleEntity();
			CharactersUpgradesBalancingData charactersUpgradesBalancingData = list.Find((CharactersUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount);
			AbilitiesBalancingData ability = state.balancingDataEntity.abilitiesBalancingData.Map[charactersStaticBalancingData.ABILITYTYPE];
			return new CSCharacterData
			{
				Id = charId,
				Name = charactersStaticBalancingData.Name,
				WelcomeMessage = charactersStaticBalancingData.Welcomemessage,
				Level = upgradeData.integer.Amount,
				Ability = ability,
				Stamina = charactersUpgradesBalancingData.Stamina,
				Unlocked = (stateEntity2.boolean.Value || charactersStaticBalancingData.Unlocked),
				Upgrades = list,
				OwnedParts = stateEntity.integer.Amount,
				MinLevel = charactersStaticBalancingData.Minlevel,
				MaxLifes = charactersUpgradesBalancingData.Lifes,
				Lifes = stateEntity3.integer.Amount,
				LastRefillTime = stateEntity4.dateTime.Value,
				RefillCost = charactersStaticBalancingData.Refillcosthc,
				RefillDuration = charactersStaticBalancingData.Refilldurations,
				Rarity = charactersStaticBalancingData.RARITY
			};
		}

		public static CSCompanionData GetCompData(int compId)
		{
			StateContext state = Contexts.sharedInstance.state;
			StateEntity balancingDataEntity = state.balancingDataEntity;
			if (!balancingDataEntity.companionsUpgradesBalancingData.Map.ContainsKey(compId) || !balancingDataEntity.companionsStaticBalancingData.Map.ContainsKey(compId))
			{
				return null;
			}
			List<CompanionsUpgradesBalancingData> upgrades = balancingDataEntity.companionsUpgradesBalancingData.Map[compId];
			CompanionsStaticBalancingData companionsStaticBalancingData = balancingDataEntity.companionsStaticBalancingData.Map[compId];
			if (!balancingDataEntity.abilitiesBalancingData.Map.ContainsKey(companionsStaticBalancingData.ABILITYTYPE))
			{
				return null;
			}
			AbilitiesBalancingData ability = balancingDataEntity.abilitiesBalancingData.Map[companionsStaticBalancingData.ABILITYTYPE];
			string name = "CompUpgradeData" + compId;
			string name2 = "CompPartsData" + compId;
			string name3 = "CompUnlockData" + compId;
			StateEntity stateEntity = state.GetEntitiesWithName(name).SingleEntity();
			StateEntity stateEntity2 = state.GetEntitiesWithName(name2).SingleEntity();
			StateEntity stateEntity3 = state.GetEntitiesWithName(name3).SingleEntity();
			if (stateEntity == null || stateEntity2 == null || stateEntity3 == null)
			{
				return null;
			}
			return new CSCompanionData(compId, companionsStaticBalancingData.Name, stateEntity.integer.Amount, stateEntity2.integer.Amount, companionsStaticBalancingData.Minlevel, stateEntity3.boolean.Value || companionsStaticBalancingData.Unlocked, upgrades, ability, companionsStaticBalancingData.RARITY);
		}

		public static int GetCharIndexFromName(string name)
		{
			int result = -1;
			StateContext state = Contexts.sharedInstance.state;
			Dictionary<int, CharactersStaticBalancingData> map = state.balancingDataEntity.characterStaticBalancingData.Map;
			Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Value.Name.Equals(name))
				{
					result = enumerator.Current.Key;
					break;
				}
			}
			enumerator.Dispose();
			return result;
		}

		public static int GetCompIndexFromName(string name)
		{
			int result = -1;
			StateContext state = Contexts.sharedInstance.state;
			Dictionary<int, CompanionsStaticBalancingData> map = state.balancingDataEntity.companionsStaticBalancingData.Map;
			Dictionary<int, CompanionsStaticBalancingData>.Enumerator enumerator = map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (enumerator.Current.Value.Name.Equals(name))
				{
					result = enumerator.Current.Key;
					break;
				}
			}
			enumerator.Dispose();
			return result;
		}
	}
}
