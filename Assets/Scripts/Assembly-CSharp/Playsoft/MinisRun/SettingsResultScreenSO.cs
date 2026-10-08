using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "ResultScreen", menuName = "SO/ResultScreen")]
	public class SettingsResultScreenSO : ScriptableObject
	{
		[Range(0f, 1f)]
		public float FadeTransparency = 0.75f;

		public float CounterAnimTime = 1f;

		public AnimationCurve CounterAnimCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

		[Header("MissionProgressBarOnThreeStarsAnimation")]
		public float MissionProgressBarOnThreeStarsAnimationScaleDownDuration;

		public float MissionProgressBarOnThreeStarsAnimationScaleUpDuration;

		public float MissionProgressBarOnThreeStarsAnimationScaleUpDelay;

		public float MissionProgressBarOnThreeStarsAnimationParticlesDelay;

		public AnimationCurve MissionProgressBarOnThreeStarsAnimationScaleDownAnimationCurve;

		public AnimationCurve MissionProgressBarOnThreeStarsAnimationScaleUpAnimationCurve;
	}
}
