namespace Playsoft.MinisRun
{
	public class CollectibleSystems : Feature
	{
		public CollectibleSystems(Contexts contexts)
			: base("Collectible Systems")
		{
			Add(new CollectibleHitSystem(contexts));
			Add(new RaccoonCollectibleHitSystem(contexts));
			Add(new RacoonIntroChangePlayerAnimationSystem(contexts));
			Add(new CollectibleScoreSystem(contexts));
			Add(new CollectibleStaminaSystem(contexts));
			Add(new CollectibleCoinSystem(contexts));
			Add(new CollectibleGiantTokenSystem(contexts));
			Add(new CollectibleMagnetSystem(contexts));
			Add(new CollectibleSpeedUpSystem(contexts));
			Add(new CollectibleScoreMultiplierSystem(contexts));
			Add(new CollectibleUpgradeScoreSystem(contexts));
		}
	}
}
