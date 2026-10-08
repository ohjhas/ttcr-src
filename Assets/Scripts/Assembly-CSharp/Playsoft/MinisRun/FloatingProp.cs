using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class FloatingProp : AbstractProperty
	{
		public float Amplitude;

		public float Speed;

		public float Offset;

		public bool HasLocalTimer;

		public FloatingProp()
		{
		}

		public FloatingProp(FloatingProp floatable)
		{
			IsEnabled = floatable.IsEnabled;
			Amplitude = floatable.Amplitude;
			Speed = floatable.Speed;
			Offset = floatable.Offset;
			HasLocalTimer = floatable.HasLocalTimer;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddFloating(Amplitude, Speed, Offset, HasLocalTimer, 0f);
			}
		}
	}
}
