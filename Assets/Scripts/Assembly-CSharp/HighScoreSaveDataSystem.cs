using System.Collections.Generic;
using Entitas;

public class HighScoreSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
{
	private readonly StateContext stateContext;

	public const string HighScoreSend = "HighScoreSend";

	public const string HighScoreKey = "HighScore";

	public HighScoreSaveDataSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		foreach (KeyValuePair<int, MissionsBalancingData> item in stateContext.balancingDataEntity.missionBalancingData.Map)
		{
			stateContext.CreateIntegerData("HighScore" + item.Key, 0, false);
		}
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.SavedData);
	}

	protected override bool Filter(StateEntity entity)
	{
		return entity.isSavedData && entity.name.Name == "HighScoreSend";
	}

	protected override void Execute(List<StateEntity> entities)
	{
		StateEntity stateEntity = stateContext.GetEntitiesWithName("HighScore" + SettingsService.SettingsSO.WorldIndex).SingleEntity();
		int amount = stateEntity.integer.Amount;
		foreach (StateEntity entity in entities)
		{
			if (entity.integer.Amount > amount)
			{
				stateEntity.ReplaceInteger(entity.integer.Amount);
				stateEntity.isToSave = true;
			}
			entity.isDestroyed = true;
		}
	}
}
