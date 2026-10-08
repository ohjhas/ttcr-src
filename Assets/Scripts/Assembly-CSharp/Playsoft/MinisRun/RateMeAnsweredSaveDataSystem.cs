using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RateMeAnsweredSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string rateMeDontLikeKey = "RateMeDontLike";

		public const string rateMeAnsweredSendKey = "RateMeDontLikeSend";

		public RateMeAnsweredSaveDataSystem(Contexts context)
			: base((IContext<StateEntity>)context.state)
		{
			stateContext = context.state;
		}

		public void Initialize()
		{
			stateContext.CreateBoolData("RateMeDontLike", false, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "RateMeDontLikeSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("RateMeDontLike").SingleEntity();
			foreach (StateEntity entity in entities)
			{
				stateEntity.ReplaceBoolean(entity.boolean.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
