using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class PlayerLevelExperienceAmountDataComponent : IComponent
{
	public Dictionary<PlayerExperienceType, List<PlayerLevelExperienceAmountData>> Map;
}
