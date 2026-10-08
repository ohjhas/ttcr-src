using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class CameraSizeProp : AbstractProperty
	{
		public float Value;

		public CameraSizeProp()
		{
		}

		public CameraSizeProp(CameraSizeProp cameraSize)
		{
			IsEnabled = cameraSize.IsEnabled;
			Value = cameraSize.Value;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddChangeCameraSizeTrigger(Value);
			}
		}
	}
}
