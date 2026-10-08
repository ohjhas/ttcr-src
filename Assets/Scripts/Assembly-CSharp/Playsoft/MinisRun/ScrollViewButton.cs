using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class ScrollViewButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDropHandler, IEventSystemHandler
	{
		[FormerlySerializedAs("onClick")]
		[SerializeField]
		private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();

		[SerializeField]
		private Image Source;

		[SerializeField]
		private float buttonSensitivity = 0.05f;

		public bool interactable = true;

		private bool isPointerHoverButton;

		private Vector3 pressPosition;

		public Button.ButtonClickedEvent onClick
		{
			get
			{
				return m_OnClick;
			}
			set
			{
				m_OnClick = value;
			}
		}

		private void OnEnable()
		{
			isPointerHoverButton = false;
		}

		private void OnDisable()
		{
			isPointerHoverButton = false;
		}

		public void OnPointerEnter(PointerEventData data)
		{
			isPointerHoverButton = true;
		}

		public void OnPointerExit(PointerEventData data)
		{
			isPointerHoverButton = false;
		}

		public void OnPointerDown(PointerEventData data)
		{
			pressPosition = Input.mousePosition;
		}

		public void OnPointerUp(PointerEventData data)
		{
			if (isPointerHoverButton && interactable)
			{
				m_OnClick.Invoke();
				isPointerHoverButton = false;
			}
		}

		public void OnDrop(PointerEventData data)
		{
			Vector3 vector = Input.mousePosition - pressPosition;
			float a = Mathf.Abs(vector.x) / (float)Screen.width;
			float b = Mathf.Abs(vector.y) / (float)Screen.width;
			if (isPointerHoverButton && interactable && Mathf.Max(a, b) < buttonSensitivity)
			{
				m_OnClick.Invoke();
				isPointerHoverButton = false;
			}
		}
	}
}
