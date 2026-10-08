using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class StaminaParticlesBehaviour : MonoBehaviour, IStaminaListener
	{
		[SerializeField]
		private GameObject _flyingParticlesPrefab;

		[SerializeField]
		private Camera _mainCamera;

		[SerializeField]
		private Camera _uiCamera;

		private SettingsIngameHUDSO _settingsSO;

		private Tweener shakeTween;

		private void Start()
		{
			Contexts.sharedInstance.game.CreateEntity().AddStaminasListener(this);
			_settingsSO = SettingsService.GetSetting<SettingsIngameHUDSO>();
		}

		public void OnStaminaPotionCollected(Vector3 staminaPosition)
		{
			Vector3 position = _mainCamera.WorldToViewportPoint(staminaPosition);
			Vector3 position2 = _uiCamera.ViewportToWorldPoint(position);
			Vector3 position3 = base.transform.position;
			position2.z = 0f;
			GameObject _flyingParticles = Object.Instantiate(_flyingParticlesPrefab, base.transform);
			_flyingParticles.transform.position = position2;
			DOTween.Sequence().Append(_flyingParticles.transform.DOMove(position3, _settingsSO.StaminaFlyTimelength)).Join(_flyingParticles.transform.DOScale(2f * _flyingParticles.transform.localScale, _settingsSO.StaminaFlyTimelength).SetEase(_settingsSO.StaminaFlyAnimationCurve))
				.OnComplete(delegate
				{
					Object.Destroy(_flyingParticles);
					if (shakeTween != null)
					{
						shakeTween.Complete();
					}
					shakeTween = base.transform.DOPunchScale(Vector3.one * 0.2f, 0.5f, 2);
				})
				.SetUpdate(true);
		}
	}
}
