using System.Collections.Generic;
using Entitas;

public static class ContextsExtensions
{
	public static HashSet<GameEntity> GetEntitiesWithAsset(this GameContext context, string name)
	{
		return ((EntityIndex<GameEntity, string>)context.GetEntityIndex("Asset")).GetEntities(name);
	}

	public static HashSet<StateEntity> GetEntitiesWithName(this StateContext context, string Name)
	{
		return ((EntityIndex<StateEntity, string>)context.GetEntityIndex("Name")).GetEntities(Name);
	}
}
