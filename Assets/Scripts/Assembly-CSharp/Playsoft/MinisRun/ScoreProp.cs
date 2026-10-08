using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class ScoreProp : AbstractProperty
	{
		public int Score;

		public ScoreProp()
		{
		}

		public ScoreProp(ScoreProp score)
		{
			IsEnabled = score.IsEnabled;
			Score = score.Score;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddCollectibleScore(Score);
			}
		}
	}
}
