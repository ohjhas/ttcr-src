using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public class SaveDataToPlayerPrefsSystem : ReactiveSystem<StateEntity>
{
	private readonly StateContext stateContext;

	public SaveDataToPlayerPrefsSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
		stateContext = contexts.state;
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.ToSave);
	}

	protected override bool Filter(StateEntity entity)
	{
		return entity.isSavedData;
	}

	protected override void Execute(List<StateEntity> entities)
	{
		foreach (StateEntity entity in entities)
		{
			if (entity.hasInteger)
			{
				PlayerPrefs.SetInt(entity.name.Name, entity.integer.Amount);
			}
			else if (entity.hasBoolean)
			{
				PlayerPrefs.SetInt(entity.name.Name, entity.boolean.Value ? 1 : 0);
			}
			else if (entity.hasStringData)
			{
				PlayerPrefs.SetString(entity.name.Name, entity.stringData.Value);
			}
			else if (entity.hasDateTime)
			{
				PlayerPrefs.SetString(entity.name.Name, DateTimeHelper.ConvertToString(entity.dateTime.Value));
			}
			entity.isToSave = false;
		}
		PlayerPrefs.Save();
	}
}
