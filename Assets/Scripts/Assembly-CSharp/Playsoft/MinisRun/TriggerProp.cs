using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class TriggerProp : AbstractProperty
	{
		public CameraSizeProp CameraSize = new CameraSizeProp();

		public KillZoneProp KillZone = new KillZoneProp();

		public SkinChangeProp SkinChange = new SkinChangeProp();

		public SpeedChangeProp SpeedChange = new SpeedChangeProp();

		public LevelEndProp LevelEnd = new LevelEndProp();

		public TutorialProp Tutorial = new TutorialProp();

		public TriggerProp()
		{
		}

		public TriggerProp(TriggerProp trigger)
		{
			IsEnabled = trigger.IsEnabled;
			CameraSize = new CameraSizeProp(trigger.CameraSize);
			KillZone = new KillZoneProp(trigger.KillZone);
			SkinChange = new SkinChangeProp(trigger.SkinChange);
			SpeedChange = new SpeedChangeProp(trigger.SpeedChange);
			LevelEnd = new LevelEndProp(trigger.LevelEnd);
			Tutorial = new TutorialProp(trigger.Tutorial);
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				CameraSize.AddToEntity(entity);
				KillZone.AddToEntity(entity);
				SkinChange.AddToEntity(entity);
				SpeedChange.AddToEntity(entity);
				LevelEnd.AddToEntity(entity);
				Tutorial.AddToEntity(entity);
			}
		}
	}
}
