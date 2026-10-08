using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class MissionInitSystem : IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		public MissionInitSystem(Contexts contexts)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			if (stateContext.balancingDataEntity != null)
			{
				int worldIndex = SettingsService.SettingsSO.WorldIndex;
				MissionsBalancingData missionsBalancingData = stateContext.balancingDataEntity.missionBalancingData.Map[worldIndex];
				gameContext.worldEntity.AddMissionThresholds(missionsBalancingData.Star1, missionsBalancingData.Star2, missionsBalancingData.Star3);
				gameContext.worldEntity.AddMissionCounter(0);
				gameContext.worldEntity.AddMissionType(missionsBalancingData.MISSIONTYPE);
				AddMissionIcon(missionsBalancingData);
				CheckCollectibleMission(missionsBalancingData);
			}
		}

		private void AddMissionIcon(MissionsBalancingData missionData)
		{
			AssetsSettingsSO.MissionIconAsset missionIcon = SettingsService.AssetsSettingsSO.GetMissionIcon(missionData.MISSIONTYPE);
			if (missionIcon != null)
			{
				gameContext.worldEntity.AddMissionSprite(missionIcon.Size, missionIcon.Sprite);
				gameContext.worldEntity.AddMissionSpriteFillAmount(missionIcon.minFillAmount, missionIcon.minFirstFillAmount);
			}
		}

		private void CheckCollectibleMission(MissionsBalancingData missionData)
		{
			if (IsCollectableMission(missionData.MISSIONTYPE))
			{
				gameContext.worldEntity.AddMissionCollect(GetCollectibleType(missionData.MISSIONTYPE));
			}
		}

		private static bool IsCollectableMission(MissionType missionType)
		{
			return missionType == MissionType.Collect_Coins || missionType == MissionType.Collect_CoinsBig || missionType == MissionType.Collect_ScoreJelly || missionType == MissionType.Collect_ScoreIceCream || missionType == MissionType.Collect_ScoreCake || missionType == MissionType.Collect_ScoreFruit || missionType == MissionType.Collect_ScoreCandy || missionType == MissionType.Collect_ScoreTasty || missionType == MissionType.Collect_ScoreJellyMedium || missionType == MissionType.Collect_ScoreIceCreamMedium || missionType == MissionType.Collect_ScoreCakeMedium || missionType == MissionType.Collect_ScoreFruitMedium || missionType == MissionType.Collect_ScoreCandyMedium || missionType == MissionType.Collect_ScoreTastyMedium || missionType == MissionType.Collect_ScoreJellyBig || missionType == MissionType.Collect_ScoreIceCreamBig || missionType == MissionType.Collect_ScoreCakeBig || missionType == MissionType.Collect_ScoreFruitBig || missionType == MissionType.Collect_ScoreCandyBig || missionType == MissionType.Collect_ScoreTastyBig;
		}

		private static CollectibleType GetCollectibleType(MissionType missionType)
		{
			switch (missionType)
			{
			case MissionType.Collect_Coins:
				return CollectibleType.GoldCoin;
			case MissionType.Collect_CoinsBig:
				return CollectibleType.BigGoldCoin;
			case MissionType.Collect_ScoreJelly:
				return CollectibleType.ScoreJelly;
			case MissionType.Collect_ScoreIceCream:
				return CollectibleType.ScoreIceCream;
			case MissionType.Collect_ScoreCake:
				return CollectibleType.ScoreCake;
			case MissionType.Collect_ScoreFruit:
				return CollectibleType.ScoreFruit;
			case MissionType.Collect_ScoreCandy:
				return CollectibleType.ScoreCandy;
			case MissionType.Collect_ScoreTasty:
				return CollectibleType.ScoreTasty;
			case MissionType.Collect_ScoreJellyMedium:
				return CollectibleType.MediumScoreJelly;
			case MissionType.Collect_ScoreIceCreamMedium:
				return CollectibleType.MediumScoreIceCream;
			case MissionType.Collect_ScoreCakeMedium:
				return CollectibleType.MediumScoreCake;
			case MissionType.Collect_ScoreFruitMedium:
				return CollectibleType.MediumScoreFruit;
			case MissionType.Collect_ScoreCandyMedium:
				return CollectibleType.MediumScoreCandy;
			case MissionType.Collect_ScoreTastyMedium:
				return CollectibleType.MediumScoreTasty;
			case MissionType.Collect_ScoreJellyBig:
				return CollectibleType.BigScoreJelly;
			case MissionType.Collect_ScoreIceCreamBig:
				return CollectibleType.BigScoreIceCream;
			case MissionType.Collect_ScoreCakeBig:
				return CollectibleType.BigScoreCake;
			case MissionType.Collect_ScoreFruitBig:
				return CollectibleType.BigScoreFruit;
			case MissionType.Collect_ScoreCandyBig:
				return CollectibleType.BigScoreCandy;
			case MissionType.Collect_ScoreTastyBig:
				return CollectibleType.BigScoreTasty;
			default:
				Debug.LogError("Wrong mission type! Returning CollectibleType.Gold_Coin");
				return CollectibleType.GoldCoin;
			}
		}
	}
}
