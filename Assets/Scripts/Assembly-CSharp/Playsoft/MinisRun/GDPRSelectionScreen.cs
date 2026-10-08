using System.Text;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class GDPRSelectionScreen : BaseDialog, IPointerDownHandler, IEventSystemHandler
	{
		[SerializeField]
		private Button selectedButton;

		[SerializeField]
		private Button declinedButton;

		[SerializeField]
		private TextMeshProUGUI descriptionText;

		private Camera eventCamera;

		private StateContext stateContext
		{
			get
			{
				return Contexts.sharedInstance.state;
			}
		}

		public override ScreenId Id
		{
			get
			{
				return ScreenId.GDPRSelectionScreen;
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				selectedButton.onClick.RemoveAllListeners();
				declinedButton.onClick.RemoveAllListeners();
				selectedButton.onClick.AddListener(OnSelectedClicked);
				declinedButton.onClick.AddListener(OnDeclinedClicked);
				SetDescription();
			}
		}

		private void Start()
		{
			eventCamera = GetComponentInParent<Canvas>().worldCamera;
		}

		private void OnPrivacyAndPolicyClicked()
		{
			stateContext.CreateEntity().AddOpenUrl(stateContext.balancingDataEntity.privacyAndLegacyBalancingData.Data.Privacyadress);
			stateContext.CreateEntity().isPrivacyAndLegalClickedEventToBeSent = true;
		}

		private void OnSelectedClicked()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprAdProvidersSelectAll = true;
			stateEntity.isO7SdkGdprAdProvidersSubmitSelection = true;
			stateEntity.isO7SdkGdprConsentAccepted = true;
		}

		private void OnDeclinedClicked()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprAdProvidersUnSelectAll = true;
			stateEntity.isO7SdkGdprAdProvidersSubmitSelection = true;
			stateEntity.isO7SdkGdprConsentDeclined = true;
		}

		private void SetDescription()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(ScriptLocalization.GDPR.GDPR_information + "\n");
			string value = AppendLinkTag(ScriptLocalization.GDPR.GDPR_information_partners);
			stringBuilder.Append(value);
			descriptionText.text = stringBuilder.ToString();
		}

		private string AppendLinkTag(string linkedString)
		{
			int num = linkedString.IndexOf(">");
			int num2 = linkedString.IndexOf("<", num);
			string text = linkedString.Substring(num + 1, num2 - num - 1);
			string text2 = text.Insert(0, "<link=\"ManagePref\"><u>");
			text2 = text2.Insert(text2.Length, "</u></link>");
			return linkedString.Replace(text, text2);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			Camera component = GameObject.Find("UICamera").GetComponent<Camera>();
			int num = TMP_TextUtilities.FindIntersectingLink(descriptionText, Input.mousePosition, (!(eventCamera != null)) ? Camera.main : eventCamera);
			if (num != -1)
			{
				ShowProvidersScreen();
			}
		}

		private void ShowProvidersScreen()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprShowAdProvidersSelection = true;
			stateEntity.isO7SdkGdprConsentPrefSelected = true;
		}
	}
}
