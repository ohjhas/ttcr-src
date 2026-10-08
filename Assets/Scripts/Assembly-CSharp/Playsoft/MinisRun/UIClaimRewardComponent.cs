using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIClaimRewardComponent : IComponent
	{
		public RewardData data;

		public RectTransform origin;

		public bool shouldRefreshScreen;
	}
}
