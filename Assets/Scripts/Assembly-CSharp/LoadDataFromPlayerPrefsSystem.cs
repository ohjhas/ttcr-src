using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;
using UnityEngine;

public class LoadDataFromPlayerPrefsSystem : ReactiveSystem<StateEntity>
{
	private readonly StateContext stateContext;

	private readonly IGroup<UIEntity> loadListeners;

	public LoadDataFromPlayerPrefsSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
		stateContext = contexts.state;
		loadListeners = contexts.uI.GetGroup(UIMatcher.SaveDataLoadedListener);
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.ToLoad);
	}

	protected override bool Filter(StateEntity entity)
	{
		return entity.isSavedData;
	}

	protected override void Execute(List<StateEntity> entities)
	{
		foreach (StateEntity entity in entities)
		{
			if (entity.hasInteger && PlayerPrefs.HasKey(entity.name.Name))
			{
				int newAmount = PlayerPrefs.GetInt(entity.name.Name);
				entity.ReplaceInteger(newAmount);
			}
			else if (entity.hasBoolean && PlayerPrefs.HasKey(entity.name.Name))
			{
				int num = PlayerPrefs.GetInt(entity.name.Name);
				entity.ReplaceBoolean(num == 1);
			}
			else if (entity.hasStringData && PlayerPrefs.HasKey(entity.name.Name))
			{
				string newValue = PlayerPrefs.GetString(entity.name.Name);
				entity.ReplaceStringData(newValue);
			}
			else if (entity.hasDateTime)
			{
				if (PlayerPrefs.HasKey(entity.name.Name))
				{
					string value = PlayerPrefs.GetString(entity.name.Name);
					entity.ReplaceDateTime(DateTimeHelper.ConvertToDateTime(value));
				}
				else
				{
					PlayerPrefs.SetString(entity.name.Name, DateTimeHelper.ConvertToString(entity.dateTime.Value));
				}
			}
			entity.isToLoad = false;
		}
		StateEntity stateEntity = stateContext.CreateEntity();
		stateEntity.isInitLocalUserState = true;
		UIEntity[] entities2 = loadListeners.GetEntities();
		foreach (UIEntity uIEntity in entities2)
		{
			uIEntity.saveDataLoadedListener.listener.OnDataLoaded();
		}
	}
}
