using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class SpeedChangeProp : AbstractProperty
	{
		public int Speed;

		public SpeedChangeProp()
		{
		}

		public SpeedChangeProp(SpeedChangeProp speedChange)
		{
			IsEnabled = speedChange.IsEnabled;
			Speed = speedChange.Speed;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddChangeWorldSpeed(Speed);
			}
		}
	}
}
