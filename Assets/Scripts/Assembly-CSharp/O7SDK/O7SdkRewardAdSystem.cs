using System.Collections.Generic;
using Entitas;
using O7SDK.O7ADSDK;

namespace O7SDK
{
	public sealed class O7SdkRewardAdSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ITearDownSystem, ISystem
	{
		private O7ADRewardAdManager o7AdRewardAdManager;

		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		public O7SdkRewardAdSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		public void Initialize()
		{
			o7AdRewardAdManager = stateContext.o7SdkEntity.o7SdkRewardedAdManager.Manager;
			o7AdRewardAdManager.OnRewardAdClosed += OnRewardAdClosed;
			o7AdRewardAdManager.OnRewardRecieved += OnRewardReceived;
			o7AdRewardAdManager.OnRewardAdLoadSuccess += OnRewardAdLoadSuccess;
			o7AdRewardAdManager.OnRewardAdLoadFail += OnRewardAdLoadFail;
			o7AdRewardAdManager.OnRewardAdShowSuccess += OnRewardAdShowSuccess;
			o7AdRewardAdManager.OnRewardAdShowFail += OnRewardAdShowFail;
			if (stateContext.o7SdkEntity.isO7SdkReadyToLoadGame && !stateContext.o7SdkEntity.isO7SdkRewardAdsFetchRequested && !stateContext.o7SdkEntity.isO7SdkRewardAdFetched)
			{
				stateContext.o7SdkEntity.isO7SdkRewardAdsFetchRequested = true;
				o7AdRewardAdManager.FetchRewardsAd();
			}
		}

		public void TearDown()
		{
			o7AdRewardAdManager = stateContext.o7SdkEntity.o7SdkRewardedAdManager.Manager;
			o7AdRewardAdManager.OnRewardAdClosed -= OnRewardAdClosed;
			o7AdRewardAdManager.OnRewardRecieved -= OnRewardReceived;
			o7AdRewardAdManager.OnRewardAdLoadSuccess -= OnRewardAdLoadSuccess;
			o7AdRewardAdManager.OnRewardAdLoadFail -= OnRewardAdLoadFail;
			o7AdRewardAdManager.OnRewardAdShowSuccess -= OnRewardAdShowSuccess;
			o7AdRewardAdManager.OnRewardAdShowFail -= OnRewardAdShowFail;
		}

		private void OnRewardAdLoadSuccess()
		{
			stateContext.o7SdkEntity.isO7SdkRewardAdFetched = true;
		}

		private void OnRewardAdLoadFail(string error)
		{
		}

		private void OnRewardAdShowSuccess()
		{
		}

		private void OnRewardAdShowFail()
		{
			ReFetchVideo();
			FinishTransaction();
		}

		private void OnRewardAdClosed()
		{
			ReFetchVideo();
			FinishTransaction();
		}

		private void OnRewardReceived(string providerID)
		{
			stateContext.o7SdkEntity.ReplaceO7SdkRewardAwarded(providerID);
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkRewardAdsShow.Added());
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.isO7SdkRewardAdsShow;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			o7AdRewardAdManager.ShowRewardsAd();
		}

		private void FinishTransaction()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkRewardTransaction = true;
		}

		private void ReFetchVideo()
		{
			stateContext.o7SdkEntity.isO7SdkRewardAdFetched = false;
			o7AdRewardAdManager.FetchRewardsAd();
		}
	}
}
