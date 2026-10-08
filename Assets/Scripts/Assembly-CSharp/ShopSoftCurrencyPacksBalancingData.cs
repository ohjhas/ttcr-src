using System;
using UnityEngine;

[Serializable]
public class ShopSoftCurrencyPacksBalancingData
{
	[SerializeField]
	private int id;

	[SerializeField]
	private string name;

	[SerializeField]
	private string image;

	[SerializeField]
	private int amount;

	[SerializeField]
	private int price;

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

	public string Image
	{
		get
		{
			return image;
		}
		set
		{
			image = value;
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

	public int Price
	{
		get
		{
			return price;
		}
		set
		{
			price = value;
		}
	}
}
