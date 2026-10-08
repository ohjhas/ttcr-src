using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class CameraShakeBehaviour : MonoBehaviour, IObstacleHitListener, IPlayerLandListener
	{
		[SerializeField]
		private Transform MainCamera;

		private Tweener obstaclePunchTweener;

		private Tweener landingPunchTweener;

		private SettingsVisualFeedbackSO visualFeedbackSettings;

		private void Start()
		{
			GameEntity gameEntity = Contexts.sharedInstance.game.CreateEntity();
			gameEntity.AddObstacleHitListener(this);
			gameEntity.AddPlayerLandListener(this);
			visualFeedbackSettings = SettingsService.GetSetting<SettingsVisualFeedbackSO>();
		}

		public void UpdateView()
		{
			StopExistingTweens();
			obstaclePunchTweener = MainCamera.DOPunchPosition(visualFeedbackSettings.ObstaclePunchVector, visualFeedbackSettings.PunchDuration, visualFeedbackSettings.PunchVibrato, visualFeedbackSettings.PunchElasticity).OnComplete(delegate
			{
				obstaclePunchTweener = null;
			});
		}

		public void OnPlayerLand()
		{
			StopExistingTweens();
			landingPunchTweener = MainCamera.DOPunchPosition(visualFeedbackSettings.LandingPunchVector, visualFeedbackSettings.PunchDuration, visualFeedbackSettings.PunchVibrato, visualFeedbackSettings.PunchElasticity).OnComplete(delegate
			{
				landingPunchTweener = null;
			});
		}

		private void StopExistingTweens()
		{
			if (landingPunchTweener != null)
			{
				landingPunchTweener.Complete(true);
			}
			if (obstaclePunchTweener != null)
			{
				obstaclePunchTweener.Complete(true);
			}
		}
	}
}
