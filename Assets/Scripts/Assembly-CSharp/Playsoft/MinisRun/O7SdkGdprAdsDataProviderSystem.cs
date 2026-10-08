using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkGdprAdsDataProviderSystem : ReactiveSystem<UIEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		private O7SDKGdprManager manager;

		public O7SdkGdprAdsDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (stateContext.o7SdkEntity != null)
			{
				manager = stateContext.o7SdkEntity.o7SdkGdprManager.manager;
			}
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.O7SdkGdprAdProviderSelectionListener);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				StateEntity o7SdkEntity = stateContext.o7SdkEntity;
				if (o7SdkEntity == null)
				{
					break;
				}
				if (!o7SdkEntity.hasO7SdkGdprAdProviders)
				{
					FetchAdProviders();
				}
				List<AdProvider> providers = o7SdkEntity.o7SdkGdprAdProviders.providers;
				entity.o7SdkGdprAdProviderSelectionListener.listener.SetData(providers);
			}
		}

		private void FetchAdProviders()
		{
			StateEntity o7SdkEntity = stateContext.o7SdkEntity;
			string gdprConsentProviders = manager.GetGdprConsentProviders(2);
			if (!string.IsNullOrEmpty(gdprConsentProviders))
			{
				List<AdProvider> newProviders = JsonHelper.FromJson<AdProvider>(gdprConsentProviders);
				o7SdkEntity.ReplaceO7SdkGdprAdProviders(newProviders);
			}
		}
	}
}
