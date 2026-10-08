namespace Playsoft.MinisRun
{
	public class LogIngameSystems : Feature
	{
		public LogIngameSystems(Contexts contexts)
			: base("Log Ingame Systems")
		{
			Add(new LogMissionEndSystem(contexts));
			Add(new LogGameFinishedSystem(contexts));
		}
	}
}
