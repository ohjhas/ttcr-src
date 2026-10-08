using Entitas;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class PrivacyAndLegalPopup : BasePopupScreen, IO7SdkCountryCodeListener
	{
		[SerializeField]
		private Button eulaButton;

		[SerializeField]
		private Button privacyPolicyButton;

		[SerializeField]
		private Button ePrivacyButton;

		[SerializeField]
		private Button countryCodeButton;

		[SerializeField]
		private TextMeshProUGUI countryCodeNameText;

		[SerializeField]
		private TextMeshProUGUI countryCodeText;

		private bool ePrivacyOpened;

		private bool countryCodeOpened;

		public override ScreenId Id
		{
			get
			{
				return ScreenId.PrivacyAndLegacyPopup;
			}
		}

		public void Start()
		{
			base.uiContext.CreateEntity().AddO7SdkCountryCodeListener(this);
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				SetCountryButtonState();
				eulaButton.onClick.RemoveAllListeners();
				eulaButton.onClick.AddListener(OpenEula);
				privacyPolicyButton.onClick.RemoveAllListeners();
				privacyPolicyButton.onClick.AddListener(OpenPrivacyPolicy);
				ePrivacyButton.onClick.RemoveAllListeners();
				ePrivacyButton.onClick.AddListener(OpenEPrivacy);
				countryCodeButton.onClick.RemoveAllListeners();
				countryCodeButton.onClick.AddListener(OpenCountryCode);
			}
			else
			{
				if (!ePrivacyOpened && !countryCodeOpened)
				{
					base.uiContext.OpenDialog<SettingsScreen>();
				}
				countryCodeOpened = false;
				ePrivacyOpened = false;
			}
		}

		private void OpenEula()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isSettingsScreen = true;
				uIEntity.isUIOpenEula = true;
				Contexts.sharedInstance.state.CreateEntity().isEulaClickedEventToBeSent = true;
			}
		}

		private void OpenPrivacyPolicy()
		{
			if (base.IsClickable)
			{
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isSettingsScreen = true;
				uIEntity.isUIOpenPrivacyPolicy = true;
				Contexts.sharedInstance.state.CreateEntity().isPrivacyAndLegalClickedEventToBeSent = true;
			}
		}

		private void OpenEPrivacy()
		{
			if (base.IsClickable)
			{
				ePrivacyOpened = true;
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isSettingsScreen = true;
				uIEntity.isUIOpenEPrivacy = true;
			}
		}

		private void OpenCountryCode()
		{
			if (base.IsClickable)
			{
				countryCodeOpened = true;
				UIEntity uIEntity = base.uiContext.CreateEntity();
				uIEntity.isUIOpenCountryCodes = true;
				uIEntity.isSettingsScreen = true;
			}
		}

		private void SetCountryButtonState()
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("countryCodeKey").SingleEntity();
			SetCountryCodeOnButton((stateEntity == null || !stateEntity.hasStringData) ? string.Empty : stateEntity.stringData.Value);
		}

		public void SetButtonState(bool canShow)
		{
			countryCodeButton.gameObject.SetActive(canShow);
		}

		public void SetCountryCodeOnButton(string code)
		{
			if (code != null)
			{
				if (code == string.Empty)
				{
					countryCodeText.alignment = TextAlignmentOptions.Center;
					countryCodeNameText.text = code;
					SetButtonState(true);
					return;
				}
				if (code == "NotShow")
				{
					SetButtonState(false);
					return;
				}
			}
			countryCodeText.alignment = TextAlignmentOptions.Top;
			countryCodeNameText.text = code;
			SetButtonState(true);
		}
	}
}
