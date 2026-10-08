using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class HitFeedbackBehaviour : MonoBehaviour, IObstacleHitListener
	{
		[SerializeField]
		private Image RedFade;

		private Color startColor;

		private SettingsVisualFeedbackSO visualFeedbackSettings;

		private void Start()
		{
			visualFeedbackSettings = SettingsService.GetSetting<SettingsVisualFeedbackSO>();
			Contexts.sharedInstance.game.CreateEntity().AddObstacleHitListener(this);
			RedFade.enabled = false;
			startColor = new Color(visualFeedbackSettings.EndColor.r, visualFeedbackSettings.EndColor.g, visualFeedbackSettings.EndColor.b, 0f);
		}

		public void UpdateView()
		{
			RedFade.enabled = true;
			RedFade.color = startColor;
			Tweener t = RedFade.DOColor(visualFeedbackSettings.EndColor, visualFeedbackSettings.AnimTime * 0.5f);
			t.OnComplete(HideAnimation);
		}

		private void HideAnimation()
		{
			if (RedFade != null)
			{
				Tweener t = RedFade.DOColor(startColor, visualFeedbackSettings.AnimTime * 0.5f);
				t.OnComplete(delegate
				{
					RedFade.enabled = false;
				});
			}
		}
	}
}
