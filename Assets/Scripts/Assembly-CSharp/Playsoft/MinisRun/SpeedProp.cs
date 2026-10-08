using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class SpeedProp : AbstractProperty
	{
		public float Speed;

		public SpeedProp()
		{
		}

		public SpeedProp(SpeedProp speed)
		{
			IsEnabled = speed.IsEnabled;
			Speed = speed.Speed;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddSpeedData(Speed);
			}
		}
	}
}
