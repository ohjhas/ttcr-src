using System;
using System.Collections.Generic;
using Entitas;

namespace Playsoft.MinisRun
{
	public class CharactersLifeRefillSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private readonly StateContext stateContext;

		public const string lastLifeRefillTimeSendKey = "CharLifesRefillDataSend";

		public const string lastLifeRefillTimeKey = "CharLifesRefillData";

		public CharactersLifeRefillSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
		}

		public void Initialize()
		{
			Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				stateContext.CreateDateTimeData("CharLifesRefillData" + enumerator.Current.Key, DateTime.Now, true);
			}
			enumerator.Dispose();
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.hasLabel && entity.name.Name == "CharLifesRefillDataSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName(entity.label.Label).SingleEntity();
				stateEntity.ReplaceDateTime(entity.dateTime.Value);
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
