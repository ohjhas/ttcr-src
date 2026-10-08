using UnityEngine;

namespace Playsoft.MinisRun
{
	public abstract class ShopEntriesCollection<T1, T2> : BaseCollectionView<T1, T2>, IShopEntriesCollection where T1 : ShopEntry
	{
		public virtual void RemoveAllListeners()
		{
			for (int i = 0; i < Entries.Count; i++)
			{
				T1 val = Entries[i];
				val.RemoveAllListeners();
			}
		}

		public virtual void DisableEntries()
		{
			foreach (T1 entry in Entries)
			{
				T1 current = entry;
				if (!(null == current))
				{
					current.Disable();
				}
			}
		}

		public virtual void AdjustEntriesBasedOnAspect(bool isLowAspect)
		{
			foreach (T1 entry in Entries)
			{
				T1 current = entry;
				current.transform.localScale = ((!isLowAspect) ? Vector3.one : new Vector3(0.85f, 0.85f, 1f));
			}
		}
	}
}
