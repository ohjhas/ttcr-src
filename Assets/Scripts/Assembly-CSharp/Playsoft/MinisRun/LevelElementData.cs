using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class LevelElementData
	{
		[HideInInspector]
		public string AssetName;

		[HideInInspector]
		public Vector3 Position;

		public ObstacleProp Obstacle;

		public PlatformProp Platform;

		public CollectibleProp Collectible;

		public TriggerProp Trigger;

		public LevelElementData()
		{
			Obstacle = new ObstacleProp();
			Platform = new PlatformProp();
			Collectible = new CollectibleProp();
			Trigger = new TriggerProp();
		}

		public LevelElementData(LevelElementData copy)
		{
			AssetName = copy.AssetName;
			Position = copy.Position;
			Obstacle = new ObstacleProp(copy.Obstacle);
			Platform = new PlatformProp(copy.Platform);
			Collectible = new CollectibleProp(copy.Collectible);
			Trigger = new TriggerProp(copy.Trigger);
		}
	}
}
