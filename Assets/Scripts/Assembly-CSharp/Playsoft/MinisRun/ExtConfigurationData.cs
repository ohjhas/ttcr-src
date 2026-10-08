using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class ExtConfigurationData
	{
		[SerializeField]
		private int dMCId = -1;

		[SerializeField]
		private int iC = -1;

		[SerializeField]
		private int iHC = -1;

		[SerializeField]
		private int rDMH = -1;

		[SerializeField]
		private RestoreSettingsOverrideConfig rS;

		[SerializeField]
		private StaminaSettingsOverrideConfig sC;

		public int DailyMissionChestId
		{
			get
			{
				return dMCId;
			}
			set
			{
				dMCId = value;
			}
		}

		public int InitialCoins
		{
			get
			{
				return iC;
			}
			set
			{
				iC = value;
			}
		}

		public int InitialDiamonds
		{
			get
			{
				return iHC;
			}
			set
			{
				iHC = value;
			}
		}

		public int ResetDailyMissions_H
		{
			get
			{
				return rDMH;
			}
			set
			{
				rDMH = value;
			}
		}

		public RestoreSettingsOverrideConfig RestoreSettings
		{
			get
			{
				return rS;
			}
			set
			{
				rS = value;
			}
		}

		public StaminaSettingsOverrideConfig StaminaSettings
		{
			get
			{
				return sC;
			}
			set
			{
				sC = value;
			}
		}
	}
}
