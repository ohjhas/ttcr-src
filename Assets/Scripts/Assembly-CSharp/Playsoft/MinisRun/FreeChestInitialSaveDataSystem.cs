using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class FreeChestInitialSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		public const string FREE_CHEST_INITIAL_SAVE_KEY = "FreeChestInitialData";

		public const string FREE_CHEST_INITIAL_SAVE_SEND_KEY = "FreeChestInitialDataSend";

		private readonly StateContext stateContext;

		public FreeChestInitialSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateBoolData("FreeChestInitialData", false, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "FreeChestInitialDataSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("FreeChestInitialData").SingleEntity();
				stateEntity.ReplaceBoolean(entity.boolean.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
