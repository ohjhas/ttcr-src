namespace Playsoft.MinisRun
{
	public class LoadingSpinnerPopup : BasePopupScreen
	{
		public override ScreenId Id
		{
			get
			{
				return ScreenId.LoadingSpinnerPopup;
			}
		}

		public void BlockRaycasts(bool blocksRaycasts)
		{
			if (!(ContentGroup == null))
			{
				ContentGroup.blocksRaycasts = blocksRaycasts;
			}
		}
	}
}
