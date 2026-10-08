namespace Playsoft.MinisRun
{
	public class GoButtonBehaviour : TutorialElementBehaviour
	{
		protected override bool ShouldHighlight()
		{
			return !TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.GoToMission);
		}
	}
}
