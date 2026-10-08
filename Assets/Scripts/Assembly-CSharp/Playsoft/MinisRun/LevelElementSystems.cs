namespace Playsoft.MinisRun
{
	public class LevelElementSystems : Feature
	{
		public LevelElementSystems(Contexts contexts)
			: base("Level Elements")
		{
			Add(new FloatingObjSystem(contexts));
			Add(new MovingObjSystem(contexts));
			Add(new ApplySpeedToObjSystem(contexts));
			Add(new SwingingObjSystem(contexts));
			Add(new RestableAreaMoveSystem(contexts));
			Add(new ShakingObjSystem(contexts));
			Add(new DroppingObjHitSystem(contexts));
			Add(new DroppingObjSystem(contexts));
			Add(new ObstacleHitSystem(contexts));
			Add(new FlyAwayObjSystem(contexts));
			Add(new UnderMapObjSystem(contexts));
			Add(new RestableAreaHitSystem(contexts));
			Add(new CollidersDisableSystem(contexts));
			Add(new CloudsExecuteSystem(contexts));
			Add(new DestroyZoneSystem(contexts));
		}
	}
}
