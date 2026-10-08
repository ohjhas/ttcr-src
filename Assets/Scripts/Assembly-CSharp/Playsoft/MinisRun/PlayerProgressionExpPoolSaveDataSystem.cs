using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayerProgressionExpPoolSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string EXP_POOL_SEND = "PlayerProgressionExpUpdateSend";

		public const string EXP_POOL_KEY = "PlayerProgressionExpUpdate";

		public PlayerProgressionExpPoolSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateIntegerData("PlayerProgressionExpUpdate", 0, false);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "PlayerProgressionExpUpdateSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerProgressionExpUpdate").SingleEntity();
				stateEntity.ReplaceInteger(entity.integer.Amount);
				entity.isDestroyed = true;
			}
		}
	}
}
