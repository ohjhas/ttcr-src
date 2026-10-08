using Playsoft.MinisRun;

public sealed class ViewSystems : Feature
{
	public ViewSystems(Contexts contexts)
		: base("View Systems")
	{
		Add(new AddSparklesSystem(contexts));
		Add(new AddParticlesSystem(contexts));
		Add(new SetupBackgroundSystem(contexts));
		Add(new ReplaceLandAssetSystem(contexts));
		Add(new AddPoolableViewSystem(contexts));
		Add(new RemovePooledViewSystem(contexts));
		Add(new ClearPoolViewSystem(contexts));
		Add(new ResetPoolableViewSystem(contexts));
		Add(new PoolOnSpawnSystem(contexts));
		Add(new AddViewSystem(contexts));
		Add(new ViewSoundSystems(contexts));
		Add(new PlayerSpawnSystem(contexts));
		Add(new ObstacleHitFeedbackSystem(contexts));
		Add(new PlayerHitAnimSystem(contexts));
		Add(new PlayerLandFeedbackSystem(contexts));
		Add(new PlayerBlinkSystem(contexts));
		Add(new AnimationSpeedSystem(contexts));
		Add(new ScoreBlinkSystem(contexts));
		Add(new PlayerAbilityStateSystem(contexts));
		Add(new PlayerRunAnimSystem(contexts));
		Add(new PlayerAbilityRunAnimSystem(contexts));
		Add(new PlayerSpecialRunStartSystem(contexts));
		Add(new PlayerSpecialRunAnimSystem(contexts));
		Add(new PlayerAngryRunAnimSystem(contexts));
		Add(new RaccoonAnimationInitSystem(contexts));
		Add(new RaccoonCollectibleInitSystem(contexts));
		Add(new RaccoonCollectibleOnDestroySystem(contexts));
		Add(new RaccoonCollectibleAnimationSystem(contexts));
		Add(new SetViewPositionSystem(contexts));
		Add(new SetSwingingObjSystem(contexts));
		Add(new MovePositionSystem(contexts));
		Add(new RefreshScoreSystem(contexts));
		Add(new ShowGainedScoreSystem(contexts));
		Add(new FadeOutObjSystem(contexts));
		Add(new PlayerKeepPositionSystem(contexts));
		Add(new CameraMoveSystem(contexts));
		Add(new ChangeViewSizeSystem(contexts));
		Add(new WorldFinishLevelSystem(contexts));
		Add(new RemoveViewSystem(contexts));
	}
}
