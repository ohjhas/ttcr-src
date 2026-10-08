using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIChestRewardRequestComponent : IComponent
	{
		public ChestData Chest;

		public List<GeneratedReward> Rewards;
	}
}
