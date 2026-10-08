using Playsoft.MinisRun;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsSO", menuName = "SO/Settings")]
public class SettingsSO : ScriptableObject
{
	[Header("World")]
	public int WorldIndex;

	public ChapterSettingsSO[] ChapterSettings;

	public float MinPositionY;

	public Vector3 PlayerCameraOffset = new Vector3(-5f, -4f, 0f);

	public float DestroyZoneOffset = -20f;

	[Header("Character")]
	public float MinDelayBetweenOutgameAnimations = 1f;

	public CharSettingsSO[] CharSettings;

	[Header("Companions")]
	public Vector3 AnchorToPlayer = new Vector3(-2f, 3f);

	public Vector3 AnchorToScreen = new Vector3(11f, 1.75f);

	public float CameraSmoothTimeFollow = 0.2f;

	public float DragonBlastRange = 2f;

	public float UnicornAnimationLength = 2f;

	public CompanionSettingsSO[] CompanionSettings;

	[Header("World speed")]
	public float BaseSpeed;

	public float HitSpeed;

	public float HitRecoverSpeed;

	[Header("Stamina")]
	public float StartingStamina;

	[Header("Invulnerability")]
	public float InvulnerabilityTime;

	[Header("Jump")]
	public float JumpForce;

	public float JumpBufferDuration;

	public int JumpsQuantity;

	[Header("Colliders")]
	public Vector2 ColliderNormal;

	public Vector2 ColliderSlide;

	[Header("Clouds")]
	public float CloudsDuration;

	public float CloudSize;

	public float CloudsYPosition;

	[Header("Destroyed Colliders")]
	public Vector2 MaxHitForce;

	public float MaxHitTorque;

	public float FadeOutDuration;

	public int DestroyedObstacleScore;

	[Header("Gameplay type")]
	public GameplayType GameplayType;

	[Header("CharacterUpgradeAnimation")]
	public float StaminaEffectDelay;

	public float StaminaFieldBounceDelay;

	public float AbilityBounceEffectDelay;

	public float AbilityChangeTextDelay;

	public float RibbonEffectsDelayAbilityAbsent;

	public float RibbonEffectsDelayAbilityPresent;

	public float ChangeLevelTextEffectDelayAbilityAbsent;

	public float ChangeLevelTextEffectDelayAbilityPresent;

	public float WinAnimationDelayAbilityPresent;

	public float WinAnimationDelayAbilityAbsent;

	public float LevelProgressAfterUpgradeDelay;

	public float ParticlesStartTime;

	public float ParticlesStopTime;

	public float StaminaBounceEffectDuration;

	public float StaminaFieldBounceDuration;

	public float AbilityBounceEffectDuration;

	public float RibbonShineEffectDuration;

	public float CharacterJumpAnimationDuration;

	public float StaminaBounceEffectInBetweenPunchesOffset;

	public float StaminaBounceEffectFirstBounceStrength;

	public float StaminaBounceEffectLastBounceStrength;

	public float StaminaFieldBouncePunch;

	public float AbilityBouncePunch;

	public AnimationCurve BouncePunchAnimationCurve;

	public Vector2 CharacterJumpAnimationTopPosition;

	public AnimationCurve CharacterJumpAnimationCurve;
}
