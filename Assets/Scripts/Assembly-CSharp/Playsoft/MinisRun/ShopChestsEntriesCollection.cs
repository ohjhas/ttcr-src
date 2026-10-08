using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class ShopChestsEntriesCollection : ShopEntriesCollection<ShopChestEntry, ChestData>
	{
		protected override void InitializeEntry(int index, ChestData data)
		{
			Entries[index].SetData(data.Id, data);
		}

		public void SetupEntriesState(int levelsCount, FreeChestData freeChestData, Action<int> pressCallback)
		{
			for (int i = 0; i < levelsCount; i++)
			{
				Entries[i].SetState(freeChestData, pressCallback);
			}
		}
	}
}
