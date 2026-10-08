using System.Collections.Generic;
using Entitas;
using Playsoft.MinisRun;

[State]
public sealed class BoughtChestPreparedComponent : IComponent
{
	public List<GeneratedReward> PreparedChestRewards;
}
