using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class LevelEndProp : AbstractProperty
	{
		public float DistanceToFinish = 28f;

		public LevelEndProp()
		{
		}

		public LevelEndProp(LevelEndProp levelEnd)
		{
			IsEnabled = levelEnd.IsEnabled;
			DistanceToFinish = levelEnd.DistanceToFinish;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddLevelEnd(DistanceToFinish);
			}
		}
	}
}
