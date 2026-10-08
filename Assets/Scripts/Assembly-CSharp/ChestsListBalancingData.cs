using System;
using UnityEngine;

[Serializable]
public class ChestsListBalancingData
{
	[SerializeField]
	private int chestid;

	[SerializeField]
	private ItemType itemtype;

	[SerializeField]
	private string name;

	[SerializeField]
	private string contentdescription;

	[SerializeField]
	private Rarity rarity;

	[SerializeField]
	private bool shopvisibility;

	[SerializeField]
	private string assetname;

	[SerializeField]
	private int initialwaittime;

	[SerializeField]
	private int waittime;

	[SerializeField]
	private int pricesc;

	[SerializeField]
	private int pricehc;

	[SerializeField]
	private int minpartsamount;

	[SerializeField]
	private int maxpartsamount;

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

	public string Contentdescription
	{
		get
		{
			return contentdescription;
		}
		set
		{
			contentdescription = value;
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

	public bool Shopvisibility
	{
		get
		{
			return shopvisibility;
		}
		set
		{
			shopvisibility = value;
		}
	}

	public string Assetname
	{
		get
		{
			return assetname;
		}
		set
		{
			assetname = value;
		}
	}

	public int Initialwaittime
	{
		get
		{
			return initialwaittime;
		}
		set
		{
			initialwaittime = value;
		}
	}

	public int Waittime
	{
		get
		{
			return waittime;
		}
		set
		{
			waittime = value;
		}
	}

	public int Pricesc
	{
		get
		{
			return pricesc;
		}
		set
		{
			pricesc = value;
		}
	}

	public int Pricehc
	{
		get
		{
			return pricehc;
		}
		set
		{
			pricehc = value;
		}
	}

	public int Minpartsamount
	{
		get
		{
			return minpartsamount;
		}
		set
		{
			minpartsamount = value;
		}
	}

	public int Maxpartsamount
	{
		get
		{
			return maxpartsamount;
		}
		set
		{
			maxpartsamount = value;
		}
	}
}
