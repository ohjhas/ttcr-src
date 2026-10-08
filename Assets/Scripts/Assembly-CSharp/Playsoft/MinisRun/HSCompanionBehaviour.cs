using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class HSCompanionBehaviour : MonoBehaviour
	{
		private enum State
		{
			Flying = 0,
			Idle = 1
		}

		[SerializeField]
		private Vector2 _idleDurationRange;

		private Animator _animator;

		private Transform _imageTransform;

		private RectTransform _imageRT;

		private RectTransform Companion;

		private float _timer;

		private float _currentIdleDuration;

		private float _lastImagePositionX;

		private static readonly Vector3 _leftDirection = new Vector3(-1f, 1f, 1f);

		private static readonly Vector3 _rightDirection = new Vector3(1f, 1f, 1f);

		private State _state;

		public void DestroyOldComp()
		{
			if (Companion != null)
			{
				Object.Destroy(Companion.gameObject);
			}
		}

		public void CreateCompanionAsync(UnityAction onCompanionPressCallback, int compId)
		{
			Contexts.sharedInstance.uI.GetCompanionUIAssetAsync(compId, FinishCreatingNewCompanion, onCompanionPressCallback);
		}

		private void FinishCreatingNewCompanion(RectTransform compPrefab, UnityAction onCompanionPressCallback)
		{
			Companion = Object.Instantiate(compPrefab);
			Companion.SetParent(base.transform);
			Companion.anchorMin = Vector2.zero;
			Companion.anchorMax = Vector2.one;
			Companion.pivot = new Vector2(0.5f, 0f);
			Companion.localScale = Vector3.one;
			Companion.anchoredPosition3D = Vector3.zero;
			Companion.sizeDelta = Vector2.zero;
			_animator = Companion.GetComponent<Animator>();
			_imageTransform = Companion.GetChild(0);
			_timer = 0f;
			_currentIdleDuration = Random.Range(_idleDurationRange.x, _idleDurationRange.y);
			_state = State.Idle;
			_lastImagePositionX = _imageTransform.position.x;
			_imageRT = _imageTransform.GetComponent<RectTransform>();
			AddButtonToCharacter(onCompanionPressCallback);
		}

		private void OnEnable()
		{
			UICompanionAnimatorEvents.OnStateFinished = ChangeStateToIdle;
		}

		private void OnDisable()
		{
			UICompanionAnimatorEvents.OnStateFinished = null;
		}

		private void Update()
		{
			if (Companion != null)
			{
				SetFaceDirection();
				CountDownToFlyAnimation();
			}
		}

		private void LateUpdate()
		{
			if (Companion != null && (_state == State.Idle || !_animator.GetCurrentAnimatorStateInfo(0).IsName("UIFly")))
			{
				_imageRT.anchoredPosition3D = new Vector3(-300f, _imageRT.anchoredPosition3D.y, 0f);
			}
		}

		private void SetFaceDirection()
		{
			_imageTransform.localScale = ((!(_imageTransform.position.x >= _lastImagePositionX)) ? _leftDirection : _rightDirection);
			_lastImagePositionX = _imageTransform.position.x;
		}

		private void CountDownToFlyAnimation()
		{
			if (_state == State.Idle)
			{
				_timer += Time.deltaTime;
				if (_timer >= _currentIdleDuration)
				{
					_animator.SetTrigger("Fly");
					_timer = 0f;
					_currentIdleDuration = Random.Range(_idleDurationRange.x, _idleDurationRange.y);
					_state = State.Flying;
					_imageTransform.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-300f, 0f, 0f);
				}
			}
		}

		private void ChangeStateToIdle()
		{
			_animator.ResetTrigger("Fly");
			_state = State.Idle;
		}

		private void AddButtonToCharacter(UnityAction onCompanionPressCallback)
		{
			Button button = _imageRT.gameObject.AddComponent<Button>();
			ColorBlock colors = button.colors;
			Color color = (colors.disabledColor = Color.white);
			color = (colors.highlightedColor = color);
			color = (colors.pressedColor = color);
			colors.normalColor = color;
			button.colors = colors;
			button.onClick.AddListener(onCompanionPressCallback);
		}
	}
}
