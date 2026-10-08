using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ChangeTimeScaleSystem : ReactiveSystem<StateEntity>
{
	public ChangeTimeScaleSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.ChangeTimeScale.Added());
	}

	protected override bool Filter(StateEntity entity)
	{
		return entity.hasChangeTimeScale;
	}

	protected override void Execute(List<StateEntity> entities)
	{
		foreach (StateEntity entity in entities)
		{
			Time.timeScale = entity.changeTimeScale.scale;
			entity.isDestroyed = true;
		}
	}
}
