using System;
using UnityEngine;

[Serializable]
public class CompanionsStaticBalancingData
{
	[SerializeField]
	private int id;

	[SerializeField]
	private ItemType itemtype;

	[SerializeField]
	private string name;

	[SerializeField]
	private AbilityType abilitytype;

	[SerializeField]
	private Rarity rarity;

	[SerializeField]
	private int minlevel;

	[SerializeField]
	private bool unlocked;

	public int ID
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

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public AbilityType ABILITYTYPE
	{
		get
		{
			return abilitytype;
		}
		set
		{
			abilitytype = value;
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

	public int Minlevel
	{
		get
		{
			return minlevel;
		}
		set
		{
			minlevel = value;
		}
	}

	public bool Unlocked
	{
		get
		{
			return unlocked;
		}
		set
		{
			unlocked = value;
		}
	}
}
