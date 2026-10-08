namespace Playsoft.MinisRun
{
	public class CompanionSelectionQuirBehaviour : TutorialElementBehaviour
	{
		private bool tutorialLevelAvailableAtBegining;

		private void OnEnable()
		{
			tutorialLevelAvailableAtBegining = TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.SelectCompanion);
		}

		protected override bool ShouldHighlight()
		{
			return tutorialLevelAvailableAtBegining && !TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.SelectCompanion);
		}
	}
}
