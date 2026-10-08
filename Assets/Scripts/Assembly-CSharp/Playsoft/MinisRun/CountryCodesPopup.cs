using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Entitas;
using I2.Loc;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class CountryCodesPopup : BasePopupScreen
	{
		[SerializeField]
		private Button codeButton;

		[SerializeField]
		private RectTransform contentRectTransform;

		[SerializeField]
		private VerticalLayoutGroup layoutGroup;

		[SerializeField]
		private RectTransform contentScrollTransform;

		private List<CountryCodeItem> codesList = new List<CountryCodeItem>();

		private bool isInitialized;

		private readonly string countryCodePrefix = "country_code_";

		public override ScreenId Id
		{
			get
			{
				return ScreenId.CountryCodesPopup;
			}
		}

		private StateContext stateContext
		{
			get
			{
				return Contexts.sharedInstance.state;
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (!visible)
			{
				base.uiContext.OpenDialog<PrivacyAndLegalPopup>();
				return;
			}
			if (!isInitialized)
			{
				isInitialized = true;
				CreateCountryCodeLayout();
			}
			PositionCountryCode();
		}

		private void CreateCountryCodeLayout()
		{
			Dictionary<string, string> sortedCountryCodesDictionary = GetSortedCountryCodesDictionary();
			List<string> list = sortedCountryCodesDictionary.Keys.ToList();
			for (int i = 0; i < list.Count; i++)
			{
				string code = list[i];
				Button button = Object.Instantiate(codeButton, contentRectTransform, false);
				button.gameObject.SetActive(true);
				CountryCodeItem component = button.GetComponent<CountryCodeItem>();
				component.SetText(code, sortedCountryCodesDictionary[code]);
				button.onClick.AddListener(delegate
				{
					SetCountryCode(code);
				});
				codesList.Add(component);
			}
			LayoutRebuilder.ForceRebuildLayoutImmediate(contentRectTransform);
		}

		private void SetCountryCode(string code)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.AddO7SdkSelectCountryCode(code);
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.AddName("countryCodeSendKey");
			stateEntity2.AddStringData(code);
			stateEntity2.isSavedData = true;
			base.uiContext.CloseDialog(this);
		}

		private void PositionCountryCode()
		{
			float num = 0f;
			string selectedCountryCode = GetSelectedCountryCode();
			if (string.IsNullOrEmpty(selectedCountryCode))
			{
				contentRectTransform.localPosition = new Vector2(contentRectTransform.localPosition.x, 0f);
				return;
			}
			foreach (CountryCodeItem codes in codesList)
			{
				if (codes.countryCode == selectedCountryCode)
				{
					num -= contentScrollTransform.rect.height / 2f - ((RectTransform)codes.transform).rect.height / 2f - layoutGroup.spacing;
					break;
				}
				num += ((RectTransform)codes.transform).rect.height + layoutGroup.spacing;
			}
			Vector2 vector = new Vector2(contentRectTransform.localPosition.x, num);
			contentRectTransform.DOLocalMove(vector, 1f).SetUpdate(true);
		}

		private Dictionary<string, string> GetSortedCountryCodesDictionary()
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			List<string> codes = stateContext.o7SdkEntity.o7SdkCountryCodes.codes;
			foreach (string item in codes)
			{
				dictionary.Add(item, ScriptLocalization.Get(countryCodePrefix + item));
			}
			return dictionary.OrderBy((KeyValuePair<string, string> x) => x.Value).ToDictionary((KeyValuePair<string, string> x) => x.Key, (KeyValuePair<string, string> x) => x.Value);
		}

		private string GetSelectedCountryCode()
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("countryCodeKey").SingleEntity();
			return (stateEntity == null || !stateEntity.hasStringData) ? string.Empty : stateEntity.stringData.Value;
		}
	}
}
