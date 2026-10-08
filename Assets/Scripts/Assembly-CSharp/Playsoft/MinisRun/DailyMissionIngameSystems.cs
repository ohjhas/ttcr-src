namespace Playsoft.MinisRun
{
	public class DailyMissionIngameSystems : Feature
	{
		public DailyMissionIngameSystems(Contexts contexts)
			: base("Daily Mission Systems")
		{
			Add(new DailyMissionInitSystem(contexts));
			Add(new DailyMissionCountTokenSystem(contexts));
			Add(new DailyMissionResultSendSystem(contexts));
			Add(new DailyMissionDestroyObstaclesSystem(contexts));
		}
	}
}
