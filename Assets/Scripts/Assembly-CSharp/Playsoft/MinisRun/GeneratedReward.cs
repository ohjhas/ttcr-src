namespace Playsoft.MinisRun
{
	public class GeneratedReward
	{
		public ItemType Type { get; private set; }

		public Rarity Rarity { get; private set; }

		public int Index { get; private set; }

		public string OptionalName { get; set; }

		public int Amount { get; set; }

		public bool IsNew { get; set; }

		public GeneratedReward(ItemType type, Rarity rarity, int index, int amount, string optionalName = "")
		{
			Type = type;
			Rarity = rarity;
			Index = index;
			Amount = amount;
			OptionalName = optionalName;
		}
	}
}
