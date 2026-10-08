using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public abstract class Notification : BaseUI
	{
		[SerializeField]
		protected Image notificationImage;

		public virtual void ShowNotification()
		{
			notificationImage.enabled = true;
		}

		public virtual void HideNotification()
		{
			notificationImage.enabled = false;
		}
	}
}
