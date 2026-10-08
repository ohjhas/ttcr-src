using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CharacterLifeRefillUpdateSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISaveDataLoadedListener, ISystem
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		private static bool isDataLoaded;

		public CharacterLifeRefillUpdateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		public void Initialize()
		{
			uiContext.CreateEntity().AddSaveDataLoadedListener(this);
		}

		public void OnDataLoaded()
		{
			isDataLoaded = true;
			if (stateContext.currentRealTimeEntity != null)
			{
				UpdateLifesIfNeeded();
			}
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.CurrentRealTime.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return true;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			if (isDataLoaded)
			{
				UpdateLifesIfNeeded();
			}
		}

		private void UpdateLifesIfNeeded()
		{
			DateTime currentTime = stateContext.currentRealTimeEntity.currentRealTime.CurrentTime;
			Dictionary<int, CharactersStaticBalancingData>.Enumerator enumerator = stateContext.balancingDataEntity.characterStaticBalancingData.Map.GetEnumerator();
			while (enumerator.MoveNext())
			{
				List<CharactersUpgradesBalancingData> list = stateContext.balancingDataEntity.characterUpgradesBalancingData.Map[enumerator.Current.Key];
				StateEntity stateEntity = stateContext.GetEntitiesWithName("CharLifesRefillData" + enumerator.Current.Key).SingleEntity();
				StateEntity stateEntity2 = stateContext.GetEntitiesWithName("CharLifesData" + enumerator.Current.Key).SingleEntity();
				StateEntity upgradeData = stateContext.GetEntitiesWithName("CharUpgradeData" + enumerator.Current.Key).SingleEntity();
				CharactersUpgradesBalancingData charactersUpgradesBalancingData = list.Find((CharactersUpgradesBalancingData up) => up.Level == upgradeData.integer.Amount);
				if (stateEntity2.integer.Amount >= charactersUpgradesBalancingData.Lifes)
				{
					continue;
				}
				DateTime value = stateEntity.dateTime.Value;
				TimeSpan timeSpan = currentTime - value;
				if (timeSpan.TotalSeconds > (double)enumerator.Current.Value.Refilldurations)
				{
					int num = Mathf.Abs((int)timeSpan.TotalSeconds / enumerator.Current.Value.Refilldurations);
					if (RefillLives(stateEntity2, num, charactersUpgradesBalancingData.Lifes))
					{
						ResetTimer(stateEntity, currentTime);
					}
					else
					{
						UpdateTimer(stateEntity, num * enumerator.Current.Value.Refilldurations);
					}
				}
			}
			enumerator.Dispose();
		}

		private bool RefillLives(StateEntity currentLifesData, int lifesToRefill, int maxLifes)
		{
			int num = Mathf.Clamp(currentLifesData.integer.Amount + lifesToRefill, 0, maxLifes);
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("CharLifesDataSend");
			stateEntity.AddLabel(currentLifesData.name.Name);
			stateEntity.AddInteger(num);
			return num == maxLifes;
		}

		private void UpdateTimer(StateEntity lastRefillEntity, int timeUpdate)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("CharLifesRefillDataSend");
			stateEntity.AddLabel(lastRefillEntity.name.Name);
			stateEntity.AddDateTime(lastRefillEntity.dateTime.Value.AddSeconds(timeUpdate));
		}

		private void ResetTimer(StateEntity lastRefillEntity, DateTime currentTime)
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isSavedData = true;
			stateEntity.AddName("CharLifesRefillDataSend");
			stateEntity.AddLabel(lastRefillEntity.name.Name);
			stateEntity.AddDateTime(currentTime);
		}
	}
}
