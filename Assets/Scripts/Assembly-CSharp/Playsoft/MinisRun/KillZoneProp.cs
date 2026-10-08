using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class KillZoneProp : AbstractProperty
	{
		public KillZoneProp()
		{
		}

		public KillZoneProp(KillZoneProp killZone)
		{
			IsEnabled = killZone.IsEnabled;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.isObstacle = true;
				entity.isKillZone = true;
			}
		}
	}
}
