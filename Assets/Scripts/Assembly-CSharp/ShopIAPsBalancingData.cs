using System;
using UnityEngine;

[Serializable]
public class ShopIAPsBalancingData
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
	private string proposedprice;

	[SerializeField]
	private string applestoreid;

	[SerializeField]
	private string googlestoreid;

	[SerializeField]
	private string productid;

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

	public string Proposedprice
	{
		get
		{
			return proposedprice;
		}
		set
		{
			proposedprice = value;
		}
	}

	public string Applestoreid
	{
		get
		{
			return applestoreid;
		}
		set
		{
			applestoreid = value;
		}
	}

	public string Googlestoreid
	{
		get
		{
			return googlestoreid;
		}
		set
		{
			googlestoreid = value;
		}
	}

	public string Productid
	{
		get
		{
			return productid;
		}
		set
		{
			productid = value;
		}
	}
}
