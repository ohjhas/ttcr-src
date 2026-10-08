using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

public class O7SdkVideoGallerySystem : ReactiveSystem<StateEntity>, IInitializeSystem, ISystem
{
	private O7ADVideoGalleryAdManager videoGalleryManager;

	private StateContext stateContext
	{
		get
		{
			return Contexts.sharedInstance.state;
		}
	}

	public O7SdkVideoGallerySystem(Contexts context)
		: base((IContext<StateEntity>)context.state)
	{
	}

	public void Initialize()
	{
		videoGalleryManager = stateContext.o7SdkEntity.o7SdkVideoGalleryManager.Manager;
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.O7SdkVideoGallery.Added());
	}

	protected override bool Filter(StateEntity entity)
	{
		return true;
	}

	protected override void Execute(List<StateEntity> entities)
	{
		if (Application.internetReachability == NetworkReachability.NotReachable)
		{
			ShowErrorPopUp(ExplanationType.NoInternet);
		}
		else if (videoGalleryManager.IsVideoGalleryAvailable())
		{
			videoGalleryManager.ShowVideoGallery(string.Empty);
		}
		else if (O7SDKBase.O7SDK_LOG)
		{
			Debug.Log("Video Gallery is not available");
		}
	}

	private void ShowErrorPopUp(ExplanationType type)
	{
		UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
		uIEntity.isUIAction = true;
		uIEntity.AddUIDisplayExplanation(type);
	}
}
