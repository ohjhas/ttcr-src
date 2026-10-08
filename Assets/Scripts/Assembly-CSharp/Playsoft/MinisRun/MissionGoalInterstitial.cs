using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class MissionGoalInterstitial : MonoBehaviour
	{
		[SerializeField]
		private RectTransform contentHolder;

		[SerializeField]
		private TextMeshProUGUI titleText;

		[SerializeField]
		private Transform destination;

		[SerializeField]
		private Transform origin;

		public MissionProgressBar missionProgressBar;

		[Header("Pulsating")]
		[SerializeField]
		private Material pulsatingMaterial;

		[SerializeField]
		private Image background;

		private void Start()
		{
			contentHolder.anchoredPosition = new Vector2(0f, 500f);
			missionProgressBar.Hide();
		}

		public void Show()
		{
			GameEntity worldEntity = Contexts.sharedInstance.game.worldEntity;
			titleText.text = MissionHelper.GetMissionNameByType(worldEntity.missionType.Type);
			missionProgressBar.transform.SetParent(origin.parent, true);
			missionProgressBar.transform.position = origin.position;
			missionProgressBar.transform.localScale = 1.35f * Vector2.one;
			missionProgressBar.ShowWithoutAnimation();
			contentHolder.DOAnchorPosY(-170f, 0.5f).SetEase(Ease.OutBack);
		}

		public void Hide()
		{
			PlayFlyingAnimation();
			contentHolder.DOScale(0f, 0.5f);
		}

		public void Pulsate()
		{
			background.material = pulsatingMaterial;
			PassUnscaledTimeToShader passUnscaledTimeToShader = background.gameObject.AddComponent<PassUnscaledTimeToShader>();
			passUnscaledTimeToShader.Image = background;
		}

		private void PlayFlyingAnimation()
		{
			if (!(null == destination))
			{
				missionProgressBar.transform.SetParent(destination);
				SettingsIngameHUDSO setting = SettingsService.GetSetting<SettingsIngameHUDSO>();
				float duration = setting.MissionGoalMoveAnimationDelay + setting.MissionGoalAnimationMoveAnimationDuration;
				DOTween.Sequence().Append(missionProgressBar.transform.DOMove(destination.position, duration).SetEase(setting.MissionGoalMoveAnimationCurve)).Insert(setting.MissionGoalMoveAnimationDelay, missionProgressBar.transform.DOScale(1f, setting.MissionGoalAnimationMoveAnimationDuration).SetEase(setting.MissionGoalScaleAnimationCurve))
					.OnComplete(delegate
					{
						Debug.Log("PlayFlyingAnimation OnComplete");
					});
			}
		}
	}
}
