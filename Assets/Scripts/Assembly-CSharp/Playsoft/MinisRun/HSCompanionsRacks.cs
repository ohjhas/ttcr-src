using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class HSCompanionsRacks
	{
		[SerializeField]
		private List<GameObject> Companions;

		public void RefreshRacks(List<CSCompanionData> compDataList, int selectedCompId)
		{
			for (int i = 0; i < Companions.Count; i++)
			{
				if (i < compDataList.Count)
				{
					Companions[i].SetActive(compDataList[i].Unlocked && i != selectedCompId);
				}
				else
				{
					Companions[i].SetActive(false);
				}
			}
		}
	}
}
