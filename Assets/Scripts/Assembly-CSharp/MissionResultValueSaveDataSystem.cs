using System.Collections.Generic;
using Entitas;

public class MissionResultValueSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
{
	private readonly StateContext stateContext;

	public const string RESULT_VALUE_SEND = "ResultValueSend";

	public const string RESULT_VALUE_KEY = "ResultValue";

	public MissionResultValueSaveDataSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		foreach (KeyValuePair<int, MissionsBalancingData> item in stateContext.balancingDataEntity.missionBalancingData.Map)
		{
			stateContext.CreateIntegerData("ResultValue" + item.Key, 0, true);
		}
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.SavedData);
	}

	protected override bool Filter(StateEntity entity)
	{
		return entity.isSavedData && entity.name.Name == "ResultValueSend";
	}

	protected override void Execute(List<StateEntity> entities)
	{
		StateEntity stateEntity = stateContext.GetEntitiesWithName("ResultValue" + SettingsService.SettingsSO.WorldIndex).SingleEntity();
		int amount = stateEntity.integer.Amount;
		foreach (StateEntity entity in entities)
		{
			if (entity.integer.Amount >= amount)
			{
				stateEntity.ReplaceInteger(entity.integer.Amount);
				stateEntity.isToSave = true;
			}
			entity.isDestroyed = true;
		}
	}
}
