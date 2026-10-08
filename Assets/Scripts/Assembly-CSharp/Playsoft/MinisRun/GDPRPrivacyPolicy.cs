using System.Text;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Playsoft.MinisRun
{
	public class GDPRPrivacyPolicy : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		[SerializeField]
		private TextMeshProUGUI privacyText;

		private Camera uiCamera;

		private void Awake()
		{
			GameObject gameObject = GameObject.Find("UICamera");
			if (gameObject != null)
			{
				uiCamera = gameObject.GetComponent<Camera>();
			}
		}

		private void OnEnable()
		{
			if (privacyText == null)
			{
				privacyText = GetComponent<TextMeshProUGUI>();
			}
			if (!(privacyText == null))
			{
				SetPrivacyPolicy();
			}
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!(uiCamera == null))
			{
				int num = TMP_TextUtilities.FindIntersectingLink(privacyText, Input.mousePosition, uiCamera);
				StateContext state = Contexts.sharedInstance.state;
				state.CreateEntity().isOpenPrivacyUrl = true;
				state.CreateEntity().isPrivacyAndLegalClickedEventToBeSent = true;
			}
		}

		private void SetPrivacyPolicy()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<link=\"privacy\"><u>");
			stringBuilder.Append(ScriptLocalization.GDPR.GDPR_learn_privacy);
			stringBuilder.Append("</u></link> ");
			privacyText.text = stringBuilder.ToString();
		}
	}
}
