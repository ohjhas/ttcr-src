using System.Collections.Generic;
using Entitas;
using O7SDK;

namespace Playsoft.MinisRun
{
	public class O7SdkAdTrackingSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private O7SDKUtilityManager utilityManager;

		public O7SdkAdTrackingSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkAdTracking.Added(), StateMatcher.O7SdkGateDone.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			utilityManager = stateContext.o7SdkEntity.o7SdkUtilityManager.Manager;
			foreach (StateEntity entity in entities)
			{
				if (entity.hasO7SdkGateDone)
				{
					SetAdTrackingSaveData(utilityManager.IsAdTrackingDisabled());
					continue;
				}
				StateEntity stateEntity = stateContext.GetEntitiesWithName("AdTrackingEnabled").SingleEntity();
				SetAdTrackingSaveData(!stateEntity.boolean.Value);
				utilityManager.DisableAdTracking(!stateEntity.boolean.Value);
			}
		}

		private void SetAdTrackingSaveData(bool flag)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("AdTrackingEnabledSend");
			stateEntity.AddBoolean(flag);
		}
	}
}
