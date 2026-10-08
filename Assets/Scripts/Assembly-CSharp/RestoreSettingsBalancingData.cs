using System;
using UnityEngine;

[Serializable]
public class RestoreSettingsBalancingData
{
	[SerializeField]
	private RestoreSettings restoresettings;

	[SerializeField]
	private int amount;

	public RestoreSettings RESTORESETTINGS
	{
		get
		{
			return restoresettings;
		}
		set
		{
			restoresettings = value;
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
