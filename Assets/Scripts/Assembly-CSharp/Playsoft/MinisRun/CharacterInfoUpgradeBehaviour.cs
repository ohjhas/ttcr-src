namespace Playsoft.MinisRun
{
	public class CharacterInfoUpgradeBehaviour : TutorialElementBehaviour
	{
		protected override bool ShouldHighlight()
		{
			return TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.UpgradeCharacter);
		}
	}
}
