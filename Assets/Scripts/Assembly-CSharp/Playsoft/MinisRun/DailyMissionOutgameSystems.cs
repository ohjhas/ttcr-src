namespace Playsoft.MinisRun
{
	public class DailyMissionOutgameSystems : Feature
	{
		public DailyMissionOutgameSystems(Contexts contexts)
			: base("Daily Mission Systems")
		{
			Add(new DailyMissionUpgradeCharacterSystem(contexts));
			Add(new DailyMissionUpgradeCompanionSystem(contexts));
			Add(new DailyMissionOpenChestSystem(contexts));
		}
	}
}
