namespace Playsoft.MinisRun
{
	public class PlayerProgressionSystems : Feature
	{
		public PlayerProgressionSystems(Contexts contexts)
			: base("Player Progression Systems")
		{
			Add(new UpgradeCharacterCheckExperienceSystem(contexts));
			Add(new UpgradeCompanionCheckExperienceSystem(contexts));
			Add(new OpenChestCheckExperienceSystem(contexts));
			Add(new UnlockChapterCheckExperienceSystem(contexts));
			Add(new CollectStarsCheckExperienceSystem(contexts));
			Add(new InitializePlayerLevelSystem(contexts));
			Add(new GrantPlayerExperienceSystem(contexts));
			Add(new PlayerProgressionStateSystem(contexts));
			Add(new RefreshPlayerLevelSystem(contexts));
			Add(new ShowLevelUpScreenSystem(contexts));
		}
	}
}
