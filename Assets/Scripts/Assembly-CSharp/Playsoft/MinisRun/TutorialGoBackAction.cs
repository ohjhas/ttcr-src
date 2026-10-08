using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Actions/GoBack", fileName = "Action_GoBack")]
	public class TutorialGoBackAction : TutorialAction
	{
		[SerializeField]
		private ScreenId expectedScreenId;

		[SerializeField]
		private float delay;

		public override void Execute()
		{
			Contexts.sharedInstance.state.CreateEntity().AddGoBackAction(expectedScreenId, delay);
		}
	}
}
