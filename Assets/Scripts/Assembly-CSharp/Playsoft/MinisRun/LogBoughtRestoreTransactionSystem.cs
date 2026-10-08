using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class LogBoughtRestoreTransactionSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private readonly GameContext gameContext;

		public LogBoughtRestoreTransactionSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			gameContext = contexts.game;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.TransactionGranted.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isTransaction && (entity.hasBoughtPlayerResurect || entity.isBoughtPlayerRevive);
		}

		protected override void Execute(List<StateEntity> entities)
		{
			if (!CanExecute())
			{
				return;
			}
			foreach (StateEntity entity in entities)
			{
				if (entity.hasHardCurrencyPrice && entity.hardCurrencyPrice.Amount != 0)
				{
					LogRestoreData(entity.hardCurrencyPrice.Amount);
				}
			}
		}

		private bool CanExecute()
		{
			return gameContext.worldEntity != null && gameContext.worldEntity.hasRunDuration && gameContext.playerEntity != null;
		}

		private void LogRestoreData(int hardCurrencyPrice)
		{
			float duration_MS = gameContext.worldEntity.runDuration.Duration_MS;
			int worldIndex = SettingsService.SettingsSO.WorldIndex;
			GameEntity playerEntity = gameContext.playerEntity;
			int num = (playerEntity.hasResurectionsDone ? playerEntity.resurectionsDone.Amount : 0) + (playerEntity.hasRevivesDone ? playerEntity.revivesDone.Amount : 0);
			StateEntity stateEntity = stateContext.GetEntitiesWithName("HardCurrency").SingleEntity();
			int amount = stateEntity.integer.Amount;
			StateEntity stateEntity2 = stateContext.CreateEntity();
			stateEntity2.isO7SdkCall = true;
			stateEntity2.AddEventCustom("continue-run", "gc", "diamonds", num.ToString(), hardCurrencyPrice, amount, worldIndex.ToString(), duration_MS, null, false);
		}
	}
}
