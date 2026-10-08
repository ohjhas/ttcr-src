namespace Playsoft.MinisRun
{
	public class CompanionSystems : Feature
	{
		public CompanionSystems(Contexts contexts)
			: base("Companion Systems")
		{
			Add(new CompanionInitSystem(contexts));
			Add(new CompanionSetupAbilitySystem(contexts));
			Add(new CompanionFollowSystem(contexts));
			Add(new CompanionDieSystem(contexts));
			Add(new CompanionRelaySystem(contexts));
			Add(new CompanionReviveSystem(contexts));
			Add(new CompanionCreateTokenAbilitySystem(contexts));
			Add(new CompanionDragonBlastAbilitySystem(contexts));
			Add(new CompanionSpeedupAbilitySystem(contexts));
			Add(new CompanionRandomPowerupAbilitySystem(contexts));
			Add(new CompanionShieldAbilitySystem(contexts));
		}
	}
}
