using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogGDPRQuestionSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public LogGDPRQuestionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkGdprShowAdProvidersSelection.Added(), StateMatcher.O7SdkGdprShowConsentTool.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isO7SdkGdprShowAdProvidersSelection)
				{
					SendConsentEvent("preferences-question");
				}
				if (entity.isO7SdkGdprShowConsentTool)
				{
					SendConsentEvent("consent-question");
				}
			}
		}

		private void SendConsentEvent(string consentType)
		{
			StateEntity o7SdkEntity = stateContext.o7SdkEntity;
			if (o7SdkEntity != null && o7SdkEntity.hasO7SdkGdprShowConsentToolType)
			{
				GDPRShowState state = o7SdkEntity.o7SdkGdprShowConsentToolType.state;
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isO7SdkCall = true;
				EventCustomComponent eventCustomComponent = new EventCustomComponent();
				eventCustomComponent.eid = consentType;
				eventCustomComponent.gid = "data-protection";
				eventCustomComponent.p1 = O7SdkEventsHelper.GetGDPR_P1_Event_Name(state);
				eventCustomComponent.p2 = O7SdkEventsHelper.GetGDPR_P2_Event_Name(state);
				eventCustomComponent.p3 = null;
				eventCustomComponent.p4 = null;
				eventCustomComponent.p5 = null;
				eventCustomComponent.res = null;
				eventCustomComponent.jsonData = null;
				eventCustomComponent.isOnDemand = false;
				EventCustomComponent newComponent = eventCustomComponent;
				stateEntity.AddEventCustomSend(newComponent);
			}
		}
	}
}
