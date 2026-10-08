using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class ObstacleProp : AbstractProperty
	{
		public MovableProp Movable = new MovableProp();

		public SwingingProp Swinging = new SwingingProp();

		public FloatingProp Floating = new FloatingProp();

		public ObstacleProp()
		{
		}

		public ObstacleProp(ObstacleProp obstacle)
		{
			IsEnabled = obstacle.IsEnabled;
			Movable = new MovableProp(obstacle.Movable);
			Swinging = new SwingingProp(obstacle.Swinging);
			Floating = new FloatingProp(obstacle.Floating);
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.isObstacle = true;
				Movable.AddToEntity(entity);
				Swinging.AddToEntity(entity);
				Floating.AddToEntity(entity);
			}
		}
	}
}
