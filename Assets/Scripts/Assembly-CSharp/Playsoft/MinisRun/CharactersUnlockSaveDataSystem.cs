using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CharactersUnlockSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string unlockSendKey = "CharUnlockDataSend";

		public const string unlockKey = "CharUnlockData";

		public CharactersUnlockSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				stateContext.CreateBoolData("CharUnlockData" + enumerator.Current.Key, false, true);
			}
			enumerator.Dispose();
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.hasLabel && entity.name.Name == "CharUnlockDataSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName(entity.label.Label).SingleEntity();
				stateEntity.ReplaceBoolean(entity.boolean.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
