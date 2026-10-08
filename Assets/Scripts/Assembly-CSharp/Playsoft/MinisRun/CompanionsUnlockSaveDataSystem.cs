using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CompanionsUnlockSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string unlockSendKey = "CompUnlockDataSend";

		public const string unlockKey = "CompUnlockData";

		public CompanionsUnlockSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			Dictionary<int, CompanionsStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.companionsStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				stateContext.CreateBoolData("CompUnlockData" + enumerator.Current.Key, false, true);
			}
			enumerator.Dispose();
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.hasLabel && entity.name.Name == "CompUnlockDataSend";
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
