using System.Text;
using I2.Loc;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Playsoft.MinisRun
{
	public class SettingsFooter : MonoBehaviour, IPointerDownHandler, IEventSystemHandler
	{
		public enum SettingsFooterHyperlink
		{
			App_Name = 0,
			Outfit_Copyright = 1,
			Others_Copyright = 2
		}

		[SerializeField]
		private TextMeshProUGUI footerText;

		private void OnEnable()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<link=\"app_name\">");
			stringBuilder.Append(ScriptLocalization.application_name);
			stringBuilder.Append("</link> ");
			stringBuilder.Append(Contexts.sharedInstance.state.o7SdkEntity.o7SdkManager.Manager.AppVersion);
			stringBuilder.Append(" ");
			stringBuilder.Append(ScriptLocalization.copyright_term);
			stringBuilder.Append(" <link=\"outfit\"><u>");
			stringBuilder.Append(ScriptLocalization.copyright_outfit_name);
			stringBuilder.Append("</u></link> & <link=\"others\"><u>");
			stringBuilder.Append(ScriptLocalization.copyright_others);
			stringBuilder.Append("</u></link>. ");
			stringBuilder.Append(ScriptLocalization.rights_reserved);
			footerText.text = stringBuilder.ToString();
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			int num = TMP_TextUtilities.FindIntersectingLink(footerText, Input.mousePosition, Camera.main);
			if (num != -1)
			{
				SettingsFooterHyperlink newLinkType = (SettingsFooterHyperlink)num;
				UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
				uIEntity.isUIAction = true;
				uIEntity.AddUISettingsFooterPress(newLinkType);
			}
		}
	}
}
