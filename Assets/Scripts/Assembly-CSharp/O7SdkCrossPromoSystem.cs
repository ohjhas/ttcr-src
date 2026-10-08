using System.Collections.Generic;
using Entitas;
using O7SDK;
using UnityEngine;

public class O7SdkCrossPromoSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ITearDownSystem, ISystem
{
	private O7SDKCrossPromoManager crossPromoManager;

	private readonly StateContext stateContext;

	private readonly UIContext uiContext;

	public O7SdkCrossPromoSystem(Contexts context)
		: base((IContext<StateEntity>)context.state)
	{
		stateContext = context.state;
		uiContext = context.uI;
	}

	public void Initialize()
	{
		crossPromoManager = stateContext.o7SdkEntity.o7SdkCrossPromoManager.Manager;
		crossPromoManager.OnAutoNewsClosed += OnAutoNewsClosed;
		crossPromoManager.OnManualNewsClosed += OnManualNewsClosed;
		crossPromoManager.OnAutoNewsReady += OnAutoNewsReady;
		crossPromoManager.OnManualNewsReady += OnManualNewsReady;
	}

	public void TearDown()
	{
		crossPromoManager = stateContext.o7SdkEntity.o7SdkCrossPromoManager.Manager;
		crossPromoManager.OnAutoNewsClosed -= OnAutoNewsClosed;
		crossPromoManager.OnManualNewsClosed -= OnManualNewsClosed;
		crossPromoManager.OnAutoNewsReady -= OnAutoNewsReady;
		crossPromoManager.OnManualNewsReady -= OnManualNewsReady;
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.O7SdkCrossPromo);
	}

	protected override bool Filter(StateEntity entity)
	{
		return entity.isO7SdkCall;
	}

	protected override void Execute(List<StateEntity> entities)
	{
		foreach (StateEntity entity in entities)
		{
			if (Application.internetReachability == NetworkReachability.NotReachable)
			{
				ShowErrorPopUp(ExplanationType.NoInternet);
				entity.isDestroyed = true;
				break;
			}
			if (entity.hasO7SdkCrossPromo)
			{
				if (entity.o7SdkCrossPromo.isAutoNews && crossPromoManager.IsAutoNewsReady())
				{
					crossPromoManager.ShowAutoNews();
				}
				if (entity.o7SdkCrossPromo.isManualNews && crossPromoManager.IsManualNewsReady())
				{
					crossPromoManager.ShowManualNews();
				}
			}
		}
	}

	private void OnManualNewsReady(bool isReady)
	{
		SetGridButtonImage(isReady);
	}

	private void OnAutoNewsReady(bool isReady)
	{
		stateContext.o7SdkEntity.isO7SdkAutoNewsRequest = true;
	}

	private void OnManualNewsClosed()
	{
		Debug.Log("O7SdkCrossPromoSystem-->OnManualNewsClosed()");
		UpdateGridButtonImage();
	}

	private void OnAutoNewsClosed()
	{
	}

	private void UpdateGridButtonImage()
	{
		UIEntity uIEntity = uiContext.CreateEntity();
		uIEntity.isO7SdkGridButtonUpdateRequest = true;
	}

	private void SetGridButtonImage(bool isReady)
	{
		string manaulNewsButtonPath = crossPromoManager.GetManaulNewsButtonPath();
		stateContext.o7SdkEntity.ReplaceO7SdkData(isReady, manaulNewsButtonPath);
	}

	private void ShowErrorPopUp(ExplanationType type)
	{
		UIEntity uIEntity = uiContext.CreateEntity();
		uIEntity.isUIAction = true;
		uIEntity.AddUIDisplayExplanation(type);
	}
}
