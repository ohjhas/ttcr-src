using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CharacterSelectionSelectBehaviour : TutorialElementBehaviour
	{
		[SerializeField]
		private CharSelectScreen characterSelectScreen;

		protected override bool ShouldHighlight()
		{
			return TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.SelectCharacter) && characterSelectScreen.CurrentCharId == 1;
		}
	}
}
