using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class NewCompanionsUnlockedDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public NewCompanionsUnlockedDataProviderSystem(Contexts contexts)
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
			return entity.isUINewCompanionUnlockedRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewCompanionsUnlocked").SingleEntity();
			foreach (UIEntity entity in entities)
			{
				NewUnlocksData newUnlocksData = JsonUtility.FromJson<NewUnlocksData>(stateEntity.stringData.Value);
				entity.AddUINewCompanionsUnlockedResponse(newUnlocksData ?? new NewUnlocksData());
			}
		}
	}
}
