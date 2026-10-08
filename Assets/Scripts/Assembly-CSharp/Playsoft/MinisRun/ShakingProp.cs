using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class ShakingProp : AbstractProperty
	{
		public float Interval;

		public float Strength;

		public ShakingProp()
		{
		}

		public ShakingProp(ShakingProp shaking)
		{
			IsEnabled = shaking.IsEnabled;
			Interval = shaking.Interval;
			Strength = shaking.Strength;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddShaking(Interval, Strength);
			}
		}
	}
}
