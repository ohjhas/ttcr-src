using System;
using UnityEngine;

[Serializable]
public class PlayerLevelExperienceAmountBalancingData
{
	[SerializeField]
	private PlayerExperienceType playerexperiencetype;

	[SerializeField]
	private int level;

	[SerializeField]
	private int expamount;

	public PlayerExperienceType PLAYEREXPERIENCETYPE
	{
		get
		{
			return playerexperiencetype;
		}
		set
		{
			playerexperiencetype = value;
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

	public int Expamount
	{
		get
		{
			return expamount;
		}
		set
		{
			expamount = value;
		}
	}
}
