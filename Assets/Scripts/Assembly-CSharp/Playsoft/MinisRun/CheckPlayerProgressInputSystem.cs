using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CheckPlayerProgressInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public CheckPlayerProgressInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UIAction);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUICheckPlayerProgress;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
			int amount = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity().integer.Amount;
			int amount2 = stateContext.GetEntitiesWithName("PlayerLevelExp").SingleEntity().integer.Amount;
			Dictionary<int, PlayerLevelData> map = stateContext.balancingDataEntity.playerLevelBalancingData.Map;
			PlayerProgressionUpdateData progressUpdateData = GetProgressUpdateData();
			if (progressUpdateData != null && progressUpdateData.LastVisibleExp < amount2)
			{
				CurrencyAndExperienceHelper.OpenScreenForExperienceUpdate(amount, amount2, map, progressUpdateData);
				UpdateProgressionUpdateData(amount, amount2);
				CheckDoesPlayerLevelUp(progressUpdateData.LastVisbileLevel, amount);
			}
		}

		private PlayerProgressionUpdateData GetProgressUpdateData()
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerProgressionUpdate").SingleEntity();
			if (stateEntity.hasStringData && !string.IsNullOrEmpty(stateEntity.stringData.Value))
			{
				return JsonUtility.FromJson<PlayerProgressionUpdateData>(stateEntity.stringData.Value);
			}
			return null;
		}

		private void UpdateProgressionUpdateData(int currentLevelNumber, int currentExpAmount)
		{
			string newValue = JsonUtility.ToJson(new PlayerProgressionUpdateData(currentLevelNumber, currentExpAmount));
			StateEntity stateEntity = Contexts.sharedInstance.state.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("PlayerProgressionUpdateSend");
			stateEntity.AddStringData(newValue);
		}

		private void CheckDoesPlayerLevelUp(int prevLevel, int currentLevel)
		{
			for (int i = prevLevel + 1; i <= currentLevel; i++)
			{
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.AddPlayerLevelUp(i);
			}
		}
	}
}
