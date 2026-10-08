using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class NewCharactersUnlockedDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public NewCharactersUnlockedDataProviderSystem(Contexts contexts)
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
			return entity.isUINewCharactersUnlockedRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewCharactersUnlocked").SingleEntity();
			foreach (UIEntity entity in entities)
			{
				NewUnlocksData newUnlocksData = JsonUtility.FromJson<NewUnlocksData>(stateEntity.stringData.Value);
				entity.AddUINewCharactersUnlockedResponse(newUnlocksData ?? new NewUnlocksData());
			}
		}
	}
}
