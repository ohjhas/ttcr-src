using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class MovableProp : AbstractProperty
	{
		public float StartThreshold;

		public float EndThreshold;

		public Vector2 MoveVector;

		public MovableProp()
		{
		}

		public MovableProp(MovableProp movable)
		{
			IsEnabled = movable.IsEnabled;
			StartThreshold = movable.StartThreshold;
			EndThreshold = movable.EndThreshold;
			MoveVector = movable.MoveVector;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddMovableData(StartThreshold, EndThreshold, MoveVector);
			}
		}
	}
}
