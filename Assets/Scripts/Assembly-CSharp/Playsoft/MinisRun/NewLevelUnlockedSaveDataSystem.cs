using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class NewLevelUnlockedSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string newLevelUnlockedSendKey = "NewLevelUnlockedSend";

		public const string newLevelUnlockedKey = "NewLevelUnlocked";

		public NewLevelUnlockedSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateBoolData("NewLevelUnlocked", false, false);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "NewLevelUnlockedSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewLevelUnlocked").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				stateEntity.ReplaceBoolean(entity.boolean.Value);
				entity.isDestroyed = true;
			}
		}
	}
}
