using System.Collections.Generic;
using Entitas;

public class RelayPlayerInputSystem : ReactiveSystem<UIEntity>
{
	private readonly GameContext gameContext;

	private readonly StateContext stateContext;

	public RelayPlayerInputSystem(Contexts contexts)
		: base((IContext<UIEntity>)contexts.uI)
	{
		gameContext = contexts.game;
		stateContext = contexts.state;
	}

	protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
	{
		return context.CreateCollector(UIMatcher.RelayPlayerScreen);
	}

	protected override bool Filter(UIEntity entity)
	{
		return true;
	}

	protected override void Execute(List<UIEntity> entities)
	{
		gameContext.playerEntity.isPlayerRelay = true;
		RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
		int num = restoreSettingsBalancingData.Map[RestoreSettings.RelayCost];
		StateEntity stateEntity = stateContext.GetEntitiesWithName("Coins").SingleEntity();
		stateEntity.ReplaceInteger(stateEntity.integer.Amount - num);
		stateEntity.isToSave = true;
		foreach (UIEntity entity in entities)
		{
			entity.isDestroyed = true;
		}
	}
}
