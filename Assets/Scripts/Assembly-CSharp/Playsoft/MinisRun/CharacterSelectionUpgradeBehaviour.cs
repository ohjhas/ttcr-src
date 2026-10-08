namespace Playsoft.MinisRun
{
	public class CharacterSelectionUpgradeBehaviour : TutorialElementBehaviour
	{
		protected override bool ShouldHighlight()
		{
			return TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.UpgradeCharacter) && Contexts.sharedInstance.state.activeScreen.ScreenId == ScreenId.CharSelectScreen;
		}
	}
}
