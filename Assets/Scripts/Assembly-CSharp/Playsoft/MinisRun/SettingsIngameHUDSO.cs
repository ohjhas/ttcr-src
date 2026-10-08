using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "SettingsIngameHUDSO", menuName = "SO/IngameHUDSettings")]
	public class SettingsIngameHUDSO : ScriptableObject
	{
		[Header("Stamina")]
		public float StaminaFlyTimelength;

		public float StaminaBarBounceEffectInBetweenPunchesOffset;

		public AnimationCurve StaminaFlyAnimationCurve;

		public float StaminaBarBounceEffectSingleBounceDuration;

		public float StaminaBarBounceEffectFirstBounceStrength;

		public float StaminaBarBounceEffectLastBounceStrength;

		public float StaminaFillSpeed = 50f;

		[Header("MissionGoal")]
		public AnimationCurve MissionGoalScaleAnimationCurve;

		public AnimationCurve MissionGoalMoveAnimationCurve;

		public float MissionGoalAnimationMoveAnimationDuration;

		public float MissionGoalMoveAnimationDelay;
	}
}
