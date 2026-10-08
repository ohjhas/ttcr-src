namespace Playsoft.MinisRun
{
	public class TutorialSystems : Feature
	{
		public TutorialSystems(Contexts contexts)
			: base("Tutorial Systems")
		{
			Add(new TutorialLevelInitializeSystem(contexts));
			Add(new TutorialTimeControlSystem(contexts));
			Add(new TutorialInformUiSystem(contexts));
			Add(new TutorialSkipSingleJumpSystem(contexts));
			Add(new TutorialSingleJumpSystem(contexts));
			Add(new TutorialEndSlideSystem(contexts));
			Add(new TutorialSlideSystem(contexts));
			Add(new TutorialSkipDoubleJumpSystem(contexts));
			Add(new TutorialDoubleJumpSystem(contexts));
			Add(new TutorialObstacleHitFinishSystem(contexts));
			Add(new TutorialStaminaSystem(contexts));
		}
	}
}
