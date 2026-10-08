using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class SlideButton : ActionButton, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPauseListener, IEventSystemHandler
	{
		[HideInInspector]
		public bool isPressed;

		private bool isPaused;

		private void Awake()
		{
			GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
			gameEntity.AddPauseListener(this);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			isPressed = true;
			ContinueSlide();
		}

		public void OnPointerExit(PointerEventData eventData)
		{
			StopSlide();
		}

		public void OnPointerUp(PointerEventData eventData)
		{
			StopSlide();
		}

		private void Update()
		{
			if (isPressed)
			{
				ContinueSlide();
			}
		}

		private void ContinueSlide()
		{
			if (!isPaused)
			{
				InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
				inputEntity.isInput = true;
				inputEntity.isInputSlide = true;
			}
		}

		private void StopSlide()
		{
			if (!isPaused && isPressed)
			{
				ForceStopSlide();
			}
		}

		public void ForceStopSlide()
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isInputSlideStop = true;
			isPressed = false;
		}

		public void OnPause(bool paused, float timescale)
		{
			isPaused = paused;
			if (paused)
			{
				isPressed = false;
			}
			GetComponent<Button>().interactable = !paused;
		}
	}
}
