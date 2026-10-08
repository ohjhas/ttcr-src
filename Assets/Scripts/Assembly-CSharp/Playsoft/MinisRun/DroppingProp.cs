using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class DroppingProp : AbstractProperty
	{
		public float Delay;

		public DroppingProp()
		{
		}

		public DroppingProp(DroppingProp dropable)
		{
			IsEnabled = dropable.IsEnabled;
			Delay = dropable.Delay;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddDropping(false, Delay);
			}
		}
	}
}
