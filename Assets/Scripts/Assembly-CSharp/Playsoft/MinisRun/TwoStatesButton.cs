using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class TwoStatesButton : MonoBehaviour
	{
		[SerializeField]
		protected Button Button;

		[SerializeField]
		private WSColorChanger Graphics;

		public void SetInteractable(bool interactable)
		{
			Button.interactable = interactable;
			Color color = ((!interactable) ? Button.colors.disabledColor : Button.colors.normalColor);
			Graphics.SetColor(color);
		}

		public void AddListener(UnityAction callback)
		{
			Button.onClick.RemoveAllListeners();
			Button.onClick.AddListener(callback);
		}

		public void RemoveAllListeners()
		{
			Button.onClick.RemoveAllListeners();
		}
	}
}
