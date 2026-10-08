using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "AssetsSettingsSO", menuName = "SO/AssetsSettingsSO")]
	public class AssetsSettingsSO : ScriptableObject
	{
		[Serializable]
		private class SpriteAsset
		{
			public string Name;

			public Sprite Sprite;
		}

		[Serializable]
		private class LevelElementAsset
		{
			public string Name;

			public LevelElement LevelElement;
		}

		[Serializable]
		public class MissionIconAsset
		{
			public MissionType Type;

			public Sprite Sprite;

			public float Size;

			public float minFillAmount;

			public float minFirstFillAmount;
		}

		[Serializable]
		public class DailyMissionIconAsset
		{
			public DailyMissionType Type;

			public Sprite Sprite;

			public float Size = 1f;

			public Vector2 Offset = Vector2.zero;
		}

		public const string SC_ICON_NAME = "SCIcon";

		public const string HC_ICON_NAME = "HCIcon";

		public const string SMALL_STAMINA_TOKEN_NAME = "SmallStaminaToken";

		public const string GIANT_POWERUP_TOKEN_NAME = "GiantToken";

		public const string MAGNET_POWERUP_TOKEN_NAME = "MagnetCollectible";

		public const string SCORE_MULTIPLIER_POWERUP_TOKEN_NAME = "ScoreMultiplierToken";

		public const string SPEEDUP_POWERUP_TOKEN_NAME = "SpeedUpToken";

		public const string UPGRADE_SCORE_POWERUP_TOKEN_NAME = "UpgradeScoreToken";

		public const string EXPLANATION_ICON_SC_NAME = "InlinedIcons_5";

		public const string EXPLANATION_ICON_HC_NAME = "InlinedIcons_1";

		public const string EXPLANATION_ICON_SC_DUMMY_NAME = "InlinedIcons_6";

		public const string EXPLANATION_ICON_HC_DUMMY_NAME = "InlinedIcons_7";

		public const string EXPLANATION_ICON_STAR_NAME = "InlinedIcons_0";

		[SerializeField]
		private List<SpriteAsset> SpriteAssets;

		[SerializeField]
		private List<LevelElementAsset> LevelElementAssets;

		[SerializeField]
		private List<MissionIconAsset> missionIconAssets;

		[SerializeField]
		private List<DailyMissionIconAsset> dailyMissionIconAssets;

		public Sprite GetSprite(string name)
		{
			SpriteAsset spriteAsset = SpriteAssets.Find((SpriteAsset a) => a.Name.Equals(name));
			return (spriteAsset == null) ? null : spriteAsset.Sprite;
		}

		public LevelElement GetLevelElement(string name)
		{
			LevelElementAsset levelElementAsset = LevelElementAssets.Find((LevelElementAsset le) => le.Name.Equals(name));
			return (levelElementAsset == null) ? null : levelElementAsset.LevelElement;
		}

		public MissionIconAsset GetMissionIcon(MissionType type)
		{
			return missionIconAssets.Find((MissionIconAsset d) => d.Type == type);
		}

		public DailyMissionIconAsset GetDailyMissionIcon(DailyMissionType type)
		{
			return dailyMissionIconAssets.Find((DailyMissionIconAsset d) => d.Type == type);
		}
	}
}
