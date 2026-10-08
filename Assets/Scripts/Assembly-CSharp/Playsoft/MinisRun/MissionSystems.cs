namespace Playsoft.MinisRun
{
	public class MissionSystems : Feature
	{
		public MissionSystems(Contexts contexts)
			: base("Mission Systems")
		{
			Add(new MissionInitSystem(contexts));
			Add(new MissionCountTokenSystem(contexts));
			Add(new MissionScoreSystem(contexts));
			Add(new MissionStaminaSystem(contexts));
			Add(new MissionDestroyObstaclesSystem(contexts));
			Add(new MissionResultSendSystem(contexts));
		}
	}
}
