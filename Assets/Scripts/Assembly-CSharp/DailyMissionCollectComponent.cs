using System.Collections.Generic;
using Entitas;

[Game]
public sealed class DailyMissionCollectComponent : IComponent
{
	public Dictionary<CollectibleType, KeyValuePair<DailyMissionGroup, int>> MissionDict;
}
