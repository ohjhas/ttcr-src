namespace Playsoft.MinisRun
{
	public class EPrivacyPopup : BasePopupScreen
	{
		public override ScreenId Id
		{
			get
			{
				return ScreenId.EPrivacyPopup;
			}
		}

		public override void SetVisible(bool visible)
		{
			base.SetVisible(visible);
			if (!visible)
			{
				base.uiContext.OpenDialog<PrivacyAndLegalPopup>();
			}
		}
	}
}
