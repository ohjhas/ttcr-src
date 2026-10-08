using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class O7SdkDataSystem : ReactiveSystem<UIEntity>
{
	private readonly UIContext uiContext;

	private StateContext stateContext
	{
		get
		{
			return Contexts.sharedInstance.state;
		}
	}

	public O7SdkDataSystem(Contexts context)
		: base((IContext<UIEntity>)context.uI)
	{
		uiContext = context.uI;
	}

	protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
	{
		return context.CreateCollector(UIMatcher.O7SdkManualNewsListener.Added(), UIMatcher.O7SdkGridButtonUpdateRequest.Added());
	}

	protected override bool Filter(UIEntity entity)
	{
		return true;
	}

	protected override void Execute(List<UIEntity> entities)
	{
		foreach (UIEntity entity in entities)
		{
			UIEntity o7SdkManualNewsListenerEntity = uiContext.o7SdkManualNewsListenerEntity;
			Debug.Log(string.Format("O7SdkDataSystem {0}", o7SdkManualNewsListenerEntity));
			if (o7SdkManualNewsListenerEntity == null)
			{
				break;
			}
			Debug.Log(string.Format("O7SdkManualNewsListener {0}", o7SdkManualNewsListenerEntity.hasO7SdkManualNewsListener));
			if (o7SdkManualNewsListenerEntity.hasO7SdkManualNewsListener)
			{
				o7SdkManualNewsListenerEntity.o7SdkManualNewsListener.listener.UpdateGridButton(stateContext.o7SdkEntity.o7SdkData.manualNewsReady, stateContext.o7SdkEntity.o7SdkData.pathToManualNewsButton);
			}
			if (entity.isO7SdkGridButtonUpdateRequest)
			{
				entity.isDestroyed = true;
			}
		}
	}
}
