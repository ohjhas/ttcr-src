using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class O7SdkGdprShowSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public O7SdkGdprShowSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkGdprRequest);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkGdprShowAdProvidersSelection || entity.isO7SdkGdprShowConsentTool;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				ScreenId screenId = stateContext.activeScreenEntity.activeScreen.ScreenId;
				if (screenId == ScreenId.PrivacyAndLegacyPopup || screenId == ScreenId.GDPRSelectionScreen || screenId == ScreenId.HomeScreen)
				{
					if (entity.isO7SdkGdprShowAdProvidersSelection)
					{
						uiContext.OpenDialog<GDPRProvidersSelectionScreen>();
					}
					else if (entity.isO7SdkGdprShowConsentTool)
					{
						uiContext.OpenDialog<GDPRSelectionScreen>();
					}
				}
				entity.isDestroyed = true;
			}
		}
	}
}
