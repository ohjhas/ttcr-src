using System;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class MissionDetailsRibbon
	{
		[SerializeField]
		private TextMeshProUGUI levelText;

		public void SetUp(int levelIndex)
		{
			levelText.text = "LEVEL " + levelIndex;
		}
	}
}
