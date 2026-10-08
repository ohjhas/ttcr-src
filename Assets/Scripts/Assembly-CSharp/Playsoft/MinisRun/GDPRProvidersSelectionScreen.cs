using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class GDPRProvidersSelectionScreen : BaseDialog, IO7SdkGdprAdProviderSelectionListener
	{
		[SerializeField]
		private Button selectAllButton;

		[SerializeField]
		private Button unSelectAllButton;

		[SerializeField]
		private Button submitButton;

		[SerializeField]
		private GameObject togglePrefabGo;

		[SerializeField]
		private Transform parentForToggles;

		[SerializeField]
		private Button closeButton;

		private readonly List<GDPRToggleItem> toggleItems = new List<GDPRToggleItem>();

		private bool isInitialized;

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
				return ScreenId.GDPRProvidersSelectionScreen;
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				selectAllButton.onClick.RemoveAllListeners();
				unSelectAllButton.onClick.RemoveAllListeners();
				submitButton.onClick.RemoveAllListeners();
				selectAllButton.onClick.AddListener(OnSelectAllClicked);
				unSelectAllButton.onClick.AddListener(OnUnSelectedAllClicked);
				submitButton.onClick.AddListener(OnSubmitClicked);
				base.uiContext.CreateEntity().AddO7SdkGdprAdProviderSelectionListener(this);
				closeButton.gameObject.SetActive(!GameIntroHelper.IsIntroTime() && !Contexts.sharedInstance.state.sessionEntity.isO7SdkForceGdpr);
			}
			else
			{
				isInitialized = false;
				ClearToggles();
			}
		}

		private void OnSubmitClicked()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprAdProvidersSubmitSelection = true;
			stateEntity.isO7SdkGdprProvidersSendRequest = true;
		}

		private void OnUnSelectedAllClicked()
		{
			ToggleAll(false);
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprAdProvidersUnSelectAll = true;
		}

		private void OnSelectAllClicked()
		{
			ToggleAll(true);
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprAdProvidersSelectAll = true;
		}

		private void InitializeToggles(List<AdProvider> providers)
		{
			foreach (AdProvider provider in providers)
			{
				GameObject gameObject = Object.Instantiate(togglePrefabGo, parentForToggles, false);
				gameObject.gameObject.SetActive(true);
				GDPRToggleItem component = gameObject.GetComponent<GDPRToggleItem>();
				toggleItems.Add(component);
				component.SetToggle(provider);
			}
		}

		private void ClearToggles()
		{
			foreach (GDPRToggleItem toggleItem in toggleItems)
			{
				Object.Destroy(toggleItem.gameObject);
			}
			toggleItems.Clear();
		}

		public void SetData(List<AdProvider> providers)
		{
			if (!isInitialized)
			{
				InitializeToggles(providers);
				isInitialized = true;
			}
		}

		private void ToggleAll(bool isSelected)
		{
			foreach (GDPRToggleItem toggleItem in toggleItems)
			{
				toggleItem.SetToggleState(isSelected);
			}
		}
	}
}
