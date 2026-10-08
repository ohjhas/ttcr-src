using System;
using UnityEngine;

[Serializable]
public class SDKValuesBalancing : ScriptableObject, IBalancingSheet<SDKValuesBalancingData>, IBalancingSheet
{
	[HideInInspector]
	[SerializeField]
	private string sheetName = string.Empty;

	[HideInInspector]
	[SerializeField]
	private string worksheetName = string.Empty;

	public SDKValuesBalancingData[] dataArray;

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

	public SDKValuesBalancingData[] DataArray
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
			dataArray = new SDKValuesBalancingData[0];
		}
	}
}
