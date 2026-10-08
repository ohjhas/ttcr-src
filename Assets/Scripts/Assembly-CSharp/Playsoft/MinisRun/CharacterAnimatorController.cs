using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[RequireComponent(typeof(Animator))]
	public class CharacterAnimatorController : MonoBehaviour
	{
		[SerializeField]
		private Animator Animator;

		private CharSettingsSO charSetting;

		private RuntimeAnimatorController defaultController;

		private RuntimeAnimatorController nextAnimatorController;

		private RuntimeAnimatorController prefetchedController;

		private ResourceRequest resourceRequest;

		private List<CharSettingsSO.CharAnimatorData> availableAnimations;

		private CharSettingsSO.CharAnimatorData defaultAnimData;

		private bool isInitialized;

		private bool isAnimationForced;

		private CharAnimationType currentAnim = CharAnimationType.Unknown;

		private CharAnimationType nextAnim = CharAnimationType.Unknown;

		private CharAnimationType prefetchedAnim = CharAnimationType.Unknown;

		private ScreenType screenType;

		private float timer;

		private void Awake()
		{
			if (Animator == null)
			{
				Animator = GetComponent<Animator>();
			}
		}

		private void Update()
		{
			if (isInitialized)
			{
				if (availableAnimations.Count > 0 && !isAnimationForced)
				{
					if (currentAnim == CharAnimationType.Idle)
					{
						MakeNewRequestIfNeeded();
						FinishRequestIfPossible();
						UpdateTimer();
						SwitchAnimationIfPossible();
					}
					else
					{
						WaitForEndOfAnimation();
					}
				}
			}
			else
			{
				FinishInitialRequestIfExists();
			}
		}

		public void Init(int charIndex, ScreenType _screenType)
		{
			screenType = _screenType;
			charSetting = SettingsService.GetSetting<SettingsSO>().CharSettings[charIndex];
			availableAnimations = charSetting.CharAnimatorDataList.FindAll((CharSettingsSO.CharAnimatorData animData) => animData.UsageData.Exists((CharSettingsSO.AnimationUsageData screen) => screen.ScreenType.Equals(screenType)));
			defaultAnimData = availableAnimations.Find((CharSettingsSO.CharAnimatorData animData) => animData.UsageData.Exists((CharSettingsSO.AnimationUsageData screen) => screen.ScreenType.Equals(screenType) && screen.IsDefaultAnim));
			availableAnimations.Remove(defaultAnimData);
			CharSettingsSO.CharAnimatorData charAnimatorData = defaultAnimData;
			currentAnim = charAnimatorData.Type;
			string text = "Characters/UI/" + charSetting.AnimatorsFolderName + "/";
			text += charAnimatorData.AnimatorName;
			if (charAnimatorData.IsLoadImmediately(screenType))
			{
				isInitialized = true;
				defaultController = Resources.Load<RuntimeAnimatorController>(text);
				Animator.runtimeAnimatorController = defaultController;
			}
			else
			{
				resourceRequest = Resources.LoadAsync<RuntimeAnimatorController>(text);
			}
		}

		public void ForceAnimation(int charIndex, CharAnimationType animType)
		{
			isAnimationForced = true;
			currentAnim = animType;
			if (prefetchedAnim.Equals(animType))
			{
				Animator.runtimeAnimatorController = prefetchedController;
				return;
			}
			charSetting = SettingsService.GetSetting<SettingsSO>().CharSettings[charIndex];
			CharSettingsSO.CharAnimatorData charAnimatorData = charSetting.CharAnimatorDataList.Find((CharSettingsSO.CharAnimatorData a) => a.Type.Equals(animType));
			string text = "Characters/UI/" + charSetting.AnimatorsFolderName + "/";
			text += charAnimatorData.AnimatorName;
			RuntimeAnimatorController runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(text);
			Animator.runtimeAnimatorController = runtimeAnimatorController;
		}

		public void ClearForceAnimation(int charIndex)
		{
			ForceAnimation(charIndex, CharAnimationType.Idle);
			isAnimationForced = false;
		}

		public void PrefetchAnimation(int charIndex, CharAnimationType animType)
		{
			charSetting = SettingsService.GetSetting<SettingsSO>().CharSettings[charIndex];
			CharSettingsSO.CharAnimatorData charAnimatorData = charSetting.CharAnimatorDataList.Find((CharSettingsSO.CharAnimatorData a) => a.Type.Equals(animType));
			string text = "Characters/UI/" + charSetting.AnimatorsFolderName + "/";
			text += charAnimatorData.AnimatorName;
			prefetchedAnim = animType;
			prefetchedController = Resources.Load<RuntimeAnimatorController>(text);
		}

		public void PrefetchAnimationAsync(int charIndex, CharAnimationType animationType)
		{
			charSetting = SettingsService.GetSetting<SettingsSO>().CharSettings[charIndex];
			CharSettingsSO.CharAnimatorData charAnimatorData = charSetting.CharAnimatorDataList.Find((CharSettingsSO.CharAnimatorData a) => a.Type.Equals(animationType));
			string text = "Characters/UI/" + charSetting.AnimatorsFolderName + "/";
			text += charAnimatorData.AnimatorName;
			ResourceRequest loadRequest = Resources.LoadAsync<RectTransform>(text);
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			uIEntity.AddAsyncRequest(loadRequest, delegate
			{
				prefetchedController = (RuntimeAnimatorController)loadRequest.asset;
			});
		}

		public CharAnimationType GetCurrentAnimation()
		{
			return currentAnim;
		}

		private void FinishInitialRequestIfExists()
		{
			if (resourceRequest != null && resourceRequest.isDone)
			{
				defaultController = resourceRequest.asset as RuntimeAnimatorController;
				isInitialized = true;
				Animator.runtimeAnimatorController = defaultController;
				resourceRequest = null;
			}
		}

		private void MakeNewRequestIfNeeded()
		{
			if (resourceRequest == null && nextAnimatorController == null)
			{
				CharSettingsSO.CharAnimatorData charAnimatorData = availableAnimations[Random.Range(0, availableAnimations.Count)];
				nextAnim = charAnimatorData.Type;
				string text = "Characters/UI/" + charSetting.AnimatorsFolderName + "/";
				text += charAnimatorData.AnimatorName;
				if (charAnimatorData.IsLoadImmediately(screenType))
				{
					nextAnimatorController = Resources.Load<RuntimeAnimatorController>(text);
				}
				else
				{
					resourceRequest = Resources.LoadAsync<RuntimeAnimatorController>(text);
				}
			}
		}

		private void FinishRequestIfPossible()
		{
			if (resourceRequest != null && resourceRequest.isDone)
			{
				nextAnimatorController = resourceRequest.asset as RuntimeAnimatorController;
				resourceRequest = null;
			}
		}

		private void UpdateTimer()
		{
			timer += Time.deltaTime;
		}

		private void SwitchAnimationIfPossible()
		{
			if (timer >= SettingsService.SettingsSO.MinDelayBetweenOutgameAnimations && nextAnimatorController != null)
			{
				currentAnim = nextAnim;
				Animator.runtimeAnimatorController = nextAnimatorController;
				nextAnimatorController = null;
			}
		}

		private void WaitForEndOfAnimation()
		{
			if (Animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
			{
				currentAnim = defaultAnimData.Type;
				Animator.runtimeAnimatorController = defaultController;
				timer = 0f;
			}
		}
	}
}
