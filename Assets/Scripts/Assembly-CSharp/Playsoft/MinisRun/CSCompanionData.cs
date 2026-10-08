using System.Collections.Generic;

namespace Playsoft.MinisRun
{
	public class CSCompanionData
	{
		public Rarity Rarity;

		public int Id { get; private set; }

		public string Name { get; private set; }

		public int Level { get; private set; }

		public int OwnedParts { get; private set; }

		public bool Unlocked { get; private set; }

		public int MinLevel { get; set; }

		public List<CompanionsUpgradesBalancingData> Upgrades { get; private set; }

		public AbilitiesBalancingData Ability { get; private set; }

		public CSCompanionData(int _id, string _name, int _level, int _ownedParts, int _minLevel, bool _unlocked, List<CompanionsUpgradesBalancingData> _upgrades, AbilitiesBalancingData _ability, Rarity _rarity)
		{
			Id = _id;
			Name = _name;
			Level = _level;
			Ability = _ability;
			Upgrades = _upgrades;
			Unlocked = _unlocked;
			OwnedParts = _ownedParts;
			MinLevel = _minLevel;
			Rarity = _rarity;
		}

		public int GetNextUpgradeCostSC()
		{
			return (Level < Upgrades.Count) ? Upgrades[Level].Upgradecostsc : 0;
		}

		public int GetNextUpgradeCostParts()
		{
			return (Level < Upgrades.Count) ? Upgrades[Level].Upgradecostparts : 0;
		}

		public bool IsNextUpgradeAvailable()
		{
			return Level < Upgrades.Count;
		}
	}
}
