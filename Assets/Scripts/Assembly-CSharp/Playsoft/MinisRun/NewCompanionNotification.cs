namespace Playsoft.MinisRun
{
	public class NewCompanionNotification : Notification
	{
		public override void SetData(UIEntity parameters)
		{
			if (parameters == null)
			{
				HideNotification();
			}
			else if (!parameters.hasUINewCompanionsUnlockedResponse)
			{
				HideNotification();
			}
			else if (parameters.uINewCompanionsUnlockedResponse.Unlocks.UnlockedIds != null && parameters.uINewCompanionsUnlockedResponse.Unlocks.UnlockedIds.Count > 0)
			{
				ShowNotification();
			}
			else
			{
				HideNotification();
			}
		}
	}
}
