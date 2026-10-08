using System.Collections.Generic;
using Entitas;

public class PlayerLevelExperienceSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
{
	public const string PLAYER_LEVEL_EXP_SEND = "PlayerLevelExpSend";

	public const string PLAYER_LEVEL_EXP_KEY = "PlayerLevelExp";

	private readonly StateContext stateContext;

	public PlayerLevelExperienceSaveDataSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
		stateContext = contexts.state;
	}

	public void Initialize()
	{
		stateContext.CreateIntegerData("PlayerLevelExp", 0, true);
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.SavedData);
	}

	protected override bool Filter(StateEntity entity)
	{
		return entity.isSavedData && entity.name.Name == "PlayerLevelExpSend";
	}

	protected override void Execute(List<StateEntity> entities)
	{
		StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerLevelExp").SingleEntity();
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
