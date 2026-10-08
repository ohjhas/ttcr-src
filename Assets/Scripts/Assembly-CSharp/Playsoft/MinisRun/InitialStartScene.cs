using UnityEngine;

namespace Playsoft.MinisRun
{
	public class InitialStartScene : MonoBehaviour
	{
		[SerializeField]
		private AgeGateScreen ageGateScreen;

		[SerializeField]
		private GenderGateScreen genderGateScreen;

		[SerializeField]
		private RestoreScreen restoreScreen;

		[SerializeField]
		private RestoreConfirmPopup restoreConfirmPopup;

		[SerializeField]
		private IntroController introController;

		[SerializeField]
		private GDPRSelectionScreen gdprSelectionScreen;

		[SerializeField]
		private GDPRProvidersSelectionScreen gdprProvidersSelectionScreen;

		public IntroController IntroController
		{
			get
			{
				return introController;
			}
		}

		private void Start()
		{
			ageGateScreen.SetVisible(false);
			genderGateScreen.SetVisible(false);
			restoreScreen.SetVisible(false);
			gdprProvidersSelectionScreen.SetVisible(false);
			gdprSelectionScreen.SetVisible(false);
			restoreConfirmPopup.SetVisible(false);
			Contexts.sharedInstance.state.CreateEntity().AddInitialStartSceneDisplayed(this);
		}

		public void DisplayAgeGate()
		{
			ageGateScreen.SetVisible(true);
		}

		public void DisplayGenderGate()
		{
			genderGateScreen.SetVisible(true);
		}

		public void DisplayRestoreScreen()
		{
			gdprProvidersSelectionScreen.SetVisible(false);
			gdprSelectionScreen.SetVisible(false);
			restoreScreen.SetVisible(true);
		}

		public void DisplayRestoreConfirmPopup()
		{
			restoreConfirmPopup.SetVisible(true);
		}

		public void DisplayIntro()
		{
			ageGateScreen.gameObject.SetActive(false);
			genderGateScreen.gameObject.SetActive(false);
			restoreScreen.gameObject.SetActive(false);
			gdprProvidersSelectionScreen.SetVisible(false);
			gdprSelectionScreen.SetVisible(false);
			restoreConfirmPopup.gameObject.SetActive(false);
			introController.PlayIntro();
		}

		public void DisplayGdprSelectionScreen()
		{
			gdprSelectionScreen.SetVisible(true);
		}

		public void DisplayGdprProviderSelectionScreen()
		{
			gdprProvidersSelectionScreen.SetVisible(true);
		}
	}
}
