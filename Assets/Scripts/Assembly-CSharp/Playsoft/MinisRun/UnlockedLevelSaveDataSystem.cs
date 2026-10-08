using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UnlockedLevelSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string unlockedLevelSendKey = "UnlockedLevelSend";

		public const string unlockedLevelKey = "UnlockedLevel";

		public UnlockedLevelSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateIntegerData("UnlockedLevel", 0, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "UnlockedLevelSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("UnlockedLevel").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				stateEntity.ReplaceInteger(entity.integer.Amount);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
