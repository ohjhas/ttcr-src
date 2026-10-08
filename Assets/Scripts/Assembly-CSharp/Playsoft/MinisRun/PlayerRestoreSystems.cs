namespace Playsoft.MinisRun
{
	public class PlayerRestoreSystems : Feature
	{
		public PlayerRestoreSystems(Contexts contexts)
			: base("Player Restore")
		{
			Add(new PlayerRelaySystem(contexts));
			Add(new PlayerResurrectSystem(contexts));
			Add(new PlayerReviveSystem(contexts));
			Add(new PlayerRefreshViewSystem(contexts));
		}
	}
}
