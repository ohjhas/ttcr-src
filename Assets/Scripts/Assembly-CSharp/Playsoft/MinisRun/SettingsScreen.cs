using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class SettingsScreen : BasePopupScreen, IO7SdkAdTrackingActiveListener, IO7SdkGdprOptOutListener
	{
		[SerializeField]
		private Button privacyButton;

		[SerializeField]
		private Button contactButton;

		[SerializeField]
		private Button gdprButton;

		[SerializeField]
		private GameObject adTrackingGO;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.SettingsScreen;
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				privacyButton.onClick.RemoveAllListeners();
				privacyButton.onClick.AddListener(OpenPrivacyAndLegal);
				contactButton.onClick.RemoveAllListeners();
				contactButton.onClick.AddListener(OpenContact);
				gdprButton.onClick.RemoveAllListeners();
				gdprButton.onClick.AddListener(OnGdprClicked);
				base.uiContext.CreateEntity().AddO7SdkAdTrackingActiveListener(this);
				base.uiContext.CreateEntity().AddO7SdkGdprOptOutListener(this);
			}
		}

		private void OpenPrivacyAndLegal()
		{
			if (base.IsClickable)
			{
				base.uiContext.CloseDialog(this);
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isSettingsScreen = true;
				uIEntity.isUIOpenPrivacyAndLegacy = true;
			}
		}

		private void OpenContact()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isSettingsScreen = true;
				uIEntity.isUIOpenContacts = true;
			}
		}

		public void EnableAdTracking(bool isTrackingEnabled)
		{
			adTrackingGO.SetActive(isTrackingEnabled);
		}

		private void OnGdprClicked()
		{
			UIEntity uIEntity = base.uiContext.CreateEntity();
			uIEntity.isSettingsScreen = true;
			uIEntity.isUIOpenGdpr = true;
		}

		public void ShowOptOut(bool canShow)
		{
			gdprButton.gameObject.SetActive(canShow);
		}
	}
}
