using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class TutorialProp : AbstractProperty
	{
		public TutorialStep Step;

		public bool StopTime;

		public TutorialProp()
		{
		}

		public TutorialProp(TutorialProp tutorial)
		{
			IsEnabled = tutorial.IsEnabled;
			Step = tutorial.Step;
			StopTime = tutorial.StopTime;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddTutorialTrigger(Step, StopTime);
			}
		}
	}
}
