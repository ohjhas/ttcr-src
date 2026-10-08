using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	internal class SelectedLevelSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string selectedLevelSendKey = "SelectedLevelSend";

		public const string selectedLevelKey = "SelectedLevel";

		public SelectedLevelSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateIntegerData("SelectedLevel", 0, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "SelectedLevelSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("SelectedLevel").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				stateEntity.ReplaceInteger(entity.integer.Amount);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
