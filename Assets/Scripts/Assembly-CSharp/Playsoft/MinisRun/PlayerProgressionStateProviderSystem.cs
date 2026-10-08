using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayerProgressionStateProviderSystem : ReactiveSystem<UIEntity>
	{
		private readonly StateContext stateContext;

		public PlayerProgressionStateProviderSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.Setup);
		}

		protected override bool Filter(UIEntity entity)
		{
			return entity.isUIPlayerProgressionStateRequest;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				int amount = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity().integer.Amount;
				int amount2 = stateContext.GetEntitiesWithName("PlayerLevelExp").SingleEntity().integer.Amount;
				Dictionary<int, PlayerLevelData> map = stateContext.balancingDataEntity.playerLevelBalancingData.Map;
				int newCurrentRequiredExp = ((map.Count > amount) ? map[amount].RequiredExperience : 0);
				int newNextRequiredExp = ((map.Count > amount + 1) ? map[amount + 1].RequiredExperience : 0);
				entity.AddUIPlayerProgressionStateResponse(amount, amount2, newCurrentRequiredExp, newNextRequiredExp);
			}
		}
	}
}
