using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class GDPRToggleItem : MonoBehaviour
	{
		[SerializeField]
		private Toggle toggle;

		[SerializeField]
		private TextMeshProUGUI toggleText;

		private AdProvider adProvider;

		private bool isUserChangedBySelectAll;

		private void OnEnable()
		{
			toggle.onValueChanged.RemoveAllListeners();
		}

		private void Start()
		{
			toggle.onValueChanged.AddListener(OnToggleChanged);
		}

		private void OnToggleChanged(bool isOn)
		{
			if (!isUserChangedBySelectAll)
			{
				StateContext state = Contexts.sharedInstance.state;
				StateEntity stateEntity = state.CreateEntity();
				stateEntity.isO7SdkGdprRequest = true;
				stateEntity.AddO7SdkGdprEnableAdProvider(adProvider.id, isOn);
			}
			isUserChangedBySelectAll = false;
		}

		public void SetToggle(AdProvider adProvider)
		{
			this.adProvider = adProvider;
			toggle.isOn = adProvider.IsConsentProvided;
			toggleText.text = adProvider.displayName;
		}

		public void SetToggleState(bool isOn)
		{
			isUserChangedBySelectAll = true;
			toggle.isOn = isOn;
		}
	}
}
