using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Triggers/LevelPassed", fileName = "Trigger_LevelPassed")]
	public class TutorialLevelPassedTrigger : TutorialTrigger
	{
		[SerializeField]
		private int levelId;

		public int LevelId
		{
			get
			{
				return levelId;
			}
		}
	}
}
