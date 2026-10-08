using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "SettingsPowerupsSO", menuName = "SO/PowerupsSettings")]
	public class SettingsPowerupsSO : ScriptableObject
	{
		[Header("Powerups")]
		public float MagnetDuration;

		public float MagnetForce;

		public float MagnetDistance;

		public float MagnetMinRadius;

		public AnimationCurve MagnetCurve;

		public float SizeIncreaseDuration;

		public AnimationCurve SizeIncreaseCurve;

		public float SpeedUpDuration;

		public AnimationCurve SpeedUpCurve;

		public float ScoreMultiplierDuration;

		public float ScoreMultiplierFactor;

		public float UpgradeScoreDuration;

		public float UpgradeScoreDistance;

		public Vector3 ImprovedScorePosOffest;

		public float ReviveAbilityDelay;
	}
}
