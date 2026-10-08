using System;
using UnityEngine;

[Serializable]
public class InitConfigBalancingData
{
	[SerializeField]
	private EnumSettings enumsettings;

	[SerializeField]
	private int amount;

	public EnumSettings ENUMSETTINGS
	{
		get
		{
			return enumsettings;
		}
		set
		{
			enumsettings = value;
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
