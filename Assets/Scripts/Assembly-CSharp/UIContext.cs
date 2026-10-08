using System;
using Entitas;

public sealed class UIContext : Context<UIEntity>
{
	public UIEntity o7SdkManualNewsListenerEntity
	{
		get
		{
			return GetGroup(UIMatcher.O7SdkManualNewsListener).GetSingleEntity();
		}
	}

	public O7SdkManualNewsListenerComponent o7SdkManualNewsListener
	{
		get
		{
			return o7SdkManualNewsListenerEntity.o7SdkManualNewsListener;
		}
	}

	public bool hasO7SdkManualNewsListener
	{
		get
		{
			return o7SdkManualNewsListenerEntity != null;
		}
	}

	public UIEntity uIEntity
	{
		get
		{
			return GetGroup(UIMatcher.UI).GetSingleEntity();
		}
	}

	public bool isUI
	{
		get
		{
			return uIEntity != null;
		}
		set
		{
			UIEntity uIEntity = this.uIEntity;
			if (value != (uIEntity != null))
			{
				if (value)
				{
					CreateEntity().isUI = true;
				}
				else
				{
					uIEntity.Destroy();
				}
			}
		}
	}

	public UIContext()
		: base(150, 0, new ContextInfo("UI", UIComponentsLookup.componentNames, UIComponentsLookup.componentTypes), (Func<IEntity, IAERC>)((IEntity entity) => new SafeAERC(entity)))
	{
	}

	public UIEntity SetO7SdkManualNewsListener(IO7SdkManualNewsButtonUpdateListener newListener)
	{
		if (hasO7SdkManualNewsListener)
		{
			throw new EntitasException(string.Concat("Could not set O7SdkManualNewsListener!\n", this, " already has an entity with O7SdkManualNewsListenerComponent!"), "You should check if the context already has a o7SdkManualNewsListenerEntity before setting it or use context.ReplaceO7SdkManualNewsListener().");
		}
		UIEntity uIEntity = CreateEntity();
		uIEntity.AddO7SdkManualNewsListener(newListener);
		return uIEntity;
	}

	public void ReplaceO7SdkManualNewsListener(IO7SdkManualNewsButtonUpdateListener newListener)
	{
		UIEntity uIEntity = o7SdkManualNewsListenerEntity;
		if (uIEntity == null)
		{
			uIEntity = SetO7SdkManualNewsListener(newListener);
		}
		else
		{
			uIEntity.ReplaceO7SdkManualNewsListener(newListener);
		}
	}

	public void RemoveO7SdkManualNewsListener()
	{
		o7SdkManualNewsListenerEntity.Destroy();
	}
}
