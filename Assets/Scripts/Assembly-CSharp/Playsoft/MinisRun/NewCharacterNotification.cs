namespace Playsoft.MinisRun
{
	public class NewCharacterNotification : Notification
	{
		public override void SetData(UIEntity parameters)
		{
			if (parameters == null)
			{
				HideNotification();
			}
			else if (!parameters.hasUINewCharactersUnlockedResponse)
			{
				HideNotification();
			}
			else if (parameters.uINewCharactersUnlockedResponse.Unlocks.UnlockedIds != null && parameters.uINewCharactersUnlockedResponse.Unlocks.UnlockedIds.Count > 0)
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
