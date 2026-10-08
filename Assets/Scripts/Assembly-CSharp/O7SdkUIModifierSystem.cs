using System;
using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

public class O7SdkUIModifierSystem : ReactiveSystem<UIEntity>
{
	private readonly StateContext stateContext;

	public O7SdkUIModifierSystem(Contexts context)
		: base((IContext<UIEntity>)context.uI)
	{
		stateContext = context.state;
	}

	protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
	{
		return context.CreateCollector(UIMatcher.O7SdkDataListener.Added());
	}

	protected override bool Filter(UIEntity entity)
	{
		return true;
	}

	protected override void Execute(List<UIEntity> entities)
	{
		foreach (UIEntity entity in entities)
		{
			O7ADVideoGalleryAdManager manager = stateContext.o7SdkEntity.o7SdkVideoGalleryManager.Manager;
			bool canShow = false;
			try
			{
				canShow = manager.IsVideoGalleryAvailable();
			}
			catch (Exception ex)
			{
				Debug.LogError("[O7SDK - video gallery]" + ex);
			}
			entity.o7SdkDataListener.listener.UpdateVideoGalleryButton(canShow);
			entity.RemoveO7SdkDataListener();
			entity.isDestroyed = true;
		}
	}
}
