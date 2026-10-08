using UnityEngine;

namespace Playsoft.MinisRun
{
	[CreateAssetMenu(fileName = "SettingsRacoonAnimSO", menuName = "SO/RacconAnimSettings")]
	public class SettingsRacoonAnimSO : ScriptableObject
	{
		public float RaccoonIntroRaccoonRunInDelay;

		public float RaccoonIntroCharacterRunInDelay;

		public float RaccoonIntroRaccoonSpeedModifier;

		public float RaccoonIntroRaccoonDropDistanceX;

		public float RaccoonIntroRaccoonDropDistanceY;

		public AnimationCurve RaccoonIntroRaccoonDropCurve;

		public float RaccoonIntroRaccoonDropDuration;

		public Vector3 RaccoonIntroCharacterCameraOffsetInitial;

		public Vector3 RaccoonIntroRaccoonCameraOffsetFinal;

		public float RaccoonFirstIntroRaccoonRunInDelay;

		public float RaccoonFirstIntroCharacterRunInDelay;

		public float RaccoonFirstIntroRaccoonLookBackDelay;

		public float RaccoonFirstIntroRaccoonSpeedUpDelay;

		public float RaccoonFirstIntroRaccoonDropDelay;

		public float RaccoonFirstIntroRaccoonSpeedModifierSlow;

		public float RaccoonFirstIntroRaccoonSpeedModifierFast;

		public Vector3 RaccoonFirstIntroRaccoonCameraOffsetFinal;

		public Vector2 RaccoonFirstIntroCharacterInitialPos_Tablet;

		public Vector2 RaccoonFirstIntroRaccoonInitialPos_Tablet;

		public Vector2 RaccoonFirstIntroCandyStoreInitialPos_Tablet;

		public Vector2 RaccoonFirstIntroCharacterInitialPos_IPhoneX;

		public Vector2 RaccoonFirstIntroRaccoonInitialPos_IPhoneX;

		public Vector2 RaccoonFirstIntroCandyStoreInitialPos_IPhoneX;

		public int RaccoonFirstIntroPlayerWaveAnimCandiesToCollect;
	}
}
