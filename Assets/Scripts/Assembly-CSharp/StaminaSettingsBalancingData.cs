using System;
using UnityEngine;

[Serializable]
public class StaminaSettingsBalancingData
{
	[SerializeField]
	private StaminaSettings staminasettings;

	[SerializeField]
	private float amount;

	public StaminaSettings STAMINASETTINGS
	{
		get
		{
			return staminasettings;
		}
		set
		{
			staminasettings = value;
		}
	}

	public float Amount
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
