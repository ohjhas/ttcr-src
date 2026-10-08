using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class CoinProp : AbstractProperty
	{
		public int Coins;

		public CoinProp()
		{
		}

		public CoinProp(CoinProp coins)
		{
			IsEnabled = coins.IsEnabled;
			Coins = coins.Coins;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddCollectibleCoin(Coins);
			}
		}
	}
}
