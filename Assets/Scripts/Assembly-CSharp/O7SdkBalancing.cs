using System;
using UnityEngine;

[Serializable]
public class O7SdkBalancing : ScriptableObject, IBalancingSheet<O7SdkBalancingData>, IBalancingSheet
{
	[HideInInspector]
	[SerializeField]
	private string sheetName = string.Empty;

	[HideInInspector]
	[SerializeField]
	private string worksheetName = string.Empty;

	public O7SdkBalancingData[] dataArray;

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

	public O7SdkBalancingData[] DataArray
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
			dataArray = new O7SdkBalancingData[0];
		}
	}
}
