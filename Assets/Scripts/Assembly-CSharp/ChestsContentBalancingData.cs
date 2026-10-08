using System;
using UnityEngine;

[Serializable]
public class ChestsContentBalancingData
{
	[SerializeField]
	private int chestid;

	[SerializeField]
	private GroupType grouptype;

	[SerializeField]
	private ItemType itemtype;

	[SerializeField]
	private Rarity rarity;

	[SerializeField]
	private int index;

	[SerializeField]
	private float probability;

	[SerializeField]
	private int amountmin;

	[SerializeField]
	private int amountmax;

	public int Chestid
	{
		get
		{
			return chestid;
		}
		set
		{
			chestid = value;
		}
	}

	public GroupType GROUPTYPE
	{
		get
		{
			return grouptype;
		}
		set
		{
			grouptype = value;
		}
	}

	public ItemType ITEMTYPE
	{
		get
		{
			return itemtype;
		}
		set
		{
			itemtype = value;
		}
	}

	public Rarity RARITY
	{
		get
		{
			return rarity;
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
			return index;
		}
		set
		{
			index = value;
		}
	}

	public float Probability
	{
		get
		{
			return probability;
		}
		set
		{
			probability = value;
		}
	}

	public int Amountmin
	{
		get
		{
			return amountmin;
		}
		set
		{
			amountmin = value;
		}
	}

	public int Amountmax
	{
		get
		{
			return amountmax;
		}
		set
		{
			amountmax = value;
		}
	}
}
