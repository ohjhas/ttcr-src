using System;
using UnityEngine;

[Serializable]
public class PlayerLevelRewardsBalancingData
{
	[SerializeField]
	private int level;

	[SerializeField]
	private ItemType itemtype;

	[SerializeField]
	private int itemindex;

	[SerializeField]
	private int amount;

	public int Level
	{
		get
		{
			return level;
		}
		set
		{
			level = value;
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

	public int Itemindex
	{
		get
		{
			return itemindex;
		}
		set
		{
			itemindex = value;
		}
	}

	public int Amount
	{
		get
		{
			return amount;
		}
		set
		{
			amount = value;
		}
	}
}
