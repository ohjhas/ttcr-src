using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SdkGdprSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		public readonly StateContext stateContext;

		public O7SDKGdprManager gdprManager;

		public O7SdkGdprSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (stateContext.o7SdkEntity != null)
			{
				gdprManager = stateContext.o7SdkEntity.o7SdkGdprManager.manager;
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkGdprRequest);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkGdprAdProvidersSelectAll || entity.isO7SdkGdprAdProvidersUnSelectAll || entity.isO7SdkGdprAdProvidersSubmitSelection || entity.hasO7SdkGdprEnableAdProvider || entity.hasO7SdkGdprAdProvidersFetch;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasO7SdkGdprAdProvidersFetch)
				{
					FetchAdProviders(entity.o7SdkGdprAdProvidersFetch.mode);
				}
				if (entity.isO7SdkGdprAdProvidersSelectAll)
				{
					SelectAllProviders(true);
				}
				if (entity.isO7SdkGdprAdProvidersUnSelectAll)
				{
					SelectAllProviders(false);
				}
				if (entity.isO7SdkGdprAdProvidersSubmitSelection)
				{
					SubmitAdProviders();
					StateEntity stateEntity = stateContext.CreateEntity();
					stateEntity.isO7SdkGdprDone = true;
					stateEntity.isO7SdkGdprRequest = true;
				}
				if (entity.hasO7SdkGdprEnableAdProvider)
				{
					EnableAdProvider(entity.o7SdkGdprEnableAdProvider.providerId, entity.o7SdkGdprEnableAdProvider.isEnabled);
				}
				entity.isDestroyed = true;
			}
		}

		private void EnableAdProvider(string providerId, bool isEnabled)
		{
			if (!stateContext.o7SdkEntity.hasO7SdkGdprAdProviders)
			{
				Debug.LogError("SDK didnot fetch Providers yet");
				return;
			}
			O7SdkGdprAdProvidersComponent o7SdkGdprAdProviders = stateContext.o7SdkEntity.o7SdkGdprAdProviders;
			AdProvider adProvider = o7SdkGdprAdProviders.providers.Find((AdProvider x) => x.id == providerId);
			if (adProvider != null)
			{
				adProvider.IsConsentProvided = isEnabled;
			}
		}

		private void SelectAllProviders(bool isSelected)
		{
			if (!stateContext.o7SdkEntity.hasO7SdkGdprAdProviders)
			{
				Debug.LogError("SDK didnot fetch Providers yet");
				return;
			}
			O7SdkGdprAdProvidersComponent o7SdkGdprAdProviders = stateContext.o7SdkEntity.o7SdkGdprAdProviders;
			foreach (AdProvider provider in o7SdkGdprAdProviders.providers)
			{
				provider.IsConsentProvided = isSelected;
			}
		}

		private void SubmitAdProviders()
		{
			if (!stateContext.o7SdkEntity.hasO7SdkGdprAdProviders)
			{
				Debug.LogError("SDK didnot fetch Providers yet");
				return;
			}
			O7SdkGdprAdProvidersComponent o7SdkGdprAdProviders = stateContext.o7SdkEntity.o7SdkGdprAdProviders;
			string jsonArray = JsonHelper.ToJson(o7SdkGdprAdProviders.providers);
			jsonArray = JsonHelper.UnWrapArray(jsonArray);
			gdprManager.SetGdprConsentProviders(jsonArray);
		}

		private void FetchAdProviders(int mode)
		{
			StateEntity o7SdkEntity = stateContext.o7SdkEntity;
			string gdprConsentProviders = gdprManager.GetGdprConsentProviders(mode);
			if (!string.IsNullOrEmpty(gdprConsentProviders))
			{
				List<AdProvider> newProviders = JsonHelper.FromJson<AdProvider>(gdprConsentProviders);
				o7SdkEntity.ReplaceO7SdkGdprAdProviders(newProviders);
			}
		}
	}
}
