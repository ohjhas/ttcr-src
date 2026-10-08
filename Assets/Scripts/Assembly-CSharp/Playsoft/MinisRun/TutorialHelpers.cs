using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public static class TutorialHelpers
	{
		public const int SELECT_CHARACTER_TUTORIAL_CHARACTER_ID = 1;

		public const int SELECT_COMPANION_TUTORIAL_COMPANION_ID = 0;

		public const int SHOP_TUTORIAL_CHEST_ENTRY_ID = 1;

		public const int SHOP_TUTORIAL_TUTORIAL_CHEST_ID = 7;

		public const int UPGRADE_CHARACTER_TUTORIAL_CHARACTER_ID = 0;

		public static TutorialLayer TutorialLayer
		{
			get
			{
				return (Contexts.sharedInstance.uI == null || Contexts.sharedInstance.uI.uIEntity == null || Contexts.sharedInstance.uI.uIEntity.tutorialLayer == null) ? null : Contexts.sharedInstance.uI.uIEntity.tutorialLayer.layer;
			}
		}

		public static bool IsTutorialLevelUnlocked(int levelNumber, TutorialOutgameLevel tutorialLevel)
		{
			return levelNumber > Contexts.sharedInstance.state.balancingDataEntity.tutorialOutgameUnlockLevelsBalancingData.Map[tutorialLevel].PassedLevelId;
		}

		public static bool IsTutorialLevelUnlocked(TutorialOutgameLevel tutorialLevel)
		{
			if (Contexts.sharedInstance.state.tutorialEntity.isTutorialDisabled)
			{
				return true;
			}
			if (!Contexts.sharedInstance.state.tutorialEntity.isTutorialInitialized)
			{
				return false;
			}
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("UnlockedLevel").SingleEntity();
			return IsTutorialLevelUnlocked((stateEntity == null) ? (-1) : stateEntity.integer.Amount, tutorialLevel);
		}

		public static bool IsTutorialActive()
		{
			return Contexts.sharedInstance.state.tutorialEntity.hasTutorialActiveStep && Contexts.sharedInstance.state.tutorialEntity.tutorialActiveStep.Step != null;
		}

		public static bool IsTutorialLevelAvailable(TutorialOutgameLevel tutorialLevel)
		{
			if (!Contexts.sharedInstance.state.tutorialEntity.isTutorialInitialized)
			{
				return false;
			}
			return IsTutorialLevelUnlocked(tutorialLevel) && !IsTutorialLevelPassed(tutorialLevel);
		}

		public static bool IsTutorialLevelPassed(TutorialOutgameLevel tutorialLevel)
		{
			if (!Contexts.sharedInstance.state.tutorialEntity.isTutorialInitialized)
			{
				return false;
			}
			string value = Contexts.sharedInstance.state.GetEntitiesWithName("TutorialProgress").SingleEntity().stringData.Value;
			TutorialProgressData tutorialProgressData = JsonUtility.FromJson<TutorialProgressData>(value) ?? new TutorialProgressData();
			return tutorialProgressData.IsTutorialLevelPassed(tutorialLevel);
		}
	}
}
