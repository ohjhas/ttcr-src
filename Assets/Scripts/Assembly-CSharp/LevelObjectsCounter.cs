using System.Collections.Generic;
using Playsoft.MinisRun;
using UnityEngine;

[ExecuteInEditMode]
public class LevelObjectsCounter : MonoBehaviour
{
	public Dictionary<string, int> objectsDict = new Dictionary<string, int>();

	public void CountObjects()
	{
		objectsDict = new Dictionary<string, int>();
		LevelElement[] array = Object.FindObjectsOfType<LevelElement>();
		for (int i = 0; i < array.Length; i++)
		{
			if (string.IsNullOrEmpty(array[i].Data.AssetName))
			{
				array[i].OnBeforeSerialize();
			}
			AddElementToDictionary(array[i].Data.AssetName);
		}
	}

	private void AddElementToDictionary(string assetName)
	{
		if (objectsDict.ContainsKey(assetName))
		{
			objectsDict[assetName]++;
		}
		else
		{
			objectsDict.Add(assetName, 1);
		}
	}
}
