using System;
using System.Collections.Generic;
using Entitas;
using O7SDK.O7ADSDK;
using Playsoft.MinisRun;

public sealed class O7SdkBannerAdSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ITearDownSystem, ISystem
{
	private O7ADBannerAdManager bannerAdManager;

	private readonly StateContext stateContext;

	private readonly UIContext uiContext;

	private bool isBannerLoaded;

	private bool isBannerPositioned;

	public O7SdkBannerAdSystem(Contexts contexts)
		: base((IContext<StateEntity>)contexts.state)
	{
		stateContext = contexts.state;
		uiContext = contexts.uI;
	}

	public void Initialize()
	{
		bannerAdManager = stateContext.o7SdkEntity.o7SdkBannerAdManager.Manager;
		O7ADBannerAdManager o7ADBannerAdManager = bannerAdManager;
		o7ADBannerAdManager.OnBannerLoadSuccess = (Action)Delegate.Combine(o7ADBannerAdManager.OnBannerLoadSuccess, new Action(OnBannerLoadSuccess));
		O7ADBannerAdManager o7ADBannerAdManager2 = bannerAdManager;
		o7ADBannerAdManager2.OnBannerLoadFail = (Action<string>)Delegate.Combine(o7ADBannerAdManager2.OnBannerLoadFail, new Action<string>(OnBannerLoadFail));
		O7ADBannerAdManager o7ADBannerAdManager3 = bannerAdManager;
		o7ADBannerAdManager3.OnBannerShowSuccess = (Action)Delegate.Combine(o7ADBannerAdManager3.OnBannerShowSuccess, new Action(OnBannerShowSuccess));
		O7ADBannerAdManager o7ADBannerAdManager4 = bannerAdManager;
		o7ADBannerAdManager4.OnBannerShowFail = (Action)Delegate.Combine(o7ADBannerAdManager4.OnBannerShowFail, new Action(OnBannerShowFail));
		O7ADBannerAdManager o7ADBannerAdManager5 = bannerAdManager;
		o7ADBannerAdManager5.OnBannerClosed = (Action)Delegate.Combine(o7ADBannerAdManager5.OnBannerClosed, new Action(OnBannerClosed));
	}

	public void TearDown()
	{
		bannerAdManager = stateContext.o7SdkEntity.o7SdkBannerAdManager.Manager;
		bannerAdManager.OnBannerLoadSuccess = null;
		bannerAdManager.OnBannerLoadFail = null;
		bannerAdManager.OnBannerShowSuccess = null;
		bannerAdManager.OnBannerShowFail = null;
		bannerAdManager.OnBannerClosed = null;
	}

	protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
	{
		return context.CreateCollector(StateMatcher.O7SdkBannerAdShow.Added(), StateMatcher.O7SdkBannerAdHide.Added());
	}

	protected override bool Filter(StateEntity entity)
	{
		return (entity.isO7SdkBannerAdHide || entity.isO7SdkBannerAdShow) && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.EndTutorial) && !stateContext.o7SdkEntity.isO7SdkPaidUser;
	}

	protected override void Execute(List<StateEntity> entities)
	{
		foreach (StateEntity entity in entities)
		{
			if (entity.isO7SdkBannerAdHide)
			{
				bannerAdManager.HideBanner();
			}
			if (entity.isO7SdkBannerAdShow)
			{
				bannerAdManager.ShowBanner();
			}
		}
	}

	private void SetBannerPositionIfNeeded()
	{
		if (stateContext.o7SdkEntity.isO7SdkReadyToLoadGame && !isBannerPositioned)
		{
			isBannerPositioned = true;
			stateContext.o7SdkEntity.o7SdkBannerAdManager.Manager.SetBannerBottomPosition(false);
		}
	}

	private void OnBannerLoadSuccess()
	{
		isBannerLoaded = true;
	}

	private void OnBannerLoadFail(string obj)
	{
		isBannerLoaded = false;
	}

	private void OnBannerClosed()
	{
		isBannerLoaded = false;
	}

	private void OnBannerShowSuccess()
	{
		isBannerLoaded = false;
	}

	private void OnBannerShowFail()
	{
		isBannerLoaded = false;
	}
}
