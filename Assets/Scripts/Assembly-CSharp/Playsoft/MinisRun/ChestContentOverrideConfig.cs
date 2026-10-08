using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class ChestContentOverrideConfig
	{
		[SerializeField]
		private int id;

		[SerializeField]
		private string gT;

		private GroupType? grouptype;

		[SerializeField]
		private string iT;

		private ItemType? itemtype;

		[SerializeField]
		private string r;

		private Rarity? rarity;

		[SerializeField]
		private int i;

		[SerializeField]
		private float p;

		[SerializeField]
		private int aMin;

		[SerializeField]
		private int aMax;

		public int Chestid
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public GroupType GroupType
		{
			get
			{
				GroupType? groupType = grouptype;
				return (!groupType.HasValue) ? ((GroupType)Enum.Parse(typeof(GroupType), gT)) : groupType.Value;
			}
			set
			{
				grouptype = value;
			}
		}

		public ItemType ItemType
		{
			get
			{
				ItemType? itemType = itemtype;
				return (!itemType.HasValue) ? ((ItemType)Enum.Parse(typeof(ItemType), iT)) : itemType.Value;
			}
			set
			{
				itemtype = value;
			}
		}

		public Rarity Rarity
		{
			get
			{
				Rarity? rarity = this.rarity;
				return (!rarity.HasValue) ? ((Rarity)Enum.Parse(typeof(Rarity), r)) : rarity.Value;
			}
			set
			{
				rarity = value;
			}
		}

		public int Index
		{
			get
			{
				return i;
			}
			set
			{
				i = value;
			}
		}

		public float Probability
		{
			get
			{
				return p;
			}
			set
			{
				p = value;
			}
		}

		public int Amountmin
		{
			get
			{
				return aMin;
			}
			set
			{
				aMin = value;
			}
		}

		public int Amountmax
		{
			get
			{
				return aMax;
			}
			set
			{
				aMax = value;
			}
		}

		public ChestRewardData ConvertToChestRewardData()
		{
			return new ChestRewardData
			{
				Group = GroupType,
				Type = ItemType,
				Rarity = Rarity,
				Index = Index,
				Probability = Probability,
				AmountMin = Amountmin,
				AmountMax = Amountmax
			};
		}
	}
}
