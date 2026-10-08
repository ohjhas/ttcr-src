using System;
using UnityEngine;

[Serializable]
public class CompanionsUpgradesBalancingData
{
	[SerializeField]
	private int id;

	[SerializeField]
	private int level;

	[SerializeField]
	private int upgradecostsc;

	[SerializeField]
	private int upgradecostparts;

	[SerializeField]
	private float abilityparameter;

	[SerializeField]
	private float abilityduration;

	[SerializeField]
	private float abilitychargetime;

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

	public int Upgradecostsc
	{
		get
		{
			return upgradecostsc;
		}
		set
		{
			upgradecostsc = value;
		}
	}

	public int Upgradecostparts
	{
		get
		{
			return upgradecostparts;
		}
		set
		{
			upgradecostparts = value;
		}
	}

	public float Abilityparameter
	{
		get
		{
			return abilityparameter;
		}
		set
		{
			abilityparameter = value;
		}
	}

	public float Abilityduration
	{
		get
		{
			return abilityduration;
		}
		set
		{
			abilityduration = value;
		}
	}

	public float Abilitychargetime
	{
		get
		{
			return abilitychargetime;
		}
		set
		{
			abilitychargetime = value;
		}
	}
}
