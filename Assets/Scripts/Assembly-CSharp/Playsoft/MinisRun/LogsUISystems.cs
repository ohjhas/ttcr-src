namespace Playsoft.MinisRun
{
	public class LogsUISystems : Feature
	{
		public LogsUISystems(Contexts contexts)
			: base("Log UI Systems")
		{
			Add(new LogStartTrackingScreensSystem(contexts));
			Add(new LogNewSceneSystem(contexts));
			Add(new LogScreenChangedSystem(contexts));
			Add(new LogRateMeSystem(contexts));
			Add(new LogIAPScreenExitSystem(contexts));
			Add(new LogIAPTransactionSystem(contexts));
		}
	}
}
