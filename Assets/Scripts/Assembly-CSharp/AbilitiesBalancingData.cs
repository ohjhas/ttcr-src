using System;
using UnityEngine;

[Serializable]
public class AbilitiesBalancingData
{
	[SerializeField]
	private AbilityType abilitytype;

	[SerializeField]
	private string name;

	[SerializeField]
	private string assetname;

	[SerializeField]
	private string description;

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

	public string Description
	{
		get
		{
			return description;
		}
		set
		{
			description = value;
		}
	}
}
