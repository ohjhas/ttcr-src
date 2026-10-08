using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CharactersUpgradeSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string upgradeSendKey = "CharUpgradeDataSend";

		public const string upgradeKey = "CharUpgradeData";

		public CharactersUpgradeSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				stateContext.CreateIntegerData("CharUpgradeData" + enumerator.Current.Key, 1, true);
			}
			enumerator.Dispose();
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.hasLabel && entity.name.Name == "CharUpgradeDataSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName(entity.label.Label).SingleEntity();
				stateEntity.ReplaceInteger(entity.integer.Amount);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
