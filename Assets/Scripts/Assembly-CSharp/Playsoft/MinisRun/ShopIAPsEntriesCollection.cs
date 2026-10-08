using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class ShopIAPsEntriesCollection : ShopEntriesCollection<ShopIAPEntry, IAPsData>
	{
		protected override void InitializeEntry(int index, IAPsData data)
		{
			Entries[index].SetData(data.Id, data);
		}

		public void SetupEntriesState(Action<int> pressCallback)
		{
			foreach (ShopIAPEntry entry in Entries)
			{
				entry.SetState(pressCallback);
			}
		}
	}
}
