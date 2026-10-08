using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionSelectionSelectBehaviour : TutorialElementBehaviour
	{
		[SerializeField]
		private CompSelectScreen companionSelectScreen;

		protected override bool ShouldHighlight()
		{
			return TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.SelectCompanion) && companionSelectScreen.CurrentCompId == 0;
		}
	}
}
