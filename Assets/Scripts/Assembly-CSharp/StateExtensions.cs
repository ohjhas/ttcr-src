using System;

public static class StateExtensions
{
	public static StateEntity CreateData(this StateContext stateContext, string name, bool isUserStateData)
	{
		if (stateContext.GetEntitiesWithName(name).Count > 0)
		{
			return null;
		}
		StateEntity stateEntity = stateContext.CreateEntity();
		stateEntity.isSavedData = true;
		stateEntity.isUserStateData = isUserStateData;
		stateEntity.AddName(name);
		stateEntity.isToLoad = true;
		return stateEntity;
	}

	public static void CreateIntegerData(this StateContext stateContext, string name, int defaultValue, bool isUserStateData)
	{
		StateEntity stateEntity = stateContext.CreateData(name, isUserStateData);
		if (stateEntity != null)
		{
			stateEntity.AddInteger(defaultValue);
		}
	}

	public static void CreateBoolData(this StateContext stateContext, string name, bool defaultValue, bool isUserStateData)
	{
		StateEntity stateEntity = stateContext.CreateData(name, isUserStateData);
		if (stateEntity != null)
		{
			stateEntity.AddBoolean(defaultValue);
		}
	}

	public static void CreateStringData(this StateContext stateContext, string name, string defaultValue, bool isUserStateData)
	{
		StateEntity stateEntity = stateContext.CreateData(name, isUserStateData);
		if (stateEntity != null)
		{
			stateEntity.AddStringData(defaultValue);
		}
	}

	public static void CreateDateTimeData(this StateContext stateContext, string name, DateTime defaultValue, bool isUserStateData)
	{
		StateEntity stateEntity = stateContext.CreateData(name, isUserStateData);
		if (stateEntity != null)
		{
			stateEntity.AddDateTime(defaultValue);
		}
	}
}
