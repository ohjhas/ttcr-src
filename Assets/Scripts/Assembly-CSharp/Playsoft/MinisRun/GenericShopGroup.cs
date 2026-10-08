using System;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public abstract class GenericShopGroup<T> : ShopGroup where T : IShopEntriesCollection, new()
	{
		[SerializeField]
		protected RectTransform entriesParent;

		[SerializeField]
		protected T entries = new T();

		[SerializeField]
		protected Vector2 normalAspectSpacing;

		[SerializeField]
		protected Vector2 lowAspectSpacing;

		public override void AdjustBasedOnAspect(bool isLowAspect)
		{
			entries.AdjustEntriesBasedOnAspect(isLowAspect);
			entriesParent.GetComponent<GridLayoutGroup>().spacing = ((!isLowAspect) ? normalAspectSpacing : lowAspectSpacing);
		}
	}
}
