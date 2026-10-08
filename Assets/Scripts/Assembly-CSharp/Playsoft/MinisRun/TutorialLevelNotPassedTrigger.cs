using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(menuName = "Tutorial/Triggers/LevelNotPassed", fileName = "Trigger_LevelNotPassed")]
	public class TutorialLevelNotPassedTrigger : TutorialTrigger
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
