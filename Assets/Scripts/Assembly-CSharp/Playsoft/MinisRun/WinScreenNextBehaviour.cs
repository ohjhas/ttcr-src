using UnityEngine;

namespace Playsoft.MinisRun
{
	public class WinScreenNextBehaviour : TutorialElementBehaviour
	{
		private const float TIME_TO_WAIT_BEFORE_HAND = 5f;

		[SerializeField]
		private RectTransform handTarget;

		private bool isShowingHand;

		private float waitTime;

		protected override void Update()
		{
			base.Update();
			CheckIdleTime();
			if (ShouldShowHand())
			{
				if (!isShowingHand)
				{
					ShowHand(handTarget, -135f);
					isShowingHand = true;
				}
			}
			else
			{
				HideHand();
				isShowingHand = false;
			}
		}

		private void CheckIdleTime()
		{
			if (Contexts.sharedInstance.state.activeScreen.ScreenId == ScreenId.WinScreen)
			{
				waitTime += Time.unscaledDeltaTime;
			}
			else
			{
				waitTime = 0f;
			}
		}

		private bool ShouldShowHand()
		{
			return !TutorialHelpers.IsTutorialLevelPassed(TutorialOutgameLevel.PlayAgain) && waitTime >= 5f;
		}

		protected override bool ShouldHighlight()
		{
			return !TutorialHelpers.IsTutorialLevelPassed(TutorialOutgameLevel.PlayAgain);
		}
	}
}
