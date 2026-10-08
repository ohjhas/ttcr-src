using System.Collections.Generic;
using Entitas;

public class StateDestroySystem : ReactiveSystem<StateEntity>
{
	public StateDestroySystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.Destroyed.Added());
	}

	protected override bool Filter(StateEntity entity)
	{
		return true;
	}

	protected override void Execute(List<StateEntity> entities)
	{
		foreach (StateEntity entity in entities)
		{
			if (entity.isEnabled)
			{
				entity.Destroy();
			}
		}
	}
}
