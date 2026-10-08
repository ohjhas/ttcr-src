using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class RestoreConfirmPopup : BasePopupScreen
	{
		[SerializeField]
		private Button yesButton;

		[SerializeField]
		private Button noButton;

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				yesButton.onClick.RemoveAllListeners();
				noButton.onClick.RemoveAllListeners();
				yesButton.onClick.AddListener(YesClicked);
				noButton.onClick.AddListener(NoClicked);
			}
		}

		private void YesClicked()
		{
			Contexts.sharedInstance.state.CreateEntity().ReplaceUserStateSolveConflict(true);
		}

		private void NoClicked()
		{
			SetVisible(false);
		}
	}
}
