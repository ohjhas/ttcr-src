using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Triggers/ScreenClosed", fileName = "Trigger_ScreenClosed")]
	public class TutorialScreenClosedTrigger : TutorialTrigger
	{
		[SerializeField]
		private ScreenId screenId;

		public ScreenId ScreenId
		{
			get
			{
				return screenId;
			}
		}
	}
}
