using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class NewCompanionUnlockedSavedDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string NEW_COMPANION_UNLOCKED_SEND = "NewCompanionsUnlockedSend";

		public const string NEW_COMPANION_UNLOCKED_KEY = "NewCompanionsUnlocked";

		public NewCompanionUnlockedSavedDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateStringData("NewCompanionsUnlocked", string.Empty, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "NewCompanionsUnlockedSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewCompanionsUnlocked").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				stateEntity.ReplaceStringData(entity.stringData.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
