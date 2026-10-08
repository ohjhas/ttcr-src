using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class DailyMissionsSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISaveDataLoadedListener, ISystem
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public const string dailyMissionSendKey = "DailyMissionDataSend";

		public const string dailyMissionKey = "DailyMissionData";

		public const string dailyMissionCreationTime = "DailyMissionCreationTime";

		private const string dailyMissionChestState = "DailyMissionChestState";

		public static readonly string[] dailyMissionProgress = new string[3] { "DailyMissionFirstMission", "DailyMissionSecondMission", "DailyMissionThirdMission" };

		public DailyMissionsSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		public void Initialize()
		{
			stateContext.CreateData("DailyMissionData", false);
			stateContext.CreateBoolData("DailyMissionChestState", false, true);
			stateContext.CreateDateTimeData("DailyMissionCreationTime", DateTime.MinValue, true);
			stateContext.CreateStringData(dailyMissionProgress[0], string.Empty, true);
			stateContext.CreateStringData(dailyMissionProgress[1], string.Empty, true);
			stateContext.CreateStringData(dailyMissionProgress[2], string.Empty, true);
			uiContext.CreateEntity().AddSaveDataLoadedListener(this);
		}

		public void OnDataLoaded()
		{
			DateTime value = stateContext.GetEntitiesWithName("DailyMissionCreationTime").SingleEntity().dateTime.Value;
			if (value < DailyMissionsHelper.GetLastResetTime())
			{
				DateTime currentTime = Contexts.sharedInstance.state.currentRealTimeEntity.currentRealTime.CurrentTime;
				GenerateNewDailyMissions(currentTime);
				SaveNewDailyMissions();
			}
			else
			{
				LoadDailyMissionsProgress();
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.name.Name == "DailyMissionDataSend" && (entity.hasDailyMissionProgressUpdate || entity.hasDailyMissionClaimUpdate || entity.hasDailyMissionChestStateUpdate);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				if (entity.hasDailyMissionProgressUpdate)
				{
					UpdateMissionProgress(entity.dailyMissionProgressUpdate.Group, entity.dailyMissionProgressUpdate.MissionProgress);
				}
				else if (entity.hasDailyMissionClaimUpdate)
				{
					UpdateMissionClaimState(entity.dailyMissionClaimUpdate.Group, entity.dailyMissionClaimUpdate.IsClaimed);
				}
				else if (entity.hasDailyMissionChestStateUpdate)
				{
					UpdateChestState(entity.dailyMissionChestStateUpdate.IsClaimed);
				}
				entity.isDestroyed = true;
			}
		}

		private void GenerateNewDailyMissions(DateTime currentTime)
		{
			DailyMissionsCollection dailyMissionsCollection = new DailyMissionsCollection();
			dailyMissionsCollection.CreationTime = currentTime;
			dailyMissionsCollection.IsChestClaimed = false;
			dailyMissionsCollection.DailyMissions = new DailyMissionData[3];
			DailyMissionsCollection dailyMissionsCollection2 = dailyMissionsCollection;
			List<DailyMissionsBalancingData> list = new List<DailyMissionsBalancingData>();
			for (int i = 0; i < 3; i++)
			{
				DailyMissionsBalancingData dailyMissionsBalancingData = TakeNewMission((DailyMissionGroup)i, list);
				dailyMissionsCollection2.DailyMissions[i] = new DailyMissionData
				{
					IsClaimed = false,
					CurrentProgress = 0,
					MissionData = dailyMissionsBalancingData
				};
				list.Add(dailyMissionsBalancingData);
			}
			CreateDataEntity(dailyMissionsCollection2);
		}

		private DailyMissionsBalancingData TakeNewMission(DailyMissionGroup group, List<DailyMissionsBalancingData> takenMissions)
		{
			Dictionary<int, DailyMissionsBalancingData> map = stateContext.balancingDataEntity.dailyMissionsBalancingData.Map;
			List<DailyMissionsBalancingData> list = new List<DailyMissionsBalancingData>();
			foreach (KeyValuePair<int, DailyMissionsBalancingData> mission in map)
			{
				if (mission.Value.DAILYMISSIONGROUP.Equals(group) && !takenMissions.Exists((DailyMissionsBalancingData m) => m.DAILYMISSIONTYPE.Equals(mission.Value.DAILYMISSIONTYPE)))
				{
					list.Add(mission.Value);
				}
			}
			return list[UnityEngine.Random.Range(0, list.Count)];
		}

		private void SaveNewDailyMissions()
		{
			DailyMissionsCollection collection = stateContext.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection.Collection;
			SaveCreationTime(collection.CreationTime);
			SaveChestState(collection.IsChestClaimed);
			SaveMission(dailyMissionProgress[0], collection.DailyMissions[0]);
			SaveMission(dailyMissionProgress[1], collection.DailyMissions[1]);
			SaveMission(dailyMissionProgress[2], collection.DailyMissions[2]);
		}

		private void SaveChestState(bool isChestClaimed)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("DailyMissionChestState").SingleEntity();
			stateEntity.ReplaceBoolean(isChestClaimed);
			stateEntity.isToSave = true;
		}

		private void SaveCreationTime(DateTime creationTime)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("DailyMissionCreationTime").SingleEntity();
			stateEntity.ReplaceDateTime(creationTime);
			stateEntity.isToSave = true;
		}

		private void SaveMission(string missionEntityName, DailyMissionData missionData)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName(missionEntityName).SingleEntity();
			stateEntity.ReplaceStringData(JsonUtility.ToJson(missionData));
			stateEntity.isToSave = true;
		}

		private void LoadDailyMissionsProgress()
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("DailyMissionCreationTime").SingleEntity();
			StateEntity stateEntity2 = stateContext.GetEntitiesWithName("DailyMissionChestState").SingleEntity();
			DailyMissionsCollection dailyMissionsCollection = new DailyMissionsCollection();
			dailyMissionsCollection.CreationTime = stateEntity.dateTime.Value;
			dailyMissionsCollection.IsChestClaimed = stateEntity2.boolean.Value;
			dailyMissionsCollection.DailyMissions = new DailyMissionData[3];
			DailyMissionsCollection dailyMissionsCollection2 = dailyMissionsCollection;
			Dictionary<int, DailyMissionsBalancingData> map = stateContext.balancingDataEntity.dailyMissionsBalancingData.Map;
			for (int i = 0; i < 3; i++)
			{
				StateEntity stateEntity3 = stateContext.GetEntitiesWithName(dailyMissionProgress[i]).SingleEntity();
				DailyMissionData dailyMissionData = JsonUtility.FromJson<DailyMissionData>(stateEntity3.stringData.Value);
				dailyMissionData.MissionData = map[dailyMissionData.GetMissionDataId()];
				dailyMissionsCollection2.DailyMissions[i] = dailyMissionData;
			}
			CreateDataEntity(dailyMissionsCollection2);
		}

		private void CreateDataEntity(DailyMissionsCollection newCollection)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("DailyMissionData").SingleEntity();
			stateEntity.ReplaceDailyMissionsCollection(newCollection);
		}

		private void UpdateMissionProgress(DailyMissionGroup group, int missionProgress)
		{
			DailyMissionsCollection collection = stateContext.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection.Collection;
			collection.DailyMissions[(int)group].CurrentProgress = missionProgress;
			SaveMission(dailyMissionProgress[(int)group], collection.DailyMissions[(int)group]);
		}

		private void UpdateMissionClaimState(DailyMissionGroup group, bool isClaimed)
		{
			DailyMissionsCollection collection = stateContext.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection.Collection;
			collection.DailyMissions[(int)group].IsClaimed = isClaimed;
			SaveMission(dailyMissionProgress[(int)group], collection.DailyMissions[(int)group]);
		}

		private void UpdateChestState(bool isClaimed)
		{
			DailyMissionsCollection collection = stateContext.GetEntitiesWithName("DailyMissionData").SingleEntity().dailyMissionsCollection.Collection;
			collection.IsChestClaimed = isClaimed;
			SaveChestState(isClaimed);
		}
	}
}
