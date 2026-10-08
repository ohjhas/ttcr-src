using Playsoft.MinisRun;

public sealed class ViewSoundSystems : Feature
{
	public ViewSoundSystems(Contexts contexts)
		: base("View Sound Systems")
	{
		Add(new PlayCollectSoundSystem(contexts));
		Add(new PlayJumpSoundSystem(contexts));
		Add(new PlaySlideSoundSystem(contexts));
		Add(new PlayHitImpactSoundSystem(contexts));
		Add(new PlayMagnetizeSoundSystem(contexts));
		Add(new PlayObstacleCrushSoundSystem(contexts));
		Add(new PlayGrowUpSoundSystem(contexts));
		Add(new PlayMegaLandSoundSystem(contexts));
		Add(new PlayMegaStepSoundSystem(contexts));
	}
}
