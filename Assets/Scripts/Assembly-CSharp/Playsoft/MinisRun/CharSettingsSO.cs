using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "CharSettingsSO", menuName = "SO/CharSettingsSO")]
	public class CharSettingsSO : ScriptableObject
	{
		[Serializable]
		public class AnimationUsageData
		{
			public bool IsDefaultAnim;

			public bool LoadImmediately;

			public ScreenType ScreenType;
		}

		[Serializable]
		public class CharAnimatorData
		{
			public CharAnimationType Type;

			public string AnimatorName;

			public List<AnimationUsageData> UsageData;

			public bool IsLoadImmediately(ScreenType screenType)
			{
				return UsageData.Find((AnimationUsageData u) => u.ScreenType.Equals(screenType)).LoadImmediately;
			}
		}

		public string IngameAssetName;

		public string UIAssetName;

		public string UICardAssetName;

		[Header("animations")]
		public string AnimatorsFolderName;

		public List<CharAnimatorData> CharAnimatorDataList;
	}
}
