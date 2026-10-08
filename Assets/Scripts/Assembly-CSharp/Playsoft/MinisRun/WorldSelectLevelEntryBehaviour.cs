using UnityEngine;

namespace Playsoft.MinisRun
{
	public class WorldSelectLevelEntryBehaviour : TutorialElementBehaviour
	{
		private const float TIME_TO_WAIT_BEFORE_HAND = 5f;

		[SerializeField]
		private RectTransform handTarget;

		[SerializeField]
		private WSLevelEntry levelEntry;

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
					ShowHand(handTarget, 35f);
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
			if (levelEntry.IsHiglighted && Contexts.sharedInstance.state.activeScreen.ScreenId == ScreenId.SagaMap)
			{
				waitTime += Time.deltaTime;
			}
			else
			{
				waitTime = 0f;
			}
		}

		private bool ShouldShowHand()
		{
			return TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.LevelEntry) && waitTime >= 5f;
		}

		protected override bool ShouldHighlight()
		{
			return false;
		}
	}
}
