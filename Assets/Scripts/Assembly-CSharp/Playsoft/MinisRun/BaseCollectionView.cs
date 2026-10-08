using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class BaseCollectionView<T1, T2> where T1 : MonoBehaviour
	{
		[SerializeField]
		protected List<T1> Entries;

		[Space]
		[SerializeField]
		protected T1 EntryPrefab;

		public List<T1> GetEntries()
		{
			return Entries;
		}

		public virtual void CreateEntriesIfNeeded(Transform parent, int count)
		{
			while (Entries.Count < count)
			{
				T1 item = UnityEngine.Object.Instantiate(EntryPrefab);
				item.transform.SetParent(parent);
				item.transform.localPosition = Vector3.zero;
				item.transform.localScale = Vector3.one;
				Entries.Add(item);
			}
		}

		public virtual void SetupEntries(List<T2> dataList)
		{
			for (int i = 0; i < Entries.Count; i++)
			{
				if (i < dataList.Count)
				{
					T1 val = Entries[i];
					val.gameObject.SetActive(true);
					InitializeEntry(i, dataList[i]);
				}
				else
				{
					T1 val2 = Entries[i];
					val2.gameObject.SetActive(false);
				}
			}
		}

		protected virtual void InitializeEntry(int index, T2 data)
		{
		}
	}
}
