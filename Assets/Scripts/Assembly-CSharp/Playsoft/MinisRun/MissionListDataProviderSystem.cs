using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class MissionListDataProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public MissionListDataProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUIMissionListRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			List<WSLevelData> newLevelDataList = CreateMissionDataList();
			foreach (UIEntity entity in entities)
			{
				entity.AddUIMissionListResponse(newLevelDataList);
			}
		}

		private List<WSLevelData> CreateMissionDataList()
		{
			List<WSLevelData> list = new List<WSLevelData>();
			Dictionary<int, MissionChestsBalancingData> map = stateContext.balancingDataEntity.missionChestsBalancingData.Map;
			foreach (KeyValuePair<int, MissionsBalancingData> item2 in stateContext.balancingDataEntity.missionBalancingData.Map)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("HighScore" + item2.Key).SingleEntity();
				StateEntity stateEntity2 = stateContext.GetEntitiesWithName("MissionData" + item2.Key).SingleEntity();
				StateEntity stateEntity3 = stateContext.GetEntitiesWithName("ResultValue" + item2.Key).SingleEntity();
				int[] itemsToCollect = new int[3]
				{
					item2.Value.Star1,
					item2.Value.Star2,
					item2.Value.Star3
				};
				int num = ((!map.ContainsKey(item2.Key)) ? (-1) : map[item2.Key].Chestid);
				StateEntity stateEntity4 = stateContext.GetEntitiesWithName("ChestData" + item2.Key).SingleEntity();
				WSLevelData item = new WSLevelData
				{
					Name = item2.Value.Levelname,
					Chapter = item2.Value.Chapter,
					Highscore = stateEntity.integer.Amount,
					Stars = stateEntity2.integer.Amount,
					ResultValue = stateEntity3.integer.Amount,
					MissionType = item2.Value.MISSIONTYPE,
					ItemsToCollect = itemsToCollect,
					ChestRewardId = num,
					ChestCollected = (stateEntity4.boolean.Value || num < 0)
				};
				list.Add(item);
			}
			return list;
		}
	}
}
