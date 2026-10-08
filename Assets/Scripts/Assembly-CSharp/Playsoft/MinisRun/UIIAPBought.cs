using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class UIIAPBought : IComponent
	{
		public int IAPId;

		public RectTransform origin;

		public string productId;
	}
}
