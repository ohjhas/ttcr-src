using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MissionChestAvailabilitySaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string missionChestSendKey = "ChestDataSend";

		public const string missionChestKey = "ChestData";

		public MissionChestAvailabilitySaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			foreach (KeyValuePair<int, MissionsBalancingData> item in stateContext.balancingDataEntity.missionBalancingData.Map)
			{
				stateContext.CreateBoolData("ChestData" + item.Key, false, true);
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "ChestDataSend" && entity.hasLabel;
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
