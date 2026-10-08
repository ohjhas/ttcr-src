using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RevivePlayerInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		public RevivePlayerInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.RevivePlayerScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			CreateTransaction();
			CountDoneRevives();
			foreach (UIEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
		}

		private void CreateTransaction()
		{
			RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
			int newAmount = restoreSettingsBalancingData.Map[RestoreSettings.ReviveCost];
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			if (!PlayerRestoreHelper.IsReviveForVideoAvailable())
			{
				stateEntity.AddHardCurrencyPrice(newAmount);
				stateEntity.isTransactionBegin = true;
			}
			else
			{
				stateEntity.isO7SdkRewardAdsShow = true;
				stateEntity.AddO7SdkRewardOfferType(O7SdkRewardOfferType.Continue_Run);
			}
			stateEntity.isBoughtPlayerRevive = true;
		}

		private void CountDoneRevives()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			int num = (playerEntity.hasRevivesDone ? playerEntity.revivesDone.Amount : 0);
			playerEntity.ReplaceRevivesDone(num + 1);
		}
	}
}
