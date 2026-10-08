using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class PlayerProgressionStateSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public PlayerProgressionStateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.PlayerExperienceGranted);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasPlayerExperienceGranted;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				int currentExperienceAmount = entity.playerExperienceGranted.CurrentExperienceAmount;
				int amount = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity().integer.Amount;
				Dictionary<int, PlayerLevelData> map = stateContext.balancingDataEntity.playerLevelBalancingData.Map;
				if (map.Count <= amount)
				{
					break;
				}
				int num = amount;
				for (int i = amount + 1; i < map.Count; i++)
				{
					if (map[i].RequiredExperience <= currentExperienceAmount)
					{
						num = i;
						stateContext.CreateEntity().AddPlayerProgressionUpdated(num, currentExperienceAmount);
					}
				}
				if (num > amount)
				{
					StateEntity stateEntity = stateContext.CreateEntity();
					stateEntity.isSavedData = true;
					stateEntity.AddLabel("PlayerLevelNumber");
					stateEntity.AddName("PlayerLevelNumberSend");
					stateEntity.AddInteger(num);
				}
				else
				{
					stateContext.CreateEntity().AddPlayerProgressionUpdated(num, currentExperienceAmount);
				}
			}
		}
	}
}
