using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class O7SdkPrivacyPolicyShowSystem : ReactiveSystem<StateEntity>
	{
		public readonly StateContext stateContext;

		public O7SdkPrivacyPolicyShowSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.OpenPrivacyUrl);
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity o7SdkEntity = stateContext.o7SdkEntity;
				if (o7SdkEntity == null || !o7SdkEntity.hasO7SdkPrivacyURL)
				{
					Debug.LogError("No privacy URL available");
					break;
				}
				entity.AddOpenUrl(o7SdkEntity.o7SdkPrivacyURL.url);
			}
		}
	}
}
