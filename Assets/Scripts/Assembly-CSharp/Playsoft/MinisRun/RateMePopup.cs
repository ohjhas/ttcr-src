namespace Playsoft.MinisRun
{
	public sealed class RateMePopup : BaseScreen
	{
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
				return ScreenId.RateMePopup;
			}
		}

		public void OnRateMeNowClicked()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkRateMeNow = true;
			CloseDialog();
		}

		public void OnRateMeLaterClicked()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkRateMeLater = true;
			CloseDialog();
		}

		public void OnIdontLikeItClicked()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkRateMeDontLike = true;
			CloseDialog();
		}

		public void OnCloseClicked()
		{
			StateEntity stateEntity = stateContext.CreateEntity();
			stateEntity.isO7SdkRateMeClosed = true;
			CloseDialog();
		}

		private void CloseDialog()
		{
			InputEntity inputEntity = Contexts.sharedInstance.input.CreateEntity();
			inputEntity.isInput = true;
			inputEntity.isGoBack = true;
		}
	}
}
