using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class AdTrackingSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string AD_TRACKING_KEY = "AdTrackingEnabled";

		public const string AD_TRACKING_SEND_KEY = "AdTrackingEnabledSend";

		public AdTrackingSaveDataSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			stateContext.CreateBoolData("AdTrackingEnabled", false, false);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "AdTrackingEnabledSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("AdTrackingEnabled").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				if (entity.boolean.Value != stateEntity.boolean.Value)
				{
					stateEntity.ReplaceBoolean(entity.boolean.Value);
					stateEntity.isToSave = true;
				}
				entity.isDestroyed = true;
			}
		}
	}
}
