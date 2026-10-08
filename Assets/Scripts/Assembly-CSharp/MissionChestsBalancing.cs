using System;
using UnityEngine;

[Serializable]
public class MissionChestsBalancing : ScriptableObject, IBalancingSheet<MissionChestsBalancingData>, IBalancingSheet
{
	[HideInInspector]
	[SerializeField]
	private string sheetName = string.Empty;

	[HideInInspector]
	[SerializeField]
	private string worksheetName = string.Empty;

	public MissionChestsBalancingData[] dataArray;

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

	public MissionChestsBalancingData[] DataArray
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
			dataArray = new MissionChestsBalancingData[0];
		}
	}
}
