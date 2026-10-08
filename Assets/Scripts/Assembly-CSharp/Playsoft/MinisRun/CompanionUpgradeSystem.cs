using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CompanionUpgradeSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public CompanionUpgradeSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.UpgradeCompanion);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasUpgradeCompanion;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				string name = "CompUpgradeData" + entity.upgradeCompanion.CompanionId;
				StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
				StateEntity stateEntity2 = stateContext.CreateEntity();
				stateEntity2.isSavedData = true;
				stateEntity2.AddLabel("CompUpgradeData" + entity.upgradeCompanion.CompanionId);
				stateEntity2.AddName("CompUpgradeDataSend");
				stateEntity2.AddInteger(stateEntity.integer.Amount + 1);
			}
		}
	}
}
