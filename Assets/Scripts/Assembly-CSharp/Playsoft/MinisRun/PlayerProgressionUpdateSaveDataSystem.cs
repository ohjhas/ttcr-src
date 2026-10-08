using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayerProgressionUpdateSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string PROGRESSION_UPDATE_SEND = "PlayerProgressionUpdateSend";

		public const string PROGRESSION_UPDATE_KEY = "PlayerProgressionUpdate";

		public PlayerProgressionUpdateSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateStringData("PlayerProgressionUpdate", string.Empty, false);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "PlayerProgressionUpdateSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerProgressionUpdate").SingleEntity();
				stateEntity.ReplaceStringData(entity.stringData.Value);
				entity.isDestroyed = true;
			}
		}
	}
}
