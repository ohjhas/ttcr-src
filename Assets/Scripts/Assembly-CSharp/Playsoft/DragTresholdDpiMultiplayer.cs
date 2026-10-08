using UnityEngine;
using UnityEngine.EventSystems;

namespace Playsoft
{
	public class DragTresholdDpiMultiplayer : MonoBehaviour
	{
		private void Start()
		{
			EventSystem current = EventSystem.current;
			if (!(current == null))
			{
				int pixelDragThreshold = current.pixelDragThreshold;
				current.pixelDragThreshold = Mathf.Max(pixelDragThreshold, (int)((float)pixelDragThreshold * Screen.dpi / 160f));
			}
		}
	}
}
