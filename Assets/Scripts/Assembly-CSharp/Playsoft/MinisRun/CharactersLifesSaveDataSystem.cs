using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CharactersLifesSaveDataSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISaveDataLoadedListener, ISystem
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public const string lifesSendKey = "CharLifesDataSend";

		public const string lifesKey = "CharLifesData";

		public CharactersLifesSaveDataSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		public void Initialize()
		{
			Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				stateContext.CreateIntegerData("CharLifesData" + enumerator.Current.Key, -1, true);
			}
			enumerator.Dispose();
			uiContext.CreateEntity().AddSaveDataLoadedListener(this);
		}

		public void OnDataLoaded()
		{
			Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName("CharLifesData" + enumerator.Current.Key).SingleEntity();
				if (stateEntity.integer.Amount < 0)
				{
					List<CharactersUpgradesBalancingData> list = stateContext.balancingDataEntity.characterUpgradesBalancingData.Map[enumerator.Current.Key];
					StateEntity upgradeData = stateContext.GetEntitiesWithName("CharUpgradeData" + enumerator.Current.Key).SingleEntity();
					CharactersUpgradesBalancingData charactersUpgradesBalancingData = list.Find((CharactersUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount);
					StateEntity stateEntity2 = stateContext.CreateEntity();
					stateEntity2.isSavedData = true;
					stateEntity2.AddName("CharLifesDataSend");
					stateEntity2.AddLabel(stateEntity.name.Name);
					stateEntity2.AddInteger(charactersUpgradesBalancingData.Lifes);
				}
			}
			enumerator.Dispose();
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.SavedData);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isSavedData && entity.hasLabel && entity.name.Name == "CharLifesDataSend";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				StateEntity stateEntity = stateContext.GetEntitiesWithName(entity.label.Label).SingleEntity();
				stateEntity.ReplaceInteger(Mathf.Max(0, entity.integer.Amount));
				stateEntity.isToSave = true;
				entity.isDestroyed = true;
			}
		}
	}
}
