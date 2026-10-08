using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RateMeSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string rateMeShownKey = "RateMeShown";

		public const string rateMeShownSendKey = "RateMeShownSend";

		public RateMeSaveDataSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			stateContext.CreateIntegerData("RateMeShown", 0, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "RateMeShownSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("RateMeShown").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				stateEntity.ReplaceInteger(entity.integer.Amount);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
