using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PaidUserSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public const string PAID_USER_KEY = "PaidUser";

		public const string PAID_USER_SEND_KEY = "PaidUserSend";

		public PaidUserSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		public void Initialize()
		{
			stateContext.CreateBoolData("PaidUser", false, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "PaidUserSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("PaidUser").SingleEntity();
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
