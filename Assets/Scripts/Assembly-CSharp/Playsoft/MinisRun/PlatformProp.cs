using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class PlatformProp : AbstractProperty
	{
		public MovableProp Movable = new MovableProp();

		public RestableAreaProp RestableArea = new RestableAreaProp();

		public DroppingProp Dropable = new DroppingProp();

		public ShakingProp Shakeable = new ShakingProp();

		public FloatingProp Floating = new FloatingProp();

		public PlatformProp()
		{
		}

		public PlatformProp(PlatformProp platform)
		{
			IsEnabled = platform.IsEnabled;
			Movable = new MovableProp(platform.Movable);
			RestableArea = new RestableAreaProp(platform.RestableArea);
			Dropable = new DroppingProp(platform.Dropable);
			Shakeable = new ShakingProp(platform.Shakeable);
			Floating = new FloatingProp(platform.Floating);
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.isPlatform = true;
				Movable.AddToEntity(entity);
				RestableArea.AddToEntity(entity);
				Dropable.AddToEntity(entity);
				Shakeable.AddToEntity(entity);
				Floating.AddToEntity(entity);
			}
		}
	}
}
