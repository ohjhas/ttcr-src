using System;
using UnityEngine;

[Serializable]
public class SDKValuesBalancingData
{
	[SerializeField]
	private int ratemeshowfrequency;

	public int Ratemeshowfrequency
	{
		get
		{
			return ratemeshowfrequency;
		}
		set
		{
			ratemeshowfrequency = value;
		}
	}
}
