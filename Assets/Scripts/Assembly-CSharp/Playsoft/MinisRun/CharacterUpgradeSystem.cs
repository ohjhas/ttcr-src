using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CharacterUpgradeSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		public CharacterUpgradeSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.UpgradeCharacter);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasUpgradeCharacter;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				string name = "CharUpgradeData" + entity.upgradeCharacter.CharacterId;
				StateEntity stateEntity = stateContext.GetEntitiesWithName(name).SingleEntity();
				StateEntity stateEntity2 = stateContext.CreateEntity();
				stateEntity2.isSavedData = true;
				stateEntity2.AddLabel("CharUpgradeData" + entity.upgradeCharacter.CharacterId);
				stateEntity2.AddName("CharUpgradeDataSend");
				stateEntity2.AddInteger(stateEntity.integer.Amount + 1);
			}
		}
	}
}
