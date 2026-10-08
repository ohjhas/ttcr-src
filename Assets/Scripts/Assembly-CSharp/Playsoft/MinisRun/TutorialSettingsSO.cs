using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "TutorialSettings", menuName = "SO/TutorialSettings")]
	public class TutorialSettingsSO : ScriptableObject
	{
		public float TriggerOffset = 1.5f;

		public float TriggerLength = 1.5f;

		public AnimationCurve SlowDownCurve;

		public float PvEScreenTutorialDelayToPulsate = 5f;

		public float HitTutorialDuration = 2f;

		public int HitTutorialPossibleDisplayesCount = 2;

		[Header("Slow motion")]
		public float SlowDownThresholdOffset = -1f;

		public float SlowDownMargin = 0.2f;

		[Header("Stamina")]
		public float staminaSlowDownFactor = 0.8f;

		public float staminaSlowDownDuration = 0.5f;

		public float staminaDrainTutorialDuration = 5f;

		public float staminaPotionTutorialDuration = 2.5f;

		[Header("Goal")]
		public float goalSlowDownFactor = 0.8f;

		public float goalSlowDownDuration = 0.5f;

		public float goalInitialDelay = 2f;

		public float hideInterstitialDelay = 3f;
	}
}
