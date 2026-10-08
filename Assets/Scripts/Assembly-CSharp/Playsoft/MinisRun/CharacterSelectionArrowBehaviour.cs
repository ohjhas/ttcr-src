using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CharacterSelectionArrowBehaviour : TutorialElementBehaviour
	{
		[SerializeField]
		private CharSelectScreen characterSelectScreen;

		[SerializeField]
		private bool isRightArrow;

		protected override bool ShouldHighlight()
		{
			bool flag = TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.SelectCharacter);
			int charEntryOrderById = characterSelectScreen.GetCharEntryOrderById(1);
			bool flag2 = ((!isRightArrow) ? (characterSelectScreen.SelectedCharOrder > charEntryOrderById) : (characterSelectScreen.SelectedCharOrder < charEntryOrderById));
			return flag && flag2;
		}
	}
}
