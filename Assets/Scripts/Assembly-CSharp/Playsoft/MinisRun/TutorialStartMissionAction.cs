using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Actions/StartMission", fileName = "Action_StartMission")]
	public class TutorialStartMissionAction : TutorialAction
	{
		[SerializeField]
		private int levelId;

		public override void Execute()
		{
			SettingsService.SettingsSO.WorldIndex = levelId;
			Contexts.sharedInstance.uI.CreateEntity().AddChangeScene(ProjectConstants.INGAME_SCENE_NAME, false);
		}
	}
}
