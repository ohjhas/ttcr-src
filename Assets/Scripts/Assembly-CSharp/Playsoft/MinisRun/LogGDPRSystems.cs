namespace Playsoft.MinisRun
{
	public class LogGDPRSystems : Feature
	{
		public LogGDPRSystems(Contexts contexts)
			: base("Log GDPR")
		{
			Add(new LogGDPRSelectionDecisionSystem(contexts));
			Add(new LogGDPRQuestionSystem(contexts));
			Add(new LogGDPRProviderDecisionSystem(contexts));
		}
	}
}
