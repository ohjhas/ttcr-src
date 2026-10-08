namespace Playsoft.MinisRun
{
	public class PlayerAbilitySystems : Feature
	{
		public PlayerAbilitySystems(Contexts contexts)
			: base("Player Abilities")
		{
			Add(new PlayerSetupAbilitySystem(contexts));
			Add(new PlayerAbilityUpgradeScoreSystem(contexts));
			Add(new PlayerAbilityMagnetSystem(contexts));
			Add(new PlayerAbilitySpeedupSystem(contexts));
			Add(new PlayerAbilityGiantSpeedupSystem(contexts));
		}
	}
}
