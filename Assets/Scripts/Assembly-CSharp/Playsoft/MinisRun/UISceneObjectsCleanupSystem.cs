using System;
using System.Collections.Generic;
using System.Linq;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class UISceneObjectsCleanupSystem : ReactiveSystem<UIEntity>
	{
		public readonly StateContext stateContext;

		public readonly UIContext uiContext;

		public UISceneObjectsCleanupSystem(Contexts contexts)
			: base((IContext<UIEntity>)contexts.uI)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
		{
			return context.CreateCollector(UIMatcher.ChangeScene.Added(), UIMatcher.IntroDone.Added());
		}

		protected override bool Filter(UIEntity entity)
		{
			return true;
		}

		protected override void Execute(List<UIEntity> entities)
		{
			foreach (UIEntity entity in entities)
			{
				if (entity.isIntroDone)
				{
					entity.isDestroyed = true;
				}
			}
			if (uiContext == null || uiContext.uIEntity == null || !uiContext.uIEntity.hasScreenMap || uiContext.uIEntity.screenMap.ScreenMap == null)
			{
				return;
			}
			Dictionary<Type, GameObject> screenMap = uiContext.uIEntity.screenMap.ScreenMap;
			for (int i = 0; i < screenMap.Keys.Count; i++)
			{
				KeyValuePair<Type, GameObject> keyValuePair = screenMap.ElementAt(i);
				if (keyValuePair.Value == null)
				{
					screenMap.Remove(keyValuePair.Key);
				}
			}
		}
	}
}
