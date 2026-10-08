using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class NewWorldUnlockedDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public NewWorldUnlockedDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUINewMissionUnlockedRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewLevelUnlocked").SingleEntity();
			foreach (UIEntity entity in entities)
			{
				entity.AddUINewMissionUnlockedResponse(stateEntity.boolean.Value);
			}
		}
	}
}
