using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Actions/StartFirstMission", fileName = "Action_StartFirstMission")]
	public class TutoriaStartFirstMissionAction : TutorialAction
	{
		public override void Execute()
		{
			SettingsService.SettingsSO.WorldIndex = 0;
			Contexts.sharedInstance.uI.CreateEntity().AddChangeScene(ProjectConstants.INGAME_SCENE_NAME, false);
		}
	}
}
