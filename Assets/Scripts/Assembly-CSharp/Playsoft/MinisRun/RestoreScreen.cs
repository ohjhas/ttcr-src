using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class RestoreScreen : BaseDialog
	{
		[SerializeField]
		private Button restoreButton;

		[SerializeField]
		private Button continueButton;

		[SerializeField]
		private UserStateView restoreView;

		[SerializeField]
		private UserStateView currentView;

		private StateContext stateContext
		{
			get
			{
				return Contexts.sharedInstance.state;
			}
		}

		public override ScreenId Id
		{
			get
			{
				return ScreenId.RestoreScreen;
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (visible)
			{
				restoreButton.onClick.RemoveAllListeners();
				continueButton.onClick.RemoveAllListeners();
				restoreButton.onClick.AddListener(OnRestoreButton);
				continueButton.onClick.AddListener(OnContinueButton);
				restoreView.SetData(stateContext.o7SdkEntity.remoteUserState.State);
				currentView.SetData(stateContext.localUserStateEntity.localUserState.StateData);
			}
		}

		private void OnRestoreButton()
		{
			Contexts.sharedInstance.state.CreateEntity().ReplaceUserStateSolveConflict(false);
		}

		private void OnContinueButton()
		{
			Contexts.sharedInstance.state.CreateEntity().isUserStateTryStartNewGame = true;
		}
	}
}
