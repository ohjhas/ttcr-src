using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class MissionProgressBarStar : MonoBehaviour
	{
		private const string COLLECT_ANIMATION = "starCollect";

		[SerializeField]
		private Animator starAnimator;

		[SerializeField]
		private CanvasParticleEmitter particle;

		[SerializeField]
		private TextMeshProUGUI goalText;

		[SerializeField]
		private TextMeshProUGUI goalTextShadow;

		[SerializeField]
		private Image fullStarImage;

		private bool isReached;

		public void Init(int goalValue)
		{
			TextMeshProUGUI textMeshProUGUI = goalText;
			string text = goalValue.ToString();
			goalTextShadow.text = text;
			textMeshProUGUI.text = text;
			particle.enabled = false;
		}

		public void SetReached()
		{
			if (!isReached)
			{
				isReached = true;
				fullStarImage.enabled = true;
				starAnimator.Play("starCollect", 0, 0f);
			}
		}

		public void SetNotReached()
		{
			if (isReached)
			{
				isReached = false;
				fullStarImage.enabled = false;
			}
		}

		public void ShowParticleEffect()
		{
			particle.enabled = true;
			particle.StartEmitter();
		}
	}
}
