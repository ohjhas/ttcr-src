using System.Collections.Generic;
using DarkTonic.MasterAudio;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ReplaceLocalUserStateSystem : ReactiveSystem<StateEntity>
	{
		private readonly StateContext stateContext;

		private IGroup<StateEntity> userStateDataGroup;

		public ReplaceLocalUserStateSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			userStateDataGroup = stateContext.GetGroup(StateMatcher.AllOf(StateMatcher.UserStateData, StateMatcher.Name));
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.ReplaceUserState.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return stateContext.localUserStateEntity != null;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			for (int i = 0; i < entities.Count; i++)
			{
				UserStateData newState = entities[i].replaceUserState.NewState;
				stateContext.localUserState.StateData = newState;
				entities[i].isDestroyed = true;
			}
			List<StateEntity> saveDataEntities = new List<StateEntity>(userStateDataGroup.GetEntities());
			stateContext.localUserState.StateData.SpreadDataToEntities(saveDataEntities);
			RefreshPlayerLevel();
			RefreshSoundSettings();
		}

		private void RefreshPlayerLevel()
		{
			int amount = stateContext.GetEntitiesWithName("PlayerLevelExp").SingleEntity().integer.Amount;
			int amount2 = stateContext.GetEntitiesWithName("PlayerLevelNumber").SingleEntity().integer.Amount;
			string newValue = JsonUtility.ToJson(new PlayerProgressionUpdateData(amount2, amount));
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("PlayerProgressionUpdate").SingleEntity();
			stateEntity.ReplaceStringData(newValue);
			stateContext.CreateEntity().AddPlayerProgressionUpdated(amount2, amount);
		}

		private void RefreshSoundSettings()
		{
			StateEntity stateEntity = stateContext.GetEntitiesWithName("Music").SingleEntity();
			PersistentAudioSettings.MusicMuted = !stateEntity.boolean.Value;
			StateEntity stateEntity2 = stateContext.GetEntitiesWithName("Sounds").SingleEntity();
			PersistentAudioSettings.MixerMuted = !stateEntity2.boolean.Value;
		}
	}
}
