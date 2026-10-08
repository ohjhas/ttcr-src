using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CompanionSelectedSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string selectSendKey = "CompSelectDataSend";

		public const string selectKey = "CompSelectData";

		public CompanionSelectedSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateIntegerData("CompSelectData", -1, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "CompSelectDataSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("CompSelectData").SingleEntity();
				stateEntity.ReplaceInteger(entity.integer.Amount);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
