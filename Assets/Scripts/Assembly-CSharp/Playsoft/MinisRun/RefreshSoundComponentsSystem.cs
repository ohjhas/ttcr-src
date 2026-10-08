using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshSoundComponentsSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private IGroup<UIEntity> _soundsListeners;

		private StateContext _stateContext;

		private UIContext _uiContext;

		public RefreshSoundComponentsSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			_uiContext = contexts.uI;
			_stateContext = contexts.state;
			_soundsListeners = _uiContext.GetGroup(UIMatcher.ToggleSoundsListener);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.Boolean);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasName && entity.name.Name == "Sounds";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			InformListeners(entities);
		}

		public void Initialize()
		{
			HashSet<StateEntity> entitiesWithName = _stateContext.GetEntitiesWithName("Sounds");
			InformListeners(entitiesWithName.ToList());
		}

		private void InformListeners(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				UIEntity[] entities2 = _soundsListeners.GetEntities();
				foreach (UIEntity uIEntity in entities2)
				{
					uIEntity.toggleSoundsListener.listener.SetState(entity.boolean.Value);
				}
			}
		}
	}
}
