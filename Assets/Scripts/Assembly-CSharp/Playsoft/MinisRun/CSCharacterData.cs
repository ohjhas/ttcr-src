using System;
using System.Collections.Generic;

namespace Playsoft.MinisRun
{
	public struct CSCharacterData
	{
		public Rarity Rarity;

		public int Id { get; set; }

		public string Name { get; set; }

		public string WelcomeMessage { get; set; }

		public int Level { get; set; }

		public int Stamina { get; set; }

		public int OwnedParts { get; set; }

		public bool Unlocked { get; set; }

		public int MinLevel { get; set; }

		public int Lifes { get; set; }

		public int MaxLifes { get; set; }

		public DateTime LastRefillTime { get; set; }

		public int RefillCost { get; set; }

		public int RefillDuration { get; set; }

		public List<CharactersUpgradesBalancingData> Upgrades { get; set; }

		public AbilitiesBalancingData Ability { get; set; }

		public int GetNextUpgradeCostSC()
		{
			return (Level < Upgrades.Count) ? Upgrades[Level].Upgradecostsc : 0;
		}

		public int GetNextUpgradeCostParts()
		{
			return (Level < Upgrades.Count) ? Upgrades[Level].Upgradecostparts : 0;
		}

		public int GetNextUpgradeStaminaValue()
		{
			return (Level < Upgrades.Count) ? Upgrades[Level].Stamina : 0;
		}

		public bool IsNextUpgradeAvailable()
		{
			return Level < Upgrades.Count;
		}
	}
}
