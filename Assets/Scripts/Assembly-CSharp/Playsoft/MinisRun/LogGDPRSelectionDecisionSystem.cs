using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogGDPRSelectionDecisionSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public LogGDPRSelectionDecisionSystem(Contexts contexts)
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
			return entity.isO7SdkGdprConsentAccepted || entity.isO7SdkGdprConsentDeclined || entity.isO7SdkGdprConsentPrefSelected;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.isO7SdkGdprConsentAccepted)
				{
					string gDPRConsentProvidersJson = O7SdkEventsHelper.GetGDPRConsentProvidersJson();
					SendConsentEvent("consent-decision", "accept", gDPRConsentProvidersJson);
				}
				if (entity.isO7SdkGdprConsentDeclined)
				{
					string gDPRConsentProvidersJson2 = O7SdkEventsHelper.GetGDPRConsentProvidersJson();
					SendConsentEvent("consent-decision", "decline", gDPRConsentProvidersJson2);
				}
				if (entity.isO7SdkGdprConsentPrefSelected)
				{
					SendConsentEvent("consent-decision", "preferences", string.Empty);
				}
			}
		}

		private void SendConsentEvent(string consentType, string p5, string json)
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
				eventCustomComponent.p5 = p5;
				eventCustomComponent.res = null;
				eventCustomComponent.jsonData = json;
				eventCustomComponent.isOnDemand = false;
				EventCustomComponent newComponent = eventCustomComponent;
				stateEntity.AddEventCustomSend(newComponent);
			}
		}
	}
}
