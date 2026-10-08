using System.Collections.Generic;
using Entitas;
using O7SDK.O7ADSDK;
using Playsoft.MinisRun;
using UnityEngine;

namespace O7SDK
{
	public sealed class O7SdkInterstitialAdSystem : ReactiveSystem<StateEntity>, IInitializeSystem, ITearDownSystem, ISystem
	{
		private readonly StateContext stateContext;

		private readonly UIContext uiContext;

		private float lastRequestTimeScale = 1f;

		public O7SdkInterstitialAdSystem(Contexts contexts)
			: base((IContext<StateEntity>)contexts.state)
		{
			stateContext = contexts.state;
			uiContext = contexts.uI;
		}

		public void Initialize()
		{
			O7ADInterstitialAdManager manager = stateContext.o7SdkEntity.o7SdkInterstitialAdManager.Manager;
			manager.OnInterstitialLoadSuccess += OnInterstitialLoadSuccess;
			manager.OnInterstitialLoadFail += OnInterstitialLoadFail;
			manager.OnInterstitialShowSuccess += OnInterstitialShowSuccess;
			manager.OnInterstitialShowFail += OnInterstitialShowFail;
			manager.OnInterstitialClosed += OnInterstitialClosed;
		}

		public void TearDown()
		{
			O7ADInterstitialAdManager manager = stateContext.o7SdkEntity.o7SdkInterstitialAdManager.Manager;
			manager.OnInterstitialLoadSuccess -= OnInterstitialLoadSuccess;
			manager.OnInterstitialLoadFail -= OnInterstitialLoadFail;
			manager.OnInterstitialShowSuccess -= OnInterstitialShowSuccess;
			manager.OnInterstitialShowFail -= OnInterstitialShowFail;
			manager.OnInterstitialClosed -= OnInterstitialClosed;
		}

		protected override ICollector<StateEntity> GetTrigger(IContext<StateEntity> context)
		{
			return context.CreateCollector(StateMatcher.O7SdkInterstitialAd);
		}

		protected override bool Filter(StateEntity entity)
		{
			return entity.hasO7SdkInterstitialAd && TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.EndTutorial) && !stateContext.o7SdkEntity.isO7SdkPaidUser;
		}

		protected override void Execute(List<StateEntity> entities)
		{
			O7ADInterstitialAdManager manager = stateContext.o7SdkEntity.o7SdkInterstitialAdManager.Manager;
			foreach (StateEntity entity in entities)
			{
				manager.ShowInterstitialAd(entity.o7SdkInterstitialAd.screen1, entity.o7SdkInterstitialAd.screen2);
			}
		}

		private void OnInterstitialLoadSuccess()
		{
		}

		private void OnInterstitialLoadFail(string obj)
		{
		}

		private void OnInterstitialShowSuccess()
		{
			lastRequestTimeScale = Time.timeScale;
		}

		private void OnInterstitialShowFail()
		{
		}

		private void OnInterstitialClosed()
		{
		}
	}
}
