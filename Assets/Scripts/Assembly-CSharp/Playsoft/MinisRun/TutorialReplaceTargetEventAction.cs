using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Actions/ReplaceTargetEvent", fileName = "Action_ReplaceTargetEvent")]
	public class TutorialReplaceTargetEventAction : TutorialAction
	{
		[SerializeField]
		private TutorialAction[] actions;

		public override void Execute()
		{
			Contexts.sharedInstance.state.CreateEntity().AddReplaceTargetEvent(actions);
		}
	}
}
