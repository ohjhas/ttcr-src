using Playsoft.MinisRun;

public sealed class GameSystems : Feature
{
	public GameSystems(Contexts contexts)
		: base("Game Systems")
	{
		Add(new PlayerInitSystem(contexts));
		Add(new PlayerMovementSystems(contexts));
		Add(new PlayerRestoreSystems(contexts));
		Add(new PlayerAbilitySystems(contexts));
		Add(new WorldSystems(contexts));
		Add(new CompanionSystems(contexts));
		Add(new LevelElementSystems(contexts));
		Add(new PlayerStaminaSystems(contexts));
		Add(new CollectibleSystems(contexts));
		Add(new MissionSystems(contexts));
		Add(new DailyMissionIngameSystems(contexts));
		Add(new TriggerSystems(contexts));
		Add(new PowerupSystems(contexts));
		Add(new PlayerDieSystem(contexts));
		Add(new RaccoonIntroSystem(contexts));
		Add(new RaccoonFirstIntroSystem(contexts));
		Add(new RaccoonOutroSystem(contexts));
		Add(new RaccoonCloudsExecuteSystem(contexts));
		Add(new PlayGameMusicSystem(contexts));
		Add(new PlayGameWinMusicSystem(contexts));
		Add(new PlayGameFailMusicSystem(contexts));
		Add(new TutorialSystems(contexts));
		Add(new LogIngameSystems(contexts));
	}
}
