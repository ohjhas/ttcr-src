using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class HSMovingChar : MonoBehaviour
	{
		public enum CharacterLayer
		{
			Front = 0,
			Middle = 1,
			Back = 2
		}

		private enum AnimState
		{
			Running = 0,
			Idle = 1
		}

		[SerializeField]
		private RectTransform shadowTransform;

		[SerializeField]
		private RectTransform _rectTransform;

		[SerializeField]
		private Vector2 _idleAnimationDurationRange;

		[SerializeField]
		private Vector2 _runningAnimationDurationRange;

		[SerializeField]
		private float _idleAnimatorSpeed;

		[SerializeField]
		private float _runningAnimatorSpeed;

		[SerializeField]
		private float _runningAnimationSpeed;

		[SerializeField]
		private float _leftAnchorPos;

		[SerializeField]
		private float _rightAnchorPos;

		[SerializeField]
		private CharacterLayer layer;

		[SerializeField]
		[Range(-1f, 1f)]
		private float _startingPos;

		private RectTransform runningCharacter;

		private Image runningCharacterImage;

		private CharacterSizeController runningCharacterSizeController;

		private CharacterAnimatorController runningCharacterAnimatorController;

		private Animator runningCharacterAnimator;

		private float _idleAnimationTimer;

		private float _runningAnimationTimer;

		private float _shouldMoveToIdleAnimationTimer;

		private float _lastAnimFactor;

		private float _idleAnimationDuration;

		private float _runningAnimationDuration;

		private SettingsHomeScreenSO homeScreenSettings;

		private AnimState _state;

		private int lastCharId = -1;

		private void Start()
		{
			_idleAnimationDuration = UnityEngine.Random.Range(_idleAnimationDurationRange.x, _idleAnimationDurationRange.y);
			_runningAnimationDuration = UnityEngine.Random.Range(_runningAnimationDurationRange.x, _runningAnimationDurationRange.y);
			homeScreenSettings = SettingsService.GetSetting<SettingsHomeScreenSO>();
		}

		private void Update()
		{
			if (runningCharacter == null || runningCharacterAnimator == null)
			{
				return;
			}
			if (runningCharacterAnimatorController == null)
			{
				UpdateAnimation();
				return;
			}
			if (runningCharacterAnimatorController.GetCurrentAnimation() == CharAnimationType.Walking)
			{
				_state = AnimState.Running;
				PlayRunningAnimation();
				return;
			}
			_state = AnimState.Idle;
			if (layer == CharacterLayer.Front)
			{
				runningCharacter.localScale = Vector3.one;
			}
		}

		private void LateUpdate()
		{
			if (runningCharacterImage != null && homeScreenSettings != null)
			{
				_rectTransform.sizeDelta = runningCharacterImage.sprite.rect.size * homeScreenSettings.GetCharAnimData(layer).Size;
				if (runningCharacterAnimatorController != null)
				{
					float num = runningCharacterImage.sprite.pivot.x / runningCharacterImage.sprite.rect.size.x;
					num = ((!(runningCharacter.localScale.x < 0f)) ? num : (1f - num));
					_rectTransform.pivot = new Vector2(num, 0f);
					shadowTransform.anchorMin = new Vector2(num - 0.5f, 0f);
					shadowTransform.anchorMax = new Vector2(1f - (0.5f - num), 0f);
				}
			}
		}

		public void Init(UnityAction onCharacterPressCallback, int charIndex)
		{
			base.gameObject.SetActive(true);
			CreateBgCharacter(onCharacterPressCallback, charIndex);
		}

		private void CreateBgCharacter(UnityAction onCharacterPressCallback, int charIndex)
		{
			if (charIndex != lastCharId)
			{
				if (runningCharacter != null)
				{
					UnityEngine.Object.Destroy(runningCharacter.gameObject);
				}
				CreateNewCharacterAsync(onCharacterPressCallback, charIndex);
			}
		}

		private void CreateNewCharacterAsync(UnityAction onCharacterPressCallback, int charIndex)
		{
			Contexts.sharedInstance.uI.GetCharacterUIAssetAsync(charIndex, delegate(RectTransform prefab)
			{
				FinishCreatingNewCharacter(prefab);
				FinishCreatingBgCharacter(onCharacterPressCallback, charIndex);
				ResetAnimation();
				lastCharId = charIndex;
			});
		}

		private void FinishCreatingNewCharacter(RectTransform charPrefab)
		{
			runningCharacter = UnityEngine.Object.Instantiate(charPrefab);
			runningCharacter.SetParent(_rectTransform);
			runningCharacter.localScale = Vector3.one;
			runningCharacter.pivot = new Vector2(0.5f, 0f);
			runningCharacter.anchoredPosition3D = Vector3.zero;
			runningCharacter.sizeDelta = Vector2.zero;
		}

		private void FinishCreatingBgCharacter(UnityAction onCharacterPressCallback, int charIndex)
		{
			GetReferences(charIndex);
			if (TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.SelectCharacter))
			{
				AddButtonToCharacter(onCharacterPressCallback);
			}
			_state = AnimState.Idle;
		}

		private void GetReferences(int charIndex)
		{
			runningCharacterAnimator = runningCharacter.GetComponent<Animator>();
			runningCharacterImage = runningCharacter.GetComponentInChildren<Image>();
			runningCharacterSizeController = runningCharacter.GetComponentInChildren<CharacterSizeController>();
			if (runningCharacterSizeController != null)
			{
				runningCharacterSizeController.enabled = false;
			}
			runningCharacterAnimatorController = runningCharacter.GetComponentInChildren<CharacterAnimatorController>();
			if (runningCharacterAnimatorController != null)
			{
				runningCharacterAnimatorController.Init(charIndex, ScreenType.HomeScreen);
			}
		}

		private void ResetAnimation()
		{
			_runningAnimationTimer = _startingPos * (float)Math.PI;
			float num = Mathf.Sin(_runningAnimationSpeed * _runningAnimationTimer * (float)Math.PI) * 0.5f + 0.5f;
			SetAnchorPosition(Mathf.Lerp(_leftAnchorPos, _rightAnchorPos, num));
			_lastAnimFactor = num;
		}

		private void AddButtonToCharacter(UnityAction onCharacterPressCallback)
		{
			if (!(null == runningCharacter))
			{
				Button button = runningCharacter.GetChild(0).gameObject.AddComponent<Button>();
				if (button != null)
				{
					ColorBlock colors = button.colors;
					Color color = (colors.disabledColor = Color.white);
					color = (colors.highlightedColor = color);
					color = (colors.pressedColor = color);
					colors.normalColor = color;
					button.colors = colors;
					button.onClick.AddListener(onCharacterPressCallback);
				}
			}
		}

		private void SetAnchorPosition(float xAnchor)
		{
			_rectTransform.anchoredPosition = new Vector2(xAnchor, _rectTransform.anchoredPosition.y);
		}

		private void StartRunningAnimation()
		{
			_state = AnimState.Running;
			_idleAnimationDuration = UnityEngine.Random.Range(_idleAnimationDurationRange.x, _idleAnimationDurationRange.y);
			_idleAnimationTimer = 0f;
			runningCharacterAnimator.speed = _runningAnimatorSpeed;
			runningCharacterAnimator.Play("UIRunAnim");
		}

		private void PlayRunningAnimation()
		{
			_runningAnimationTimer += Time.deltaTime;
			float num = Mathf.Sin(_runningAnimationSpeed * _runningAnimationTimer * (float)Math.PI) * 0.5f + 0.5f;
			SetAnchorPosition(Mathf.Lerp(_leftAnchorPos, _rightAnchorPos, num));
			float x = ((num - _lastAnimFactor >= 0f) ? 1 : (-1));
			runningCharacter.localScale = new Vector3(x, 1f, 1f);
			_lastAnimFactor = num;
		}

		private void StartIdleAnimation()
		{
			_state = AnimState.Idle;
			_runningAnimationDuration = UnityEngine.Random.Range(_runningAnimationDurationRange.x, _runningAnimationDurationRange.y);
			_shouldMoveToIdleAnimationTimer = 0f;
			runningCharacterAnimator.speed = _idleAnimatorSpeed;
			runningCharacterAnimator.Play("UIIdleAnim");
		}

		private void UpdateAnimation()
		{
			switch (_state)
			{
			case AnimState.Idle:
				_idleAnimationTimer += Time.deltaTime;
				if (_idleAnimationTimer >= _idleAnimationDuration)
				{
					StartRunningAnimation();
				}
				break;
			case AnimState.Running:
				_shouldMoveToIdleAnimationTimer += Time.deltaTime;
				if (_shouldMoveToIdleAnimationTimer < _runningAnimationDuration)
				{
					PlayRunningAnimation();
				}
				else
				{
					StartIdleAnimation();
				}
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
		}
	}
}
