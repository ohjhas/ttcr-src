using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "ChestReveal", menuName = "SO/ChestReveal")]
	public class SettingsChestRevealSO : ScriptableObject
	{
		public float FadeDuration = 0.25f;

		public float ChestOpenDuration = 0.25f;

		[Space]
		public float SingleChestShakeDuration = 1f;

		public float ChestShakeStrength = 30f;

		public int ChestShakeVibrato = 2;

		public float ChestShakeRandomness = 45f;

		[Space]
		public float DelayBetweenChestShakes = 0.2f;

		public int ChestShakeLoops = 5;

		[Space]
		public float FlashTransparency = 1f;

		public float FlashDuration = 0.25f;
	}
}
