using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class TutorialOutgameUnlockLevelsBalancingDataComponent : IComponent
{
	public Dictionary<TutorialOutgameLevel, TutorialOutgameUnlockLevelData> Map;
}
