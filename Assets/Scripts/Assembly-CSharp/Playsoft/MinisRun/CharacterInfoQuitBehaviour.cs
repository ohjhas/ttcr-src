namespace Playsoft.MinisRun
{
	public class CharacterInfoQuitBehaviour : TutorialElementBehaviour
	{
		private bool tutorialLevelAvailableAtBegining;

		private void OnEnable()
		{
			tutorialLevelAvailableAtBegining = TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.UpgradeCharacter);
		}

		protected override bool ShouldHighlight()
		{
			return tutorialLevelAvailableAtBegining && !TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.UpgradeCharacter);
		}
	}
}
