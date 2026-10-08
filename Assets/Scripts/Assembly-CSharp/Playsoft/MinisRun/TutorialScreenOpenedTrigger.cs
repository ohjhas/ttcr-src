using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Triggers/ScreenOpened", fileName = "Trigger_ScreenOpened")]
	public class TutorialScreenOpenedTrigger : TutorialTrigger
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
