using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CharacterUnlockSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public CharacterUnlockSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.UICharacterUnlocked);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.hasUICharacterUnlocked;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				string newLabel = "CharUnlockData" + entity.uICharacterUnlocked.CharId;
				StateEntity stateEntity = stateContext.CreateEntity();
				stateEntity.isSavedData = true;
				stateEntity.AddName("CharUnlockDataSend");
				stateEntity.AddLabel(newLabel);
				stateEntity.AddBoolean(true);
				StoreNewUnlocked(entity.uICharacterUnlocked.CharId);
			}
		}

		private void StoreNewUnlocked(int charId)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewCharactersUnlocked").SingleEntity();
			NewUnlocksData newUnlocksData = JsonUtility.FromJson<NewUnlocksData>(stateEntity.stringData.Value) ?? new NewUnlocksData();
			if (newUnlocksData.UnlockedIds == null)
			{
				newUnlocksData.UnlockedIds = new List<int>();
			}
			newUnlocksData.UnlockedIds.Add(charId);
			string newValue = JsonUtility.ToJson(newUnlocksData);
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddName("NewCharactersUnlockedSend");
			stateEntity2.AddStringData(newValue);
		}
	}
}
