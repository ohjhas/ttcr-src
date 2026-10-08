using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class DailyMissionInitSystem : IInitializeSystem, ISystem
	{
		private readonly GameContext gameContext;

		public DailyMissionInitSystem(Contexts contexts)
		{
			gameContext = contexts.game;
		}

		public void Initialize()
		{
			HashSet<StateEntity> entitiesWithName = Contexts.sharedInstance.state.GetEntitiesWithName("DailyMissionData");
			if (entitiesWithName.Count > 0)
			{
				DailyMissionsCollectionComponent dailyMissionsCollection = entitiesWithName.SingleEntity().dailyMissionsCollection;
				for (int i = 0; i < dailyMissionsCollection.Collection.DailyMissions.Length; i++)
				{
					AddProgressEntitiesForMission(dailyMissionsCollection.Collection.DailyMissions[i]);
				}
			}
		}

		private void AddProgressEntitiesForMission(DailyMissionData dailyMission)
		{
			GameEntity worldEntity = gameContext.worldEntity;
			DailyMissionGroup dAILYMISSIONGROUP = dailyMission.MissionData.DAILYMISSIONGROUP;
			if (!worldEntity.hasDailyMissionCollect)
			{
				worldEntity.AddDailyMissionCollect(new Dictionary<CollectibleType, KeyValuePair<DailyMissionGroup, int>>());
			}
			switch (dailyMission.MissionData.DAILYMISSIONTYPE)
			{
			case DailyMissionType.Destroy_Obstacles:
				worldEntity.AddDailyMissionDestroyObstacle(dAILYMISSIONGROUP, 0);
				break;
			case DailyMissionType.Play_Missions:
				worldEntity.AddDailyMissionPlayMissions(dAILYMISSIONGROUP);
				break;
			case DailyMissionType.Collect_BoosterGiant:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.GiantToken, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_BoosterMagnet:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.Magnet, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_BoosterScoreMultipier:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.ScoreMultiplierToken, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_BoosterSpeedup:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.SpeedUpToken, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_BoosterStamina:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.StaminaPotion, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_BoosterUpgradeScore:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.UpgradeScoreToken, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_BossterBigStamina:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.BigStaminaPotion, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_Coins:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.GoldCoin, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_CoinsBig:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.BigGoldCoin, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreCake:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.ScoreCake, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreCakeMedium:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.MediumScoreCake, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreCakeBig:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.BigScoreCake, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreCandy:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.ScoreCandy, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreCandyMedium:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.MediumScoreCandy, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreCandyBig:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.BigScoreCandy, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreFruit:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.ScoreFruit, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreFruitMedium:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.MediumScoreFruit, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreFruitBig:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.BigScoreFruit, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreIceCream:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.ScoreIceCream, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreIceCreamMedium:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.MediumScoreIceCream, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreIceCreamBig:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.BigScoreIceCream, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreJelly:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.ScoreJelly, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreJellyMedium:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.MediumScoreJelly, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreJellyBig:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.BigScoreJelly, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreTasty:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.ScoreTasty, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreTastyMedium:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.MediumScoreTasty, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Collect_ScoreTastyBig:
				worldEntity.dailyMissionCollect.MissionDict.Add(CollectibleType.BigScoreTasty, new KeyValuePair<DailyMissionGroup, int>(dAILYMISSIONGROUP, 0));
				break;
			case DailyMissionType.Upgrade_Character:
			case DailyMissionType.Upgrade_Companion:
			case DailyMissionType.Open_Chests:
				break;
			}
		}
	}
}
