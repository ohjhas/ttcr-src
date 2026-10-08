using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class CollectibleProp : AbstractProperty
	{
		[NonSerialized]
		public bool IsImprovedScoreToken;

		public CollectibleType Type;

		public StaminaProp Stamina = new StaminaProp();

		public ScoreProp Scoreable = new ScoreProp();

		public CoinProp Coinable = new CoinProp();

		public FloatingProp Floating = new FloatingProp();

		public SpeedProp Speed = new SpeedProp();

		public CollectibleProp()
		{
		}

		public CollectibleProp(CollectibleProp collectible)
		{
			IsEnabled = collectible.IsEnabled;
			IsImprovedScoreToken = collectible.IsImprovedScoreToken;
			Type = collectible.Type;
			Stamina = new StaminaProp(collectible.Stamina);
			Scoreable = new ScoreProp(collectible.Scoreable);
			Coinable = new CoinProp(collectible.Coinable);
			Floating = new FloatingProp(collectible.Floating);
			Speed = new SpeedProp(collectible.Speed);
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.isCollectible = true;
				switch (Type)
				{
				case CollectibleType.Magnet:
					entity.isCollectibleMagnet = true;
					break;
				case CollectibleType.GiantToken:
					entity.isCollectibleGiantToken = true;
					break;
				case CollectibleType.SpeedUpToken:
					entity.isCollectibleSpeedUp = true;
					break;
				case CollectibleType.ScoreMultiplierToken:
					entity.isCollectibleScoreMultiplier = true;
					break;
				case CollectibleType.UpgradeScoreToken:
					entity.isCollectibleUpgradeScore = true;
					break;
				}
				entity.AddCollectibleData(Type);
				entity.isScoreToken = IsScoreToken(entity);
				entity.isImprovedScoreToken = IsImprovedScoreToken;
				entity.AddSparkle(FindSparkleName(entity));
				entity.AddCollectSound(FindCollectSoundId(entity));
				Stamina.AddToEntity(entity);
				Scoreable.AddToEntity(entity);
				Coinable.AddToEntity(entity);
				Floating.AddToEntity(entity);
				Speed.AddToEntity(entity);
			}
		}

		private bool IsScoreToken(GameEntity entity)
		{
			return entity.hasCollectibleData && (entity.collectibleData.Type == CollectibleType.ScoreJelly || entity.collectibleData.Type == CollectibleType.ScoreIceCream || entity.collectibleData.Type == CollectibleType.ScoreCake || entity.collectibleData.Type == CollectibleType.ScoreFruit || entity.collectibleData.Type == CollectibleType.ScoreCandy || entity.collectibleData.Type == CollectibleType.ScoreTasty || entity.collectibleData.Type == CollectibleType.MediumScoreJelly || entity.collectibleData.Type == CollectibleType.MediumScoreIceCream || entity.collectibleData.Type == CollectibleType.MediumScoreCake || entity.collectibleData.Type == CollectibleType.MediumScoreFruit || entity.collectibleData.Type == CollectibleType.MediumScoreCandy || entity.collectibleData.Type == CollectibleType.MediumScoreTasty || entity.collectibleData.Type == CollectibleType.BigScoreJelly || entity.collectibleData.Type == CollectibleType.BigScoreIceCream || entity.collectibleData.Type == CollectibleType.BigScoreCake || entity.collectibleData.Type == CollectibleType.BigScoreFruit || entity.collectibleData.Type == CollectibleType.BigScoreCandy || entity.collectibleData.Type == CollectibleType.BigScoreTasty);
		}

		private string FindSparkleName(GameEntity entity)
		{
			switch (entity.collectibleData.Type)
			{
			case CollectibleType.GoldCoin:
				return "SparkleCoin";
			case CollectibleType.StaminaPotion:
			case CollectibleType.BigGoldCoin:
			case CollectibleType.BigStaminaPotion:
			case CollectibleType.Magnet:
			case CollectibleType.GiantToken:
			case CollectibleType.SpeedUpToken:
			case CollectibleType.FloatingStamina:
			case CollectibleType.ScoreMultiplierToken:
			case CollectibleType.UpgradeScoreToken:
			case CollectibleType.ScoreTasty:
			case CollectibleType.MediumScoreTasty:
			case CollectibleType.BigScoreTasty:
				return "SparkleBig";
			default:
				return "SparkleRegular";
			}
		}

		private string FindCollectSoundId(GameEntity entity)
		{
			switch (entity.collectibleData.Type)
			{
			case CollectibleType.BigGoldCoin:
				return "collect_big_coin_token";
			case CollectibleType.GoldCoin:
				return "collect_coin_token";
			case CollectibleType.ScoreTasty:
			case CollectibleType.MediumScoreTasty:
			case CollectibleType.BigScoreTasty:
				return "collect_tasty_token";
			case CollectibleType.StaminaPotion:
			case CollectibleType.BigStaminaPotion:
				return "collect_potion";
			default:
				return "collect_generic_token";
			}
		}
	}
}
