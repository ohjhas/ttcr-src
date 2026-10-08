using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class StaminaSettingsOverrideConfig
	{
		[SerializeField]
		private int sLPH;

		[SerializeField]
		private int sDF;

		[SerializeField]
		private int mS;

		public int StaminaLostPerHit
		{
			get
			{
				return sLPH;
			}
			set
			{
				sLPH = value;
			}
		}

		public int StaminaDecreasingFactor
		{
			get
			{
				return sDF;
			}
			set
			{
				sDF = value;
			}
		}

		public int MaxStamina
		{
			get
			{
				return mS;
			}
			set
			{
				mS = value;
			}
		}
	}
}
