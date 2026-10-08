using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "WorldSelect", menuName = "SO/WorldSelect")]
	public class SettingsWorldSelectSO : ScriptableObject
	{
		public float CharacterSpeed = 1f;

		public float ScrollSpeed = 1f;

		public float ConfirmationPopupDelay = 0.2f;

		public float ConfirmationPopupAdditionalDelay = 0.4f;

		[Header("WSStars")]
		public float WSStarFlyAnimationDuration;

		public float WSStarsFlyAnimationOffset;

		public AnimationCurve WSStarFlyAnimationCurve;
	}
}
