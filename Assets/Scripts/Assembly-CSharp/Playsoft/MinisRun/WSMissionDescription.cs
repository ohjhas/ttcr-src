using System;
using I2.Loc;
using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSMissionDescription
	{
		[SerializeField]
		private GameObject Container;

		[SerializeField]
		private TextMeshProUGUI Description;

		public void SetData(MissionType missionType, int[] itemsToCollect)
		{
			if (!(Description == null))
			{
				string name = SettingsService.AssetsSettingsSO.GetMissionIcon(missionType).Sprite.name;
				string text = ((missionType != MissionType.Have_Stamina) ? ScriptLocalization.mission_collect_title : ScriptLocalization.mission_have_stamina_title);
				Description.text = text + Environment.NewLine + "<size=+10><sprite name=\"" + name + "\"> x " + itemsToCollect[2] + "</size>";
			}
		}

		public void SetState(bool isEnabled)
		{
			Container.SetActive(isEnabled);
		}
	}
}
