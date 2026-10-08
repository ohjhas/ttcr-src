namespace Playsoft.MinisRun
{
	public class PowerupSystems : Feature
	{
		public PowerupSystems(Contexts contexts)
			: base("Powerups Systems")
		{
			Add(new MagnetPowerupSystem(contexts));
			Add(new GiantPowerupSystem(contexts));
			Add(new SpeedupPowerupSystem(contexts));
			Add(new ScoreMultiplierPowerupSystem(contexts));
			Add(new UpgradeScorePowerupSystem(contexts));
			Add(new UpgradeSingleScorePowerupSystem(contexts));
			Add(new PowerupRemovedSystem(contexts));
		}
	}
}
