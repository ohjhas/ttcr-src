using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionSelectionArrowBehaviour : TutorialElementBehaviour
	{
		[SerializeField]
		private CompSelectScreen companionSelectScreen;

		[SerializeField]
		private bool isRightArrow;

		protected override bool ShouldHighlight()
		{
			bool flag = TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.SelectCompanion);
			int compEntryOrderById = companionSelectScreen.GetCompEntryOrderById(0);
			bool flag2 = ((!isRightArrow) ? (companionSelectScreen.CurrentCompanionOrder > compEntryOrderById) : (companionSelectScreen.CurrentCompanionOrder < compEntryOrderById));
			return flag && flag2;
		}
	}
}
