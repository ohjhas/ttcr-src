using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class InitializePlayerLevelSystem : IInitializeSystem, ISaveDataLoadedListener, ISystem
	{
		private StateContext stateContext;

		private UIContext uiContext;

		public InitializePlayerLevelSystem(Contexts contexts)
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
			int amount = stateContext.GetEntitiesWithName("PlayerLevelExp").SingleEntity().integer.Amount;
			int amount2 = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity().integer.Amount;
			string newValue = JsonUtility.ToJson(new PlayerProgressionUpdateData(amount2, amount));
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("PlayerProgressionUpdate").SingleEntity();
			stateEntity.ReplaceStringData(newValue);
			stateContext.CreateEntity().AddPlayerProgressionUpdated(amount2, amount);
		}
	}
}
