namespace Playsoft.MinisRun
{
	public class CharacterSelectionQuitBehaviour : TutorialElementBehaviour
	{
		private bool selectTutorialLevelAvailableAtBegining;

		private bool upgradeTutorialLevelAvailableAtBegining;

		private void OnEnable()
		{
			selectTutorialLevelAvailableAtBegining = TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.SelectCharacter);
			upgradeTutorialLevelAvailableAtBegining = TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.UpgradeCharacter);
		}

		protected override bool ShouldHighlight()
		{
			bool flag = selectTutorialLevelAvailableAtBegining && !TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.SelectCharacter);
			bool flag2 = upgradeTutorialLevelAvailableAtBegining && !TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.UpgradeCharacter);
			return (flag || flag2) && Contexts.sharedInstance.state.activeScreen.ScreenId == ScreenId.CharSelectScreen;
		}
	}
}
