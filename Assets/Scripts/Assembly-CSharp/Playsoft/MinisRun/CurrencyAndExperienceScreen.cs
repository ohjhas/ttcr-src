using System.Collections.Generic;
using DG.Tweening;
using Entitas;
using UnityEngine;
using UnityEngine.Events;

namespace Playsoft.MinisRun
{
	public class CurrencyAndExperienceScreen : BaseScreen
	{
		public enum Type
		{
			Experience = 0,
			Coins = 1,
			Diamonds = 2
		}

		private const string SOFT_GRANTED_SOUND_ID = "coins_received";

		private const string HARD_GRANTED_SOUND_ID = "diamonds_received";

		[Space(5f)]
		[Header("CurrencyUpdateScreen")]
		[SerializeField]
		private RectTransform _barHolder;

		[SerializeField]
		private ExperienceBar _experienceBar;

		[SerializeField]
		private CurrencyCounter _coinsCounter;

		[SerializeField]
		private CurrencyCounter _hcCounter;

		[Space(5f)]
		[Header("Prefabs")]
		[SerializeField]
		private GameObject _starPrefab;

		[SerializeField]
		private GameObject _coinPrefab;

		[SerializeField]
		private GameObject _diamondPrefab;

		[Space(5f)]
		[Header("Parameters")]
		[SerializeField]
		private float _showBarOffset;

		[SerializeField]
		private float _showBarDuration;

		[SerializeField]
		private float _flyingElementsDuration;

		[SerializeField]
		private int _flyingTokensAmount;

		private Vector2 _barOrigin;

		private Tweener _showBarTween;

		private Tweener _hideBarTween;

		private Vector2 _initialAnchorHardCurrencyCounter;

		private Tweener _showHardCurrencyCounterTween;

		public bool IsPlayingExperienceAnimations { get; private set; }

		public bool IsPlayingCurrencyAnimations { get; private set; }

		protected override void Setup()
		{
			base.Setup();
			_barOrigin = _barHolder.anchoredPosition;
			_barHolder.anchoredPosition += new Vector2(0f, _showBarOffset);
		}

		public void SetData()
		{
			StateContext state = Contexts.sharedInstance.state;
			StateEntity stateEntity = state.GetEntitiesWithName("Coins").SingleEntity();
			StateEntity stateEntity2 = state.GetEntitiesWithName("HardCurrency").SingleEntity();
			Dictionary<int, PlayerLevelData> map = state.balancingDataEntity.playerLevelBalancingData.Map;
			PlayerProgressionUpdateData progressUpdateData = GetProgressUpdateData();
			int requiredExperience = map[progressUpdateData.LastVisbileLevel].RequiredExperience;
			int requiredExperience2 = map[progressUpdateData.LastVisbileLevel + 1].RequiredExperience;
			int lastVisibleExp = progressUpdateData.LastVisibleExp;
			int lastVisbileLevel = progressUpdateData.LastVisbileLevel;
			_coinsCounter.CounterEffect.Reset(stateEntity.integer.Amount);
			_hcCounter.CounterEffect.Reset(stateEntity2.integer.Amount);
			_experienceBar.SetBarAndText(requiredExperience, requiredExperience2, lastVisibleExp, lastVisbileLevel);
		}

		private PlayerProgressionUpdateData GetProgressUpdateData()
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("PlayerProgressionUpdate").SingleEntity();
			if (stateEntity.hasStringData && !string.IsNullOrEmpty(stateEntity.stringData.Value))
			{
				return JsonUtility.FromJson<PlayerProgressionUpdateData>(stateEntity.stringData.Value);
			}
			return null;
		}

		public void SetElementsVisibility(bool shouldShowExperience = true, bool shouldShowCoins = true, bool shouldShowDiamonds = true, bool barsAreShown = true)
		{
			_experienceBar.gameObject.SetActive(shouldShowExperience);
			_coinsCounter.gameObject.SetActive(shouldShowCoins);
			_hcCounter.gameObject.SetActive(shouldShowDiamonds);
			_barHolder.anchoredPosition = _barOrigin + new Vector2(0f, barsAreShown ? 0f : _showBarOffset);
		}

		public void TurnOnButtonMode(UnityAction onCoinsClickedAction, UnityAction onHCClickedAction)
		{
			if (TutorialHelpers.IsTutorialLevelUnlocked(TutorialOutgameLevel.Shop))
			{
				_coinsCounter.TurnOnButtonMode(onCoinsClickedAction);
				_hcCounter.TurnOnButtonMode(onHCClickedAction);
			}
			else
			{
				TurnOffButtonMode();
			}
		}

		public void TurnOffButtonMode()
		{
			_coinsCounter.TurnOffButtonMode();
			_hcCounter.TurnOffButtonMode();
		}

		public void ShowBarWithAnimation()
		{
			if (_hideBarTween != null)
			{
				_hideBarTween.Kill();
				_hideBarTween = null;
			}
			if (_showBarTween == null)
			{
				float num = (_barHolder.anchoredPosition.y - _barOrigin.y) / _showBarOffset;
				_barHolder.anchoredPosition = _barOrigin + new Vector2(0f, _showBarOffset);
				_showBarTween = _barHolder.DOAnchorPosY(_barOrigin.y, num * _showBarDuration).SetUpdate(true).OnComplete(delegate
				{
					_showBarTween = null;
				});
			}
		}

		public void HideBarWithAnimation()
		{
			if (_showBarTween != null)
			{
				_showBarTween.Kill();
				_showBarTween = null;
			}
			if (_hideBarTween == null)
			{
				float num = (_barHolder.anchoredPosition.y - _barOrigin.y) / _showBarOffset;
				_hideBarTween = _barHolder.DOAnchorPosY(_barOrigin.y + _showBarOffset, (1f - num) * _showBarDuration).SetUpdate(true).OnComplete(delegate
				{
					base.uiContext.CloseDialog(this);
					_hideBarTween = null;
				});
			}
		}

		public void MoveBy(Vector3 displacement)
		{
			_barHolder.localPosition += displacement;
		}

		public void ShowCurrencyAnimation(Type type, int amount, RectTransform origin, bool updateTargetManually)
		{
			IsPlayingCurrencyAnimations = true;
			bool shouldShowBars = _barOrigin != _barHolder.anchoredPosition;
			if (type.Equals(Type.Coins))
			{
				_coinsCounter.gameObject.SetActive(true);
			}
			if (type.Equals(Type.Diamonds))
			{
				_hcCounter.gameObject.SetActive(true);
			}
			UpdateCurrencyBarsValues(type, amount, updateTargetManually);
			ShowBarWithAnimation();
			if (shouldShowBars)
			{
				_experienceBar.gameObject.SetActive(IsPlayingExperienceAnimations);
				DOVirtual.DelayedCall(_showBarDuration, delegate
				{
					SpawnAnimatedCurrency(type, origin);
				});
			}
			else
			{
				SpawnAnimatedCurrency(type, origin);
			}
			DOVirtual.DelayedCall(_showBarDuration + 2f * _flyingElementsDuration, delegate
			{
				IsPlayingCurrencyAnimations = false;
				if (shouldShowBars && !IsPlayingExperienceAnimations)
				{
					HideBarWithAnimation();
				}
			});
		}

		public void ShowExperienceAnimation(int currentLevelNumber, int currentExpAmount, Dictionary<int, PlayerLevelData> playerLevelsData, PlayerProgressionUpdateData progresUpdateData)
		{
			IsPlayingExperienceAnimations = true;
			_experienceBar.gameObject.SetActive(true);
			bool shouldShowBars = _barOrigin != _barHolder.anchoredPosition;
			ShowBarWithAnimation();
			if (shouldShowBars)
			{
				_coinsCounter.gameObject.SetActive(IsPlayingCurrencyAnimations);
				_hcCounter.gameObject.SetActive(IsPlayingCurrencyAnimations);
				DOVirtual.DelayedCall(_showBarDuration, delegate
				{
					UpdateExperienceBar(currentLevelNumber, currentExpAmount, playerLevelsData, progresUpdateData);
				});
			}
			else
			{
				UpdateExperienceBar(currentLevelNumber, currentExpAmount, playerLevelsData, progresUpdateData);
			}
			DOVirtual.DelayedCall(_showBarDuration + 2f * _flyingElementsDuration, delegate
			{
				IsPlayingExperienceAnimations = false;
				if (shouldShowBars && !IsPlayingCurrencyAnimations)
				{
					HideBarWithAnimation();
				}
			});
		}

		private void UpdateCurrencyBarsValues(Type type, int amount, bool updateTargetManually)
		{
			if (!updateTargetManually)
			{
				StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("Coins").SingleEntity();
				StateEntity stateEntity2 = Contexts.sharedInstance.state.GetEntitiesWithName("HardCurrency").SingleEntity();
				switch (type)
				{
				case Type.Coins:
					_coinsCounter.CounterEffect.Reset(stateEntity.integer.Amount - amount);
					break;
				case Type.Diamonds:
					_hcCounter.CounterEffect.Reset(stateEntity2.integer.Amount - amount);
					break;
				}
			}
		}

		private void SpawnAnimatedCurrency(Type type, RectTransform origin)
		{
			UIEntity uIEntity = Contexts.sharedInstance.uI.CreateEntity();
			switch (type)
			{
			case Type.Coins:
				uIEntity.AddFlyingElementEffect(_coinPrefab, origin, _coinsCounter.Icon, _flyingTokensAmount, _flyingElementsDuration, PlaySoftCurrencySound);
				UpdateCountersWithDelay(_flyingElementsDuration);
				break;
			case Type.Diamonds:
				uIEntity.AddFlyingElementEffect(_diamondPrefab, origin, _hcCounter.Icon, _flyingTokensAmount, _flyingElementsDuration, PlayHardCurrencySound);
				UpdateCountersWithDelay(_flyingElementsDuration);
				break;
			}
		}

		private void UpdateExperienceBar(int currentLevelNumber, int currentExpAmount, Dictionary<int, PlayerLevelData> playerLevelsData, PlayerProgressionUpdateData progresUpdateData)
		{
			int requiredExperience = playerLevelsData[progresUpdateData.LastVisbileLevel].RequiredExperience;
			int requiredExperience2 = playerLevelsData[progresUpdateData.LastVisbileLevel + 1].RequiredExperience;
			int lastVisibleExp = progresUpdateData.LastVisibleExp;
			int lastVisbileLevel = progresUpdateData.LastVisbileLevel;
			_experienceBar.SetBarAndText(requiredExperience, requiredExperience2, lastVisibleExp, lastVisbileLevel);
			SpawnAnimatedExperience(currentExpAmount, lastVisibleExp);
			StartCoroutine(_experienceBar.UpdateBar(currentLevelNumber, currentExpAmount, playerLevelsData, lastVisbileLevel, _flyingElementsDuration));
		}

		private void SpawnAnimatedExperience(int currentExpAmount, int lastExperienceVisible)
		{
			BaseDialog baseDialog = Contexts.sharedInstance.uI.FindCurrentScreen();
			RectTransform newStartPoint = ((!(baseDialog != null)) ? base.gameObject.GetComponent<RectTransform>() : baseDialog.GetLastFocusElement());
			int newElementsAmount = (currentExpAmount - lastExperienceVisible) / 10;
			base.uiContext.CreateEntity().AddFlyingElementEffect(_starPrefab, newStartPoint, _experienceBar.Star, newElementsAmount, _flyingElementsDuration, delegate
			{
			});
		}

		private void UpdateCountersWithDelay(float delay)
		{
			StateEntity stateEntity = Contexts.sharedInstance.state.GetEntitiesWithName("Coins").SingleEntity();
			StateEntity stateEntity2 = Contexts.sharedInstance.state.GetEntitiesWithName("HardCurrency").SingleEntity();
			UpdateCounterWithDelay(_coinsCounter.CounterEffect, stateEntity.integer.Amount, delay);
			UpdateCounterWithDelay(_hcCounter.CounterEffect, stateEntity2.integer.Amount, delay);
		}

		private void UpdateCounterWithDelay(CounterEffect counter, int currentAmount, float delay = 0f)
		{
			counter.TargetCount = currentAmount;
			counter.SetDelay(delay);
		}

		private void PlaySoftCurrencySound()
		{
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect("coins_received");
		}

		private void PlayHardCurrencySound()
		{
			Contexts.sharedInstance.state.CreateEntity().AddPlaySoundEffect("diamonds_received");
		}
	}
}
