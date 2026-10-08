using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "LevelData", menuName = "SO/LevelData")]
	public class LevelData : ScriptableObject
	{
		[NonSerialized]
		public string typedFileName;

		public LevelTutorialSettings LevelTutorialSettings;

		public List<LevelElementData> LevelElements;
	}
}
