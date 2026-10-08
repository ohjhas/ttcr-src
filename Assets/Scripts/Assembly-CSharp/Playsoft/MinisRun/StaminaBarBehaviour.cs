using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class StaminaBarBehaviour : MexicanWaveBarBehaviour, IStaminaListener
	{
		private enum TraceState
		{
			Waiting = 0,
			Moving = 1
		}

		[Serializable]
		private class ContainerData
		{
			public RectTransform RT;

			public float yOffset = 5f;

			public float startBarSize = 130f;

			public float endBarSize = 97f;

			public float maxMiddleBarSize = 95f;
		}

		[SerializeField]
		private ContainerData Container;

		[SerializeField]
		private List<GameObject> _bars;

		private List<Image> fillings;

		private List<GameObject> fillingsGO;

		private List<Image> traces;

		private List<GameObject> visibleBars;

		[SerializeField]
		private float _waitingTime = 0.35f;

		[SerializeField]
		private float _movingTime = 0.35f;

		private GameEntity _player;

		private bool _areBarsShowed;

		private int _numberOfBarsToShow;

		private float _currentStaminaRatio;

		private TraceState _traceState;

		private float _traceAnimationStartValue;

		private float _currentTraceRatio;

		private float _previousStaminaRatio;

		private Coroutine _animation;

		private bool isFillingAnimation;

		private float fillingDelayTimer;

		private float currentlyDisplayedStamina;

		[HideInInspector]
		public bool UseUnscaledTime;

		private StaminaSettingsBalancingDataComponent staminaSettings
		{
			get
			{
				return (Contexts.sharedInstance == null || Contexts.sharedInstance.state == null || Contexts.sharedInstance.state.balancingDataEntity == null || !Contexts.sharedInstance.state.balancingDataEntity.hasStaminaSettingsBalancingData) ? null : Contexts.sharedInstance.state.balancingDataEntity.staminaSettingsBalancingData;
			}
		}

		private float fixedDeltaTime
		{
			get
			{
				return UseUnscaledTime ? Time.fixedUnscaledDeltaTime : Time.fixedDeltaTime;
			}
		}

		private float deltaTime
		{
			get
			{
				return UseUnscaledTime ? Time.unscaledDeltaTime : Time.deltaTime;
			}
		}

		protected new void Awake()
		{
			base.Awake();
			HideAllBars();
			_traceState = TraceState.Waiting;
			_previousStaminaRatio = 1f;
		}

		private void Start()
		{
			Contexts.sharedInstance.game.CreateEntity().AddStaminasListener(this);
		}

		private void Update()
		{
			_player = Contexts.sharedInstance.game.playerEntity;
			if (_player != null || _bars.Count > 0 || staminaSettings == null)
			{
				if (!_areBarsShowed)
				{
					SetNumberOfBarsToShow();
					ShowProperBars();
					GetAllFillings();
					GetAllTraces();
				}
				SetContainerSize();
				float num = staminaSettings.Map[StaminaSettings.MaxStamina] / (float)_bars.Count;
				currentlyDisplayedStamina = (isFillingAnimation ? CalculateStamina() : _player.stamina.Value);
				_currentStaminaRatio = currentlyDisplayedStamina / ((float)_numberOfBarsToShow * num);
				Fill(fillings, _currentStaminaRatio);
				UpdateTrace();
			}
		}

		public void OnStaminaPotionCollected(Vector3 staminaPosition)
		{
			if (!isFillingAnimation)
			{
				fillingDelayTimer = 0f;
				currentlyDisplayedStamina = _player.stamina.Value;
			}
			isFillingAnimation = true;
			SettingsIngameHUDSO setting = SettingsService.GetSetting<SettingsIngameHUDSO>();
			PunchBarsIfPossible(fillingsGO, setting.StaminaFlyTimelength, setting.StaminaBarBounceEffectSingleBounceDuration, setting.StaminaBarBounceEffectInBetweenPunchesOffset, setting.StaminaBarBounceEffectFirstBounceStrength, setting.StaminaBarBounceEffectLastBounceStrength);
		}

		private void SetNumberOfBarsToShow()
		{
			if (SettingsService.SettingsSO.StartingStamina > staminaSettings.Map[StaminaSettings.MaxStamina])
			{
				Debug.LogError("Starting stamina to big: " + SettingsService.SettingsSO.StartingStamina + "! Changing to maxStamina: " + staminaSettings.Map[StaminaSettings.MaxStamina] + "! Check balancing data!");
				SettingsService.SettingsSO.StartingStamina = staminaSettings.Map[StaminaSettings.MaxStamina];
			}
			float f = SettingsService.SettingsSO.StartingStamina / staminaSettings.Map[StaminaSettings.MaxStamina] * (float)_bars.Count;
			_numberOfBarsToShow = Mathf.CeilToInt(f);
		}

		private void GetAllFillings()
		{
			fillings = new List<Image>();
			fillingsGO = new List<GameObject>();
			foreach (GameObject visibleBar in visibleBars)
			{
				if (visibleBar.activeSelf)
				{
					Image component = visibleBar.transform.Find("Filling").GetComponent<Image>();
					fillings.Add(component);
					fillingsGO.Add(component.gameObject);
				}
			}
		}

		private void GetAllTraces()
		{
			traces = new List<Image>();
			foreach (GameObject visibleBar in visibleBars)
			{
				if (visibleBar.activeSelf)
				{
					traces.Add(visibleBar.transform.Find("Trace").GetComponent<Image>());
				}
			}
		}

		private void SetContainerSize()
		{
			Container.RT.offsetMin = new Vector2(0f, Container.yOffset);
			Container.RT.offsetMax = new Vector2(0f, 0f - Container.yOffset);
			float width = Container.RT.rect.width;
			float num = Container.startBarSize + Container.endBarSize + Container.maxMiddleBarSize * (float)(_numberOfBarsToShow - 2);
			if (width > num)
			{
				float num2 = num - width;
				Container.RT.offsetMin = new Vector2((0f - num2) * 0.5f, Container.yOffset);
				Container.RT.offsetMax = new Vector2(num2 * 0.5f, 0f - Container.yOffset);
			}
		}

		private void HideAllBars()
		{
			_bars.ForEach(delegate(GameObject bar)
			{
				bar.SetActive(false);
			});
			_areBarsShowed = false;
		}

		private void ShowProperBars()
		{
			visibleBars = GetBarsThatShouldBeVisible();
			foreach (GameObject visibleBar in visibleBars)
			{
				visibleBar.SetActive(true);
			}
			_areBarsShowed = true;
		}

		private List<GameObject> GetBarsThatShouldBeVisible()
		{
			List<GameObject> list = new List<GameObject>();
			list.Add(_bars[0]);
			for (int i = 1; i < _numberOfBarsToShow - 1; i++)
			{
				list.Add(_bars[i]);
			}
			list.Add(_bars[_bars.Count - 1]);
			return list;
		}

		private float CalculateStamina()
		{
			fillingDelayTimer += fixedDeltaTime;
			SettingsIngameHUDSO setting = SettingsService.GetSetting<SettingsIngameHUDSO>();
			float num = setting.StaminaFillSpeed * fixedDeltaTime;
			float num2 = currentlyDisplayedStamina + num;
			if (_player.stamina.Value <= currentlyDisplayedStamina || _player.stamina.Value <= num2)
			{
				isFillingAnimation = false;
				return _player.stamina.Value;
			}
			return (!(fillingDelayTimer >= setting.StaminaFlyTimelength)) ? currentlyDisplayedStamina : num2;
		}

		private void UpdateTrace()
		{
			float num = currentlyDisplayedStamina / SettingsService.SettingsSO.StartingStamina;
			float num2 = _previousStaminaRatio - num;
			if (num2 <= 0f || num2 * SettingsService.SettingsSO.StartingStamina <= staminaSettings.Map[StaminaSettings.StaminaDecreasingFactor])
			{
				MakeTraceFollowStamina();
			}
			else if (_traceState == TraceState.Waiting && _animation == null)
			{
				_animation = StartCoroutine(ShowTraceAnimation());
			}
		}

		private IEnumerator ShowTraceAnimation()
		{
			float timer = 0f;
			for (timer = _waitingTime; timer > 0f; timer -= deltaTime)
			{
				yield return 0;
			}
			_traceState = TraceState.Moving;
			timer = 0f;
			while (_currentTraceRatio > _currentStaminaRatio)
			{
				timer += deltaTime;
				_currentTraceRatio = Mathf.Lerp(_traceAnimationStartValue, _currentStaminaRatio, timer / _movingTime);
				Fill(traces, _currentTraceRatio);
				yield return null;
			}
			MakeTraceFollowStamina();
		}

		private void MakeTraceFollowStamina()
		{
			_animation = null;
			_currentTraceRatio = _currentStaminaRatio;
			_previousStaminaRatio = _currentStaminaRatio;
			_traceAnimationStartValue = _currentStaminaRatio;
			_traceState = TraceState.Waiting;
			StopAllCoroutines();
			Fill(traces, _currentStaminaRatio);
		}

		private void Fill(List<Image> fillables, float fillAmount)
		{
			float num = fillAmount * (float)_numberOfBarsToShow;
			int num2 = (int)num;
			float fillAmount2 = num - (float)num2;
			for (int i = 0; i < _numberOfBarsToShow; i++)
			{
				if (i < num2)
				{
					fillables[i].fillAmount = 1f;
				}
				else if (i == num2)
				{
					fillables[num2].fillAmount = fillAmount2;
				}
				else
				{
					fillables[i].fillAmount = 0f;
				}
			}
		}
	}
}
