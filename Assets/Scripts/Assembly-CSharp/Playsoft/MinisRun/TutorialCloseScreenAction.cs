using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Actions/CloseScreen", fileName = "Action_CloseScreen")]
	public class TutorialCloseScreenAction : TutorialAction
	{
		[SerializeField]
		private ScreenId screenId;

		[SerializeField]
		private float delay;

		public override void Execute()
		{
			Contexts.sharedInstance.state.CreateEntity().AddCloseScreenAction(screenId, delay);
		}
	}
}
