using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class JumpButton : ActionButton, IPointerDownHandler, IPauseListener, IEventSystemHandler
	{
		private bool isPaused;

		private void Awake()
		{
			GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
			gameEntity.AddPauseListener(this);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!isPaused)
			{
				InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
				inputEntity.isInput = true;
				inputEntity.isInputJump = true;
				inputEntity.AddBuffer(SettingsService.SettingsSO.JumpBufferDuration);
			}
		}

		public void OnPause(bool paused, float timescale)
		{
			isPaused = paused;
			GetComponent<Button>().interactable = !paused;
		}
	}
}
