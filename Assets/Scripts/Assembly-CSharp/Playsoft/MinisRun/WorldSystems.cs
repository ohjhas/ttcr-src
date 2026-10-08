namespace Playsoft.MinisRun
{
	public class WorldSystems : Feature
	{
		public WorldSystems(Contexts contexts)
			: base("World Systems")
		{
			Add(new WorldInitSystem(contexts));
			Add(new WorldSlowDownTimeSystem(contexts));
			Add(new WorldMoveSystem(contexts));
			Add(new LevelEndAnimationSystem(contexts));
			Add(new WorldSpawnSystem(contexts));
			Add(new WorldPauseSystem(contexts));
			Add(new WorldRunDurationSystem(contexts));
			Add(new WorldUpdateTimeScaleSystem(contexts));
		}
	}
}
