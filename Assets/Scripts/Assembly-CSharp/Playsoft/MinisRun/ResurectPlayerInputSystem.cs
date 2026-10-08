using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class ResurectPlayerInputSystem : ReactiveSystem<UIEntity>
	{
		private readonly GameContext gameContext;

		private readonly StateContext stateContext;

		public ResurectPlayerInputSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			gameContext = contexts.game;
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.ResurectPlayerScreen);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			CreateTransaction();
			CountDoneResurections();
			foreach (UIEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
		}

		private void CreateTransaction()
		{
			RestoreSettingsBalancingDataComponent restoreSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.restoreSettingsBalancingData;
			int newAmount = restoreSettingsBalancingData.Map[RestoreSettings.ResurectCost];
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isTransaction = true;
			if (!PlayerRestoreHelper.IsResurectForVideoAvailable())
			{
				stateEntity.AddHardCurrencyPrice(newAmount);
				stateEntity.isTransactionBegin = true;
			}
			else
			{
				stateEntity.AddHardCurrencyPrice(0);
				stateEntity.isO7SdkRewardAdsShow = true;
				stateEntity.AddO7SdkRewardOfferType(O7SdkRewardOfferType.Continue_Run);
			}
			stateEntity.AddBoughtPlayerResurect(0f);
		}

		private void CountDoneResurections()
		{
			GameEntity playerEntity = gameContext.playerEntity;
			int num = (playerEntity.hasResurectionsDone ? playerEntity.resurectionsDone.Amount : 0);
			playerEntity.ReplaceResurectionsDone(num + 1);
		}
	}
}
