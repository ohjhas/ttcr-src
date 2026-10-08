using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CompanionSelectInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public CompanionSelectInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.CompanionSelectScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUICompanionSelected)
				{
					SetCompanion(entity.uICompanionSelected.CompId);
				}
				else if (entity.hasUICompanionUpgraded)
				{
					UpgradeCompanion(entity.uICompanionUpgraded.CompId);
				}
				else if (entity.hasUINewUnlockedCompanionDisplayed)
				{
					NewCompanionDisplayed(entity.uINewUnlockedCompanionDisplayed.CompanionId);
				}
				else if (entity.hasUIShowCompanionInfo)
				{
					OpenInfoPopup(entity.uIShowCompanionInfo.CompanionId);
				}
				entity.isDestroyed = true;
			}
		}

		private void OpenInfoPopup(int companionId)
		{
			BaseDialog newDialog = uiContext.OpenDialog<CompSelectInfoPopup>();
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddSetup(newDialog);
			uIEntity.AddUICompSingleRequest(companionId);
		}

		private void SetCompanion(int compId)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("CompSelectDataSend");
			stateEntity.AddInteger(compId);
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddSetup(uiContext.uIEntity.hUD.hud);
			uIEntity.isUICompSelectedRequest = true;
			uIEntity.isUICompanionListRequest = true;
			stateContext.CreateEntity().AddPassLevelAction(TutorialOutgameLevel.SelectCompanion);
		}

		private void UpgradeCompanion(int compId)
		{
			CSCompanionData compData = DataProviderHelper.GetCompData(compId);
			int nextUpgradeCostSC = compData.GetNextUpgradeCostSC();
			int nextUpgradeCostParts = compData.GetNextUpgradeCostParts();
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.AddSoftCurrnecyPrice(nextUpgradeCostSC);
			stateEntity.AddCompanionPartsPrice(compId, nextUpgradeCostParts);
			stateEntity.AddBoughtCompanionUpgrade(compId);
			stateEntity.isTransactionBegin = true;
		}

		private void NewCompanionDisplayed(int charId)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewCompanionsUnlocked").SingleEntity();
			NewUnlocksData newUnlocksData = JsonUtility.FromJson<NewUnlocksData>(stateEntity.stringData.Value);
			if (newUnlocksData != null)
			{
				newUnlocksData.UnlockedIds.Remove(charId);
				string newValue = JsonUtility.ToJson(newUnlocksData);
				StateEntity stateEntity2 = stateContext.CreateEntity();
				stateEntity2.isSavedData = true;
				stateEntity2.AddName("NewCompanionsUnlockedSend");
				stateEntity2.AddStringData(newValue);
			}
		}
	}
}
