using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Playsoft
{
	[RequireComponent(typeof(Button))]
	public class ButtonFeedback : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler, IPointerClickHandler, IEventSystemHandler
	{
		private enum ClickType
		{
			Big = 0,
			Small = 1,
			None = 2
		}

		[SerializeField]
		private Button button;

		[SerializeField]
		private ClickType sound;

		[SerializeField]
		private Transform target;

		[SerializeField]
		private float defaultScale = 1f;

		[SerializeField]
		private float minimumScale = 0.8f;

		[SerializeField]
		private float animationTime = 0.2f;

		private Tweener tweener;

		public bool IsScaleDown { get; private set; }

		public bool IsPressed { get; private set; }

		public float MinimumScale
		{
			get
			{
				return minimumScale;
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			IsPressed = true;
			Press();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			IsPressed = false;
			IsScaleDown = false;
			UnPress();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			if (IsPressed)
			{
				UnPress();
			}
		}

		public void OnPointerEnter(PointerEventData eventData)
		{
			if (IsPressed)
			{
				Press();
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (sound == ClickType.None || !(button != null) || (button.enabled && button.interactable))
			{
				Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect((sound != ClickType.Big) ? "button_tap_small" : "button_tap_big");
			}
		}

		private void Awake()
		{
			if (target == null)
			{
				target = base.transform;
			}
			if (button == null)
			{
				button = GetComponent<Button>();
			}
		}

		private void Press()
		{
			if (!IsScaleDown && (!(button != null) || button.interactable))
			{
				if (tweener != null)
				{
					tweener.Kill();
				}
				tweener = target.DOScale(minimumScale, animationTime).SetUpdate(UpdateType.Normal, true).OnComplete(delegate
				{
					IsScaleDown = true;
				});
			}
		}

		private void UnPress()
		{
			if (!(button != null) || button.interactable)
			{
				if (tweener != null)
				{
					tweener.Kill();
				}
				tweener = target.DOScale(defaultScale, animationTime).SetEase(Ease.OutBack).SetUpdate(UpdateType.Normal, true)
					.OnComplete(delegate
					{
						IsScaleDown = false;
					});
			}
		}
	}
}
