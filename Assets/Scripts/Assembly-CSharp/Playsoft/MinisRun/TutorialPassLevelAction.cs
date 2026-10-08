using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Actions/PassTutorialLevel", fileName = "Action_PassTutorialLevel")]
	public class TutorialPassLevelAction : TutorialAction
	{
		[SerializeField]
		private TutorialOutgameLevel tutorialLevel;

		public override void Execute()
		{
			Contexts.sharedInstance.state.CreateEntity().AddPassLevelAction(tutorialLevel);
		}
	}
}
