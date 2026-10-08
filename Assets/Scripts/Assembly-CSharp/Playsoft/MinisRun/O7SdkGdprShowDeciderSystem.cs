using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkGdprShowDeciderSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public O7SDKGdprManager gdprManager;

		public O7SdkGdprShowDeciderSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		public void Initialize()
		{
			StateEntity o7SdkEntity = stateContext.o7SdkEntity;
			if (o7SdkEntity != null)
			{
				gdprManager = o7SdkEntity.o7SdkGdprManager.manager;
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkGdprRequest);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkGdprShowDecider;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isO7SdkGdprShowDecider)
				{
					if (entity.isO7SdkGdprShowFromSetting)
					{
						FetchAdProviders(GdprAdProviderMode.Settings);
						stateContext.o7SdkEntity.ReplaceO7SdkGdprShowConsentToolType(GDPRShowState.FromSettings);
						ShowGdprProviders();
					}
					else
					{
						GDPRShowState gDPRShowState = (GDPRShowState)gdprManager.MustShowGdprConsent();
						stateContext.o7SdkEntity.ReplaceO7SdkGdprShowConsentToolType(gDPRShowState);
						switch (gDPRShowState)
						{
						case GDPRShowState.DontShow:
							ShowUserState();
							break;
						case GDPRShowState.FirstShow:
						case GDPRShowState.NotFirstShow:
							FetchAdProviders(GdprAdProviderMode.Init);
							ShowGdprConsentTool();
							break;
						}
					}
				}
				entity.isDestroyed = true;
			}
		}

		private void ShowGdprConsentTool()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprShowConsentTool = true;
		}

		private void ShowGdprProviders()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.isO7SdkGdprShowAdProvidersSelection = true;
		}

		private void ShowUserState()
		{
			stateContext.CreateEntity().isUserStateShow = true;
		}

		private void FetchAdProviders(GdprAdProviderMode mode)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkGdprRequest = true;
			stateEntity.AddO7SdkGdprAdProvidersFetch((int)mode);
		}
	}
}
