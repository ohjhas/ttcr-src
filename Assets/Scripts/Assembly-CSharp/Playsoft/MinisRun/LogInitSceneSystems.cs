namespace Playsoft.MinisRun
{
	public class LogInitSceneSystems : Feature
	{
		public LogInitSceneSystems(Contexts contexts)
			: base("Log Init Scene Systems")
		{
			Add(new LogAgeGateQuestionSystem(contexts));
			Add(new LogGenderGateQuestionSystem(contexts));
		}
	}
}
