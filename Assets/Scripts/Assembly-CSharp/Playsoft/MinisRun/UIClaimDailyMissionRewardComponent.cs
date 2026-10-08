using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIClaimDailyMissionRewardComponent : IComponent
	{
		public DailyMissionGroup group;

		public RectTransform origin;
	}
}
