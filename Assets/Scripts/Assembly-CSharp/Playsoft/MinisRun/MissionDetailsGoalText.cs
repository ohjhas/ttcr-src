using System;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class MissionDetailsGoalText
	{
		[SerializeField]
		private TextMeshProUGUI goalText;

		public void SetUp(MissionType missionType)
		{
			goalText.text = MissionHelper.GetMissionNameByType(missionType).ToUpper();
		}
	}
}
