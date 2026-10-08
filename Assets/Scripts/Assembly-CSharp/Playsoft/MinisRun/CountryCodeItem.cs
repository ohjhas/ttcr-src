using TMPro;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CountryCodeItem : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI codeText;

		public string countryCode = string.Empty;

		public void SetText(string code, string localtext)
		{
			countryCode = code;
			codeText.text = localtext;
		}
	}
}
