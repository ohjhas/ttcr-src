using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class ShopSoftCurrencyEntriesCollection : ShopEntriesCollection<ShopSoftCurrencyEntry, SoftCurrencyPacksData>
	{
		protected override void InitializeEntry(int index, SoftCurrencyPacksData data)
		{
			Entries[index].SetData(data.Id, data);
		}

		public void SetupEntriesState(int levelsCount)
		{
			for (int i = 0; i < levelsCount; i++)
			{
				Entries[i].SetState();
			}
		}
	}
}
