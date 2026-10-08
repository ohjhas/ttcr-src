using DG.Tweening;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class DailyMissionOpenBehaviour : TutorialElementBehaviour
	{
		[SerializeField]
		private Animator animator;

		private bool animationDone;

		private void OnEnable()
		{
			if (ShoudlHighlightBase())
			{
				animator.enabled = false;
				base.transform.DOScale(Vector3.zero, 1f).From().SetUpdate(UpdateType.Late, false)
					.SetEase(Ease.OutBack)
					.OnComplete(delegate
					{
						animationDone = true;
						animator.enabled = true;
					});
			}
		}

		private bool ShoudlHighlightBase()
		{
			return TutorialHelpers.IsTutorialLevelAvailable(TutorialOutgameLevel.DailyMissions);
		}

		protected override bool ShouldHighlight()
		{
			return ShoudlHighlightBase() && animationDone;
		}
	}
}
