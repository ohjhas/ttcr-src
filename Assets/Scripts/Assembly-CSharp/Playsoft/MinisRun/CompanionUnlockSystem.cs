using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionUnlockSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public CompanionUnlockSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UICompanionUnlocked);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUICompanionUnlocked;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				string newLabel = "CompUnlockData" + entity.uICompanionUnlocked.CompId;
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isSavedData = true;
				stateEntity.AddName("CompUnlockDataSend");
				stateEntity.AddLabel(newLabel);
				stateEntity.AddBoolean(true);
				StoreNewUnlocked(entity.uICompanionUnlocked.CompId);
			}
		}

		private void StoreNewUnlocked(int charId)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewCompanionsUnlocked").SingleEntity();
			NewUnlocksData newUnlocksData = JsonUtility.FromJson<NewUnlocksData>(stateEntity.stringData.Value) ?? new NewUnlocksData();
			if (newUnlocksData.UnlockedIds == null)
			{
				newUnlocksData.UnlockedIds = new List<int>();
			}
			newUnlocksData.UnlockedIds.Add(charId);
			string newValue = JsonUtility.ToJson(newUnlocksData);
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddName("NewCompanionsUnlockedSend");
			stateEntity2.AddStringData(newValue);
		}
	}
}
