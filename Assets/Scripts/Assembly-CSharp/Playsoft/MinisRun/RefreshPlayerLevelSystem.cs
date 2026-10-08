using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshPlayerLevelSystem : ReactiveSystem<StateEntity>
	{
		private readonly IGroup<StateEntity> playerLevelListeners;

		private readonly StateContext stateContext;

		public RefreshPlayerLevelSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			playerLevelListeners = stateContext.GetGroup(StateMatcher.PlayerLevelListener);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.PlayerProgressionUpdated.Added(), StateMatcher.PlayerLevelListener.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				RefreshPlayerLevel(entity);
			}
		}

		private void RefreshPlayerLevel(StateEntity e)
		{
			int num = 0;
			int num2 = 0;
			if (e.hasPlayerProgressionUpdated)
			{
				num = e.playerProgressionUpdated.CurrentExperienceAmount;
				num2 = e.playerProgressionUpdated.CurrentLevel;
			}
			else
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerLevelExp").SingleEntity();
				num = ((stateEntity != null) ? stateEntity.integer.Amount : 0);
				StateEntity stateEntity2 = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity();
				num2 = ((stateEntity2 != null) ? stateEntity2.integer.Amount : 0);
			}
			Dictionary<int, PlayerLevelData> map = stateContext.balancingDataEntity.playerLevelBalancingData.Map;
			int currentLeveRequiredExp = ((map.Count > num2) ? map[num2].RequiredExperience : 0);
			int nextLevelRequiredExp = ((map.Count > num2 + 1) ? map[num2 + 1].RequiredExperience : 0);
			StateEntity[] entities = playerLevelListeners.GetEntities();
			foreach (StateEntity stateEntity3 in entities)
			{
				stateEntity3.playerLevelListener.listener.PlayerProgressionChanged(num2, num, currentLeveRequiredExp, nextLevelRequiredExp);
			}
		}
	}
}
