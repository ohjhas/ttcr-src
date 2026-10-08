using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class WinScreenProgressBarStarController : MonoBehaviour
	{
		private const string REACHED_ANIMATION = "starReached";

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
		}

		public void SetReached()
		{
			if (!isReached)
			{
				isReached = true;
				fullStarImage.enabled = true;
				starAnimator.Play("starReached", 0, 0f);
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
			particle.StartEmitter();
		}
	}
}
