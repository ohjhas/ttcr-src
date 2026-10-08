using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class InitializeLocalUserStateSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private IGroup<StateEntity> userStateDataGroup;

		public InitializeLocalUserStateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			userStateDataGroup = stateContext.GetGroup(StateMatcher.AllOf(StateMatcher.UserStateData, StateMatcher.Name));
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.InitLocalUserState.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				entity.isDestroyed = true;
			}
			UserStateData userStateData = GetUserStateData();
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.AddLocalUserState(userStateData);
		}

		private UserStateData GetUserStateData()
		{
			UserStateData userStateData = new UserStateData();
			List<StateEntity> saveDataEntities = new List<StateEntity>(userStateDataGroup.GetEntities());
			userStateData.FillWithData(saveDataEntities, true);
			return userStateData;
		}
	}
}
