using System.Collections.Generic;
using Entitas;

public class PlayerLevelNumberSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
{
	public const string PLAYER_LEVEL_NUMBER_SEND = "PlayerLevelNumberSend";

	public const string PLAYER_LEVEL_NUMBER_KEY = "PlayerLevelNumber";

	private readonly StateContext stateContext;

	public PlayerLevelNumberSaveDataSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		stateContext.CreateIntegerData("PlayerLevelNumber", 1, true);
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.SavedData);
	}

	protected override bool Filter(StateEntity entity)
	{
		return entity.isSavedData && entity.name.Name == "PlayerLevelNumberSend";
	}

	protected override void Execute(List<StateEntity> entities)
	{
		StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity();
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
