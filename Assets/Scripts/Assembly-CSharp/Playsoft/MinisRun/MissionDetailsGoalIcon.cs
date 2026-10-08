using System;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class MissionDetailsGoalIcon
	{
		[SerializeField]
		private TextMeshProUGUI goalIconText;

		public void SetUp(MissionType missionType)
		{
			string name = SettingsService.AssetsSettingsSO.GetMissionIcon(missionType).Sprite.name;
			goalIconText.text = string.Format("<sprite name={0}>", name);
		}
	}
}
