using System.Collections.Generic;
using Entitas;
using UnityEngine.SceneManagement;

namespace Playsoft.MinisRun
{
	public class UIChangeSceneSystem : ReactiveSystem<UIEntity>
	{
		private readonly UIContext uiContext;

		private readonly StateContext stateContext;

		private IGroup<UIEntity> changeSceneListeners;

		public UIChangeSceneSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			uiContext = contexts.uI;
			stateContext = contexts.state;
			changeSceneListeners = uiContext.GetGroup(UIMatcher.SceneChangeRequestListener);
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.ChangeScene);
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			string name = entities[0].changeScene.Name;
			bool initialLoading = entities[0].changeScene.InitialLoading;
			StateEntity stateEntity = stateContext.GetEntitiesWithName("LastScene").SingleEntity();
			stateEntity.ReplaceStringData(SceneManager.GetActiveScene().name);
			if (uiContext.uIEntity != null)
			{
				uiContext.uIEntity.isNewSceneLoaded = false;
			}
			UIEntity[] entities2 = changeSceneListeners.GetEntities();
			foreach (UIEntity uIEntity in entities2)
			{
				uIEntity.sceneChangeRequestListener.listener.OnSceneChange(name, initialLoading);
			}
		}
	}
}
