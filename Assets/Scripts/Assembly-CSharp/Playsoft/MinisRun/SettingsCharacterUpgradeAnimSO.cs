using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "SettingsCharacterUpgradeAnimSO", menuName = "SO/CharacterUpgradeSO")]
	public class SettingsCharacterUpgradeAnimSO : ScriptableObject
	{
		public float ParticlesEffectDelay;

		public float StaminaEffectDelay;

		public float AbilityBounceEffectDelay;

		public float RibbonEffectsDelay;

		public float ChangeLevelTextEffectDelay;

		public float LevelProgressAfterUpgradeDelay;

		public float TotalCharUpgradeAnimationDuration;

		public float StaminaBounceEffectDuration;

		public float AbilityBounceEffectDuration;

		public float RibbonShineEffectDuration;

		public float RibbonBounceEffectDuration;

		public float RibbonBouncePunch;

		public float AbilityBouncePunch;

		public float StaminaBounceEffectInBetweenPunchesOffset;

		public float StaminaBounceEffectFirstBounceStrength;

		public float StaminaBounceEffectLastBounceStrength;
	}
}
