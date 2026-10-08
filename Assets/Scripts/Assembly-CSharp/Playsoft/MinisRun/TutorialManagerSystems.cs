namespace Playsoft.MinisRun
{
	public class TutorialManagerSystems : Feature
	{
		public TutorialManagerSystems(Contexts contexts)
			: base("Tutorial Manager Systems")
		{
			Add(new TutorialInitializeSystem(contexts));
			Add(new TutorialManageQueueSystem(contexts));
			Add(new TutorialEnableStepSystem(contexts));
			Add(new TutorialDisableStepSystem(contexts));
			Add(new TutorialFindTargetSystem(contexts));
			Add(new TutorialMoveTargetToTopSystem(contexts));
			Add(new TutorialDisplayLockLayerSystem(contexts));
			Add(new TutorialReplaceTargetEventSystem(contexts));
			Add(new TutorialShowHandSystem(contexts));
			Add(new TutorialHiglightTartgetSystem(contexts));
			Add(new TutorialCloseScreenSystem(contexts));
			Add(new TutorialGoBackSystem(contexts));
			Add(new TutorialPassLevelSystem(contexts));
			Add(new TutorialTriggerTargetClickedSystem(contexts));
			Add(new TutorialTriggerLevelPassedSystem(contexts));
			Add(new TutorialTriggerScreenDisplayedSystem(contexts));
			Add(new TutorialTriggerTutorialLevelUnlockedSystem(contexts));
			Add(new TutorialTriggerCharacterUpgradePossibleSystem(contexts));
			Add(new TutorialSafetyDisableStepSystem(contexts));
			Add(new TutorialSaveProgressSystem(contexts));
			Add(new TutorialNotifyLevelUnlockedSystem(contexts));
		}
	}
}
