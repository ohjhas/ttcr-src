using System.Collections.Generic;
using System.Linq;
using Entitas;

namespace Playsoft.MinisRun
{
	public class RefreshMusicComponentsSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
	{
		private IGroup<UIEntity> _musicListeners;

		private StateContext _stateContext;

		private UIContext _uiContext;

		public RefreshMusicComponentsSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			_uiContext = contexts.uI;
			_stateContext = contexts.state;
			_musicListeners = _uiContext.GetGroup(UIMatcher.ToggleMusicsListener);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.Boolean);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasName && entity.name.Name == "Music";
		}

		protected override void Execute(List<StateEntity> entities)
		{
			InformListeners(entities);
		}

		public void Initialize()
		{
			HashSet<StateEntity> entitiesWithName = _stateContext.GetEntitiesWithName("Music");
			InformListeners(entitiesWithName.ToList());
		}

		private void InformListeners(List<StateEntity> entities)
		{
			foreach (StateEntity entity in entities)
			{
				UIEntity[] entities2 = _musicListeners.GetEntities();
				foreach (UIEntity uIEntity in entities2)
				{
					uIEntity.toggleMusicsListener.listener.SetState(entity.boolean.Value);
				}
			}
		}
	}
}
