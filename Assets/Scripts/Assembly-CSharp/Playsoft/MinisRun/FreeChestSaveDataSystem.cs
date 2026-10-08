using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class FreeChestSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		public const string FREE_CHEST_SAVE_KEY = "FreeChestData";

		public const string FREE_CONST_SAVE_SEND_KEY = "FreeChestDataSend";

		private readonly StateContext stateContext;

		public FreeChestSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateDateTimeData("FreeChestData", DateTime.Now, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "FreeChestDataSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("FreeChestData").SingleEntity();
				stateEntity.ReplaceDateTime(entity.dateTime.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
