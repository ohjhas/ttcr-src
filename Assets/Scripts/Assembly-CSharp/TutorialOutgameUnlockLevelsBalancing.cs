using System;
using UnityEngine;

[Serializable]
public class TutorialOutgameUnlockLevelsBalancing : ScriptableObject, IBalancingSheet<TutorialOutgameUnlockLevelsBalancingData>, IBalancingSheet
{
	[HideInInspector]
	[SerializeField]
	private string sheetName = string.Empty;

	[HideInInspector]
	[SerializeField]
	private string worksheetName = string.Empty;

	public TutorialOutgameUnlockLevelsBalancingData[] dataArray;

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

	public TutorialOutgameUnlockLevelsBalancingData[] DataArray
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
			dataArray = new TutorialOutgameUnlockLevelsBalancingData[0];
		}
	}
}
