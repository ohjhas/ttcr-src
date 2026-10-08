using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "VisualFeedback", menuName = "SO/VisualFeedback")]
	public class SettingsVisualFeedbackSO : ScriptableObject
	{
		[Header("Camera Shake")]
		public Vector3 ObstaclePunchVector = new Vector3(0.25f, 0.25f, 0.25f);

		public Vector3 LandingPunchVector = new Vector3(0f, 0.5f, 0f);

		public float PunchDuration = 0.5f;

		public int PunchVibrato = 20;

		public float PunchElasticity = 10f;

		[Header("Red Fade")]
		public Color EndColor = new Color(1f, 0f, 0f, 0.75f);

		public float AnimTime = 0.15f;

		[Header("Player Animations")]
		public float RunWithWaveAnimInterval = 10f;

		public float RunWithWaveDuration = 2f;

		[Header("Player Blinking")]
		[Tooltip("number of cycles between full and min transparency during invulnerability")]
		public int NumberOfCycles = 2;

		[Header("Background Blending")]
		public Color BlendBorderColor = new Color(0.75f, 0.75f, 0.75f, 1f);

		[Range(0f, 1f)]
		public float BlendBorderWidth = 0.2f;

		[Range(0f, 1f)]
		[Tooltip("Relative part of border width")]
		public float BlendBorderMinFulfillment = 0.2f;

		[Header("Score Blinking")]
		public Material ScoreDefaultMat;

		public Material ScoreBlinkMat;

		[Header("Parts Slide fill")]
		public Color NotFullBarColor;

		public Color FullBarColor;

		public float BlendStartPosition_UV
		{
			get
			{
				return 0.6f + BlendBorderWidth;
			}
		}
	}
}
