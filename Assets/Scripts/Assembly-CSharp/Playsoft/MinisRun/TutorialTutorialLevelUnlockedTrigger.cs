using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Triggers/TutorialLevelUnlocked", fileName = "Trigger_TutorialLevelUnlocked")]
	public class TutorialTutorialLevelUnlockedTrigger : TutorialTrigger
	{
		[SerializeField]
		private TutorialOutgameLevel tutorialLevel;

		public TutorialOutgameLevel TutorialLevel
		{
			get
			{
				return tutorialLevel;
			}
		}
	}
}
