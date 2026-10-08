using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class TutorialTriggerCharacterUpgradePossibleSystem : ReactiveSystem<StateEntity>, ISaveDataLoadedListener
	{
		private readonly StateContext stateContext;

		public TutorialTriggerCharacterUpgradePossibleSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			contexts.uI.CreateEntity().AddSaveDataLoadedListener(this);
		}

		public void OnDataLoaded()
		{
			CheckTrigger();
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionComplete);
		}

		protected override bool Filter(StateEntity entity)
		{
			return stateContext.tutorialEntity.isTutorialInitialized;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			CheckTrigger();
		}

		private void CheckTrigger()
		{
			if (TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.EndTutorial))
			{
				return;
			}
			bool flag = false;
			Dictionary<int, CharactersStaticBalancingData> map = stateContext.balancingDataEntity.characterStaticBalancingData.Map;
			foreach (KeyValuePair<int, CharactersStaticBalancingData> item in map)
			{
				CSCharacterData charData = DataProviderHelper.GetCharData(item.Key);
				int ownedParts = charData.OwnedParts;
				int nextUpgradeCostParts = charData.GetNextUpgradeCostParts();
				flag = charData.IsNextUpgradeAvailable() && ownedParts >= nextUpgradeCostParts;
				if (flag)
				{
					break;
				}
			}
			TriggerUnlockPossible(flag);
		}

		private void TriggerUnlockPossible(bool possible)
		{
			foreach (TutorialTrigger trigger in stateContext.tutorialEntity.tutorialTriggersList.Triggers)
			{
				TutorialCharacterUpgradPossibleTrigger tutorialCharacterUpgradPossibleTrigger = trigger as TutorialCharacterUpgradPossibleTrigger;
				if (tutorialCharacterUpgradPossibleTrigger != null)
				{
					tutorialCharacterUpgradPossibleTrigger.IsTriggered = possible;
					if (!possible)
					{
					}
				}
			}
		}
	}
}
