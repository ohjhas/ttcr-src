using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIShowCurrencyUpdateComponent : IComponent
	{
		public int ItemIndex;

		public int ItemAmount;

		public RectTransform origin;

		public bool UpdateTargetManually;
	}
}
