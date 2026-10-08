using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialOutgameSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string TUTORIAL_SEND = "TutorialProgressSend";

		public const string TUTORIAL_KEY = "TutorialProgress";

		public TutorialOutgameSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			stateContext.CreateStringData("TutorialProgress", string.Empty, true);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "TutorialProgressSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("TutorialProgress").SingleEntity();
				stateEntity.ReplaceStringData(entity.stringData.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
