using System;
using UnityEngine;

[Serializable]
public class O7SdkBalancingData
{
	[SerializeField]
	private int ratemeshowfrequency;

	[SerializeField]
	private int ratemefirsttime;

	[SerializeField]
	private int ratemesecondtime;

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

	public int Ratemefirsttime
	{
		get
		{
			return ratemefirsttime;
		}
		set
		{
			ratemefirsttime = value;
		}
	}

	public int Ratemesecondtime
	{
		get
		{
			return ratemesecondtime;
		}
		set
		{
			ratemesecondtime = value;
		}
	}
}
