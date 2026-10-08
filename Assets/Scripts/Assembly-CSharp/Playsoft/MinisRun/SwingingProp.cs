using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class SwingingProp : AbstractProperty
	{
		public Vector2 Anchor;

		public Vector2 StartEndRotation;

		public float StartThreshold;

		public float EndThreshold;

		public SwingingProp()
		{
		}

		public SwingingProp(SwingingProp swinging)
		{
			IsEnabled = swinging.IsEnabled;
			StartThreshold = swinging.StartThreshold;
			EndThreshold = swinging.EndThreshold;
			Anchor = swinging.Anchor;
			StartEndRotation = swinging.StartEndRotation;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddSwingingData(Anchor, StartEndRotation, StartThreshold, EndThreshold, 0f);
			}
		}
	}
}
