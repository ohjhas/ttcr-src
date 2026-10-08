using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class GrantPlayerExperienceSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public GrantPlayerExperienceSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.GrantPlayerExperience);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasGrantPlayerExperience;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("PlayerLevelExp").SingleEntity();
				int num = stateEntity.integer.Amount + entity.grantPlayerExperience.ExperienceAmount;
				StateEntity stateEntity2 = stateContext.CreateEntity();
				stateEntity2.isSavedData = true;
				stateEntity2.AddLabel("PlayerLevelExp");
				stateEntity2.AddName("PlayerLevelExpSend");
				stateEntity2.AddInteger(num);
				entity.AddPlayerExperienceGranted(num);
			}
		}
	}
}
