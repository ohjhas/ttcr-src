using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class BackButtonBehaviour : MonoBehaviour
	{
		private Button button;

		private void Awake()
		{
			button = GetComponent<Button>();
			if (button != null)
			{
				button.onClick.AddListener(OnBackButton);
			}
		}

		private void OnEnable()
		{
			if (button != null)
			{
				button.interactable = true;
				if ((bool)button.animator)
				{
					button.animator.SetTrigger(button.animationTriggers.normalTrigger);
				}
			}
		}

		private void OnBackButton()
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isGoBack = true;
		}
	}
}
