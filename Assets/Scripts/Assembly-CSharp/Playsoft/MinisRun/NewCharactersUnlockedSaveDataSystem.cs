using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class NewCharactersUnlockedSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string NEW_CHARACTERS_UNLOCKED_SEND = "NewCharactersUnlockedSend";

		public const string NEW_CHARACTERS_UNLOCKED_KEY = "NewCharactersUnlocked";

		public NewCharactersUnlockedSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateStringData("NewCharactersUnlocked", string.Empty, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "NewCharactersUnlockedSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewCharactersUnlocked").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				stateEntity.ReplaceStringData(entity.stringData.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
