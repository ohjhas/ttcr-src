namespace Playsoft.MinisRun
{
	public class LogRouterSystems : Feature
	{
		public LogRouterSystems(Contexts contexts)
			: base("Log Router System")
		{
			Add(new LogRouterSystem(contexts));
		}
	}
}
