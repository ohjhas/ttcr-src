using System;
using UnityEngine;

namespace O7SDK.O7ADSDK
{
	public class O7ADHandler : MonoBehaviour
	{
		public event Action OnAdShowSuccess;

		public event Action OnAdShowFail;

		public event Action OnAdLoadSuccess;

		public event Action OnAdLoadFail;

		public event Action<bool> OnAdClosed;

		private void AdShowSuccess()
		{
			if (this.OnAdShowSuccess != null)
			{
				this.OnAdShowSuccess();
			}
		}

		private void AdShowFail()
		{
			if (this.OnAdShowFail != null)
			{
				this.OnAdShowFail();
			}
		}

		private void AdLoadSuccess()
		{
			if (this.OnAdLoadSuccess != null)
			{
				this.OnAdLoadSuccess();
			}
		}

		private void AdLoadFail()
		{
			if (this.OnAdLoadFail != null)
			{
				this.OnAdLoadFail();
			}
		}

		private void AdClosed(string isRewarded)
		{
			if (this.OnAdClosed != null)
			{
				if (isRewarded == "true")
				{
					this.OnAdClosed(true);
				}
				else
				{
					this.OnAdClosed(false);
				}
			}
		}
	}
}
