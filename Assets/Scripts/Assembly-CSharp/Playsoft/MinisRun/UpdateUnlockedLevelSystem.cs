using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class UpdateUnlockedLevelSystem : ReactiveSystem<GameEntity>
	{
		private readonly StateContext stateContext;

		public UpdateUnlockedLevelSystem(Contexts contexts)
			: base((IContext<GameEntity>)contexts.game)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
		{
			return context.CreateCollector(GameMatcher.MissionResult);
		}

		protected override bool Filter(GameEntity entity)
		{
			return entity.isPlayer && entity.hasMissionResult && entity.missionResult.Stars > 0;
		}

		protected override void Execute(List<GameEntity> entities)
		{
			int unlockedLevel = GetUnlockedLevel();
			int num = CheckIfNewLevelUnlocked(unlockedLevel);
			if (unlockedLevel < num)
			{
				SaveUnlockedLevel(num);
				SaveSelectedLevel(num);
				SaveNewLevelUnlockedFlag();
				stateContext.CreateEntity().AddNewLevelUnlocked(num);
			}
		}

		private int CheckIfNewLevelUnlocked(int unlockedLevel)
		{
			int num = unlockedLevel + 1;
			Dictionary<int, MissionsBalancingData> map = stateContext.balancingDataEntity.missionBalancingData.Map;
			Dictionary<int, ChaptersBalancingData> map2 = stateContext.balancingDataEntity.chapterBalancingData.Map;
			if (map.ContainsKey(unlockedLevel) && map.ContainsKey(num) && map2.ContainsKey(map[num].Chapter))
			{
				int worldIndex = SettingsService.SettingsSO.WorldIndex;
				int num2 = CountCollectedStars(unlockedLevel);
				int requiredstars = map2[map[num].Chapter].Requiredstars;
				int chapter = map[unlockedLevel].Chapter;
				int chapter2 = map[num].Chapter;
				return ((worldIndex != unlockedLevel || chapter != chapter2) && (chapter == chapter2 || num2 < requiredstars)) ? unlockedLevel : num;
			}
			return unlockedLevel;
		}

		private int CountCollectedStars(int unlockedLevel)
		{
			int num = 0;
			for (int i = 0; i <= unlockedLevel; i++)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("MissionData" + i).SingleEntity();
				num += stateEntity.integer.Amount;
			}
			return num;
		}

		private void SaveUnlockedLevel(int unlockedLevel)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("UnlockedLevelSend");
			stateEntity.AddInteger(unlockedLevel);
		}

		private void SaveSelectedLevel(int unlockedLevel)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("SelectedLevelSend");
			stateEntity.AddInteger(unlockedLevel);
		}

		private void SaveNewLevelUnlockedFlag()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("NewLevelUnlockedSend");
			stateEntity.AddBoolean(true);
		}

		private int GetUnlockedLevel()
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("UnlockedLevel").SingleEntity();
			return (stateEntity != null) ? stateEntity.integer.Amount : 0;
		}
	}
}
