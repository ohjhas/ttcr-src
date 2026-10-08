namespace Playsoft.MinisRun
{
	public class TriggerSystems : Feature
	{
		public TriggerSystems(Contexts contexts)
			: base("Trigger Systems")
		{
			Add(new CollisionCameraSizeSystem(contexts));
			Add(new WorldChangeSkinSystem(contexts));
			Add(new WorldChangeSpeedSystem(contexts));
			Add(new LevelEndReachedSystem(contexts));
			Add(new TutorialTriggerSystem(contexts));
		}
	}
}
