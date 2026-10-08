using System;
using UnityEngine;

[Serializable]
public class MissionChestsBalancingData
{
	[SerializeField]
	private int levelindex;

	[SerializeField]
	private int chestid;

	public int Levelindex
	{
		get
		{
			return levelindex;
		}
		set
		{
			levelindex = value;
		}
	}

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
}
