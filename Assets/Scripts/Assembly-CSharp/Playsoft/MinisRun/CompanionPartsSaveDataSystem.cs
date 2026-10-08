using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CompanionPartsSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string partsSendKey = "CompPartsDataSend";

		public const string partsKey = "CompPartsData";

		public CompanionPartsSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			Dictionary<int, CompanionsStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.companionsStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				stateContext.CreateIntegerData("CompPartsData" + enumerator.Current.Key, 0, true);
			}
			enumerator.Dispose();
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.hasLabel && entity.name.Name == "CompPartsDataSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName(entity.label.Label).SingleEntity();
				stateEntity.ReplaceInteger(entity.integer.Amount);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
