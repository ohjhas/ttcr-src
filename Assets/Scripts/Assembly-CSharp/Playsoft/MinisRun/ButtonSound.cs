using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[RequireComponent(typeof(Button))]
	public class ButtonSound : MonoBehaviour, IPointerClickHandler, IEventSystemHandler
	{
		private enum ClickType
		{
			Big = 0,
			Small = 1
		}

		[SerializeField]
		private ClickType clickType;

		[SerializeField]
		private Button button;

		private void Awake()
		{
			if (null == button)
			{
				button = GetComponent<Button>();
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (!(button != null) || (button.enabled && button.interactable))
			{
				Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect((clickType != ClickType.Big) ? "button_tap_small" : "button_tap_big");
			}
		}
	}
}
