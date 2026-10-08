using System;
using UnityEngine;

[Serializable]
public class PlayerLevelRewardsBalancing : ScriptableObject, IBalancingSheet<PlayerLevelRewardsBalancingData>, IBalancingSheet
{
	[HideInInspector]
	[SerializeField]
	private string sheetName = string.Empty;

	[HideInInspector]
	[SerializeField]
	private string worksheetName = string.Empty;

	public PlayerLevelRewardsBalancingData[] dataArray;

	public string SheetName
	{
		get
		{
			return sheetName;
		}
		set
		{
			sheetName = value;
		}
	}

	public string WorksheetName
	{
		get
		{
			return worksheetName;
		}
		set
		{
			worksheetName = value;
		}
	}

	public PlayerLevelRewardsBalancingData[] DataArray
	{
		get
		{
			return dataArray;
		}
		set
		{
			dataArray = value;
		}
	}

	private void OnEnable()
	{
		if (dataArray == null)
		{
			dataArray = new PlayerLevelRewardsBalancingData[0];
		}
	}
}
