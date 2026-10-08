using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CharacterSelectInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public CharacterSelectInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.CharacterSelectScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.hasUICharacterSelected)
				{
					if (CharacterHasEnergy(entity.uICharacterSelected.FirstCharIndex))
					{
						SetCharacter(entity.uICharacterSelected.FirstCharIndex, entity.uICharacterSelected.SecondCharIndex);
					}
					else
					{
						OpenOutOfEnergyPopup(entity.uICharacterSelected.FirstCharIndex, entity.uICharacterSelected.SecondCharIndex);
					}
				}
				else if (entity.hasUICharacterUpgraded)
				{
					UpgradeCharacter(entity.uICharacterUpgraded.CharId);
				}
				else if (entity.hasUINewUnlockedCharacterDisplayed)
				{
					NewCharacterDisplayed(entity.uINewUnlockedCharacterDisplayed.CharacterId);
				}
				else if (entity.hasUIShowCharacterInfo)
				{
					OpenInfoPopup(entity.uIShowCharacterInfo.CharacterId);
				}
				entity.isDestroyed = true;
			}
		}

		private void SetCharacter(int firstChar, int secondChar)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("FirstCharSelectDataSend");
			stateEntity.AddInteger(firstChar);
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isSavedData = true;
			stateEntity2.AddName("SecondCharSelectDataSend");
			stateEntity2.AddInteger(secondChar);
			if (uiContext.uIEntity != null && uiContext.uIEntity.hasHUD && uiContext.uIEntity.hUD != null)
			{
				UIEntity uIEntity = uiContext.CreateEntity();
				uIEntity.AddSetup(uiContext.uIEntity.hUD.hud);
				uIEntity.isUICharSelectedRequest = true;
				uIEntity.isUICharListRequest = true;
			}
			stateContext.CreateEntity().AddPassLevelAction(TutorialOutgameLevel.SelectCharacter);
		}

		private void UpgradeCharacter(int charId)
		{
			CSCharacterData charData = DataProviderHelper.GetCharData(charId);
			int nextUpgradeCostSC = charData.GetNextUpgradeCostSC();
			int nextUpgradeCostParts = charData.GetNextUpgradeCostParts();
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			stateEntity.AddSoftCurrnecyPrice(nextUpgradeCostSC);
			stateEntity.AddCharacterPartsPrice(charId, nextUpgradeCostParts);
			stateEntity.AddBoughtCharacterUpgrade(charId);
			stateEntity.isTransactionBegin = true;
			stateContext.CreateEntity().AddPassLevelAction(TutorialOutgameLevel.UpgradeCharacter);
		}

		private void NewCharacterDisplayed(int charId)
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("NewCharactersUnlocked").SingleEntity();
			NewUnlocksData newUnlocksData = JsonUtility.FromJson<NewUnlocksData>(stateEntity.stringData.Value);
			if (newUnlocksData != null)
			{
				newUnlocksData.UnlockedIds.Remove(charId);
				string newValue = JsonUtility.ToJson(newUnlocksData);
				StateEntity stateEntity2 = stateContext.CreateEntity();
				stateEntity2.isSavedData = true;
				stateEntity2.AddName("NewCharactersUnlockedSend");
				stateEntity2.AddStringData(newValue);
			}
		}

		private bool CharacterHasEnergy(int charId)
		{
			int amount = stateContext.GetEntitiesWithName("CharLifesData" + charId).SingleEntity().integer.Amount;
			return amount > 0;
		}

		private void OpenOutOfEnergyPopup(int firstCharIndex, int secondCharIndex)
		{
			BaseDialog newDialog = uiContext.OpenDialog<CharOutOfEnergyPopup>();
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddSetup(newDialog);
			uIEntity.AddUICharSingleRequest(firstCharIndex);
			uIEntity.isUITotalGemsRequest = true;
			uIEntity.AddUIActionFinishedCallback(delegate
			{
				SetCharacter(firstCharIndex, secondCharIndex);
			});
		}

		private void OpenInfoPopup(int charId)
		{
			BaseDialog newDialog = uiContext.OpenDialog<CharSelectInfoPopup>();
			UIEntity uIEntity = uiContext.CreateEntity();
			uIEntity.AddSetup(newDialog);
			uIEntity.AddUICharSingleRequest(charId);
		}
	}
}
