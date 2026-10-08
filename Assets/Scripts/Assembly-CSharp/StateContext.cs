using System;
using Entitas;
using Playsoft.MinisRun;

public sealed class StateContext : Context<StateEntity>
{
	public StateEntity activeScreenEntity
	{
		get
		{
			return GetGroup(StateMatcher.ActiveScreen).GetSingleEntity();
		}
	}

	public ActiveScreenComponent activeScreen
	{
		get
		{
			return activeScreenEntity.activeScreen;
		}
	}

	public bool hasActiveScreen
	{
		get
		{
			return activeScreenEntity != null;
		}
	}

	public StateEntity balancingDataEntity
	{
		get
		{
			return GetGroup(StateMatcher.BalancingData).GetSingleEntity();
		}
	}

	public bool isBalancingData
	{
		get
		{
			return balancingDataEntity != null;
		}
		set
		{
			StateEntity stateEntity = balancingDataEntity;
			if (value != (stateEntity != null))
			{
				if (value)
				{
					CreateEntity().isBalancingData = true;
				}
				else
				{
					stateEntity.Destroy();
				}
			}
		}
	}

	public StateEntity currentRealTimeEntity
	{
		get
		{
			return GetGroup(StateMatcher.CurrentRealTime).GetSingleEntity();
		}
	}

	public CurrentRealTimeComponent currentRealTime
	{
		get
		{
			return currentRealTimeEntity.currentRealTime;
		}
	}

	public bool hasCurrentRealTime
	{
		get
		{
			return currentRealTimeEntity != null;
		}
	}

	public StateEntity localUserStateEntity
	{
		get
		{
			return GetGroup(StateMatcher.LocalUserState).GetSingleEntity();
		}
	}

	public LocalUserStateComponent localUserState
	{
		get
		{
			return localUserStateEntity.localUserState;
		}
	}

	public bool hasLocalUserState
	{
		get
		{
			return localUserStateEntity != null;
		}
	}

	public StateEntity o7SdkEntity
	{
		get
		{
			return GetGroup(StateMatcher.O7Sdk).GetSingleEntity();
		}
	}

	public bool isO7Sdk
	{
		get
		{
			return o7SdkEntity != null;
		}
		set
		{
			StateEntity stateEntity = o7SdkEntity;
			if (value != (stateEntity != null))
			{
				if (value)
				{
					CreateEntity().isO7Sdk = true;
				}
				else
				{
					stateEntity.Destroy();
				}
			}
		}
	}

	public StateEntity previousScreenEntity
	{
		get
		{
			return GetGroup(StateMatcher.PreviousScreen).GetSingleEntity();
		}
	}

	public PreviousScreenComponent previousScreen
	{
		get
		{
			return previousScreenEntity.previousScreen;
		}
	}

	public bool hasPreviousScreen
	{
		get
		{
			return previousScreenEntity != null;
		}
	}

	public StateEntity sessionEntity
	{
		get
		{
			return GetGroup(StateMatcher.Session).GetSingleEntity();
		}
	}

	public bool isSession
	{
		get
		{
			return sessionEntity != null;
		}
		set
		{
			StateEntity stateEntity = sessionEntity;
			if (value != (stateEntity != null))
			{
				if (value)
				{
					CreateEntity().isSession = true;
				}
				else
				{
					stateEntity.Destroy();
				}
			}
		}
	}

	public StateEntity tutorialEntity
	{
		get
		{
			return GetGroup(StateMatcher.Tutorial).GetSingleEntity();
		}
	}

	public bool isTutorial
	{
		get
		{
			return tutorialEntity != null;
		}
		set
		{
			StateEntity stateEntity = tutorialEntity;
			if (value != (stateEntity != null))
			{
				if (value)
				{
					CreateEntity().isTutorial = true;
				}
				else
				{
					stateEntity.Destroy();
				}
			}
		}
	}

	public StateContext()
		: base(226, 0, new ContextInfo("State", StateComponentsLookup.componentNames, StateComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => new SafeAERC(entity)))
	{
	}

	public StateEntity SetActiveScreen(ScreenId newScreenId)
	{
		if (hasActiveScreen)
		{
			throw new EntitasException(string.Concat("Could not set ActiveScreen!\n", this, " already has an entity with ActiveScreenComponent!"), "You should check if the context already has a activeScreenEntity before setting it or use context.ReplaceActiveScreen().");
		}
		StateEntity stateEntity = CreateEntity();
		stateEntity.AddActiveScreen(newScreenId);
		return stateEntity;
	}

	public void ReplaceActiveScreen(ScreenId newScreenId)
	{
		StateEntity stateEntity = activeScreenEntity;
		if (stateEntity == null)
		{
			stateEntity = SetActiveScreen(newScreenId);
		}
		else
		{
			stateEntity.ReplaceActiveScreen(newScreenId);
		}
	}

	public void RemoveActiveScreen()
	{
		activeScreenEntity.Destroy();
	}

	public StateEntity SetCurrentRealTime(DateTime newCurrentTime)
	{
		if (hasCurrentRealTime)
		{
			throw new EntitasException(string.Concat("Could not set CurrentRealTime!\n", this, " already has an entity with CurrentRealTimeComponent!"), "You should check if the context already has a currentRealTimeEntity before setting it or use context.ReplaceCurrentRealTime().");
		}
		StateEntity stateEntity = CreateEntity();
		stateEntity.AddCurrentRealTime(newCurrentTime);
		return stateEntity;
	}

	public void ReplaceCurrentRealTime(DateTime newCurrentTime)
	{
		StateEntity stateEntity = currentRealTimeEntity;
		if (stateEntity == null)
		{
			stateEntity = SetCurrentRealTime(newCurrentTime);
		}
		else
		{
			stateEntity.ReplaceCurrentRealTime(newCurrentTime);
		}
	}

	public void RemoveCurrentRealTime()
	{
		currentRealTimeEntity.Destroy();
	}

	public StateEntity SetLocalUserState(UserStateData newStateData)
	{
		if (hasLocalUserState)
		{
			throw new EntitasException(string.Concat("Could not set LocalUserState!\n", this, " already has an entity with LocalUserStateComponent!"), "You should check if the context already has a localUserStateEntity before setting it or use context.ReplaceLocalUserState().");
		}
		StateEntity stateEntity = CreateEntity();
		stateEntity.AddLocalUserState(newStateData);
		return stateEntity;
	}

	public void ReplaceLocalUserState(UserStateData newStateData)
	{
		StateEntity stateEntity = localUserStateEntity;
		if (stateEntity == null)
		{
			stateEntity = SetLocalUserState(newStateData);
		}
		else
		{
			stateEntity.ReplaceLocalUserState(newStateData);
		}
	}

	public void RemoveLocalUserState()
	{
		localUserStateEntity.Destroy();
	}

	public StateEntity SetPreviousScreen(ScreenId newScreenId)
	{
		if (hasPreviousScreen)
		{
			throw new EntitasException(string.Concat("Could not set PreviousScreen!\n", this, " already has an entity with PreviousScreenComponent!"), "You should check if the context already has a previousScreenEntity before setting it or use context.ReplacePreviousScreen().");
		}
		StateEntity stateEntity = CreateEntity();
		stateEntity.AddPreviousScreen(newScreenId);
		return stateEntity;
	}

	public void ReplacePreviousScreen(ScreenId newScreenId)
	{
		StateEntity stateEntity = previousScreenEntity;
		if (stateEntity == null)
		{
			stateEntity = SetPreviousScreen(newScreenId);
		}
		else
		{
			stateEntity.ReplacePreviousScreen(newScreenId);
		}
	}

	public void RemovePreviousScreen()
	{
		previousScreenEntity.Destroy();
	}
}
