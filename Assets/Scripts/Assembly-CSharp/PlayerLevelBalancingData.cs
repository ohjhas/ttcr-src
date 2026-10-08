using System;
using UnityEngine;

[Serializable]
public class PlayerLevelBalancingData
{
	[SerializeField]
	private int level;

	[SerializeField]
	private int requiredxp;

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

	public int Requiredxp
	{
		get
		{
			return requiredxp;
		}
		set
		{
			requiredxp = value;
		}
	}
}
