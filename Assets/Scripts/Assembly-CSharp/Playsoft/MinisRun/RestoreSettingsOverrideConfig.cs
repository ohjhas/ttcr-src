using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class RestoreSettingsOverrideConfig
	{
		[SerializeField]
		private int rC;

		[SerializeField]
		private int rDT;

		[SerializeField]
		private int reC;

		[SerializeField]
		private int mRePP;

		[SerializeField]
		private int reFVPP;

		[SerializeField]
		private int reSA;

		[SerializeField]
		private int reDT;

		[SerializeField]
		private int rvC;

		[SerializeField]
		private int mRvPP;

		[SerializeField]
		private int rvFV;

		[SerializeField]
		private int rvSA;

		[SerializeField]
		private int rvDT;

		public int RelayCost
		{
			get
			{
				return rC;
			}
			set
			{
				rC = value;
			}
		}

		public int RelayDecisionTime
		{
			get
			{
				return rDT;
			}
			set
			{
				rDT = value;
			}
		}

		public int ResurectCost
		{
			get
			{
				return reC;
			}
			set
			{
				reC = value;
			}
		}

		public int MaxResurectsPerPlay
		{
			get
			{
				return mRePP;
			}
			set
			{
				mRePP = value;
			}
		}

		public int ResurectsForVideoPerPlay
		{
			get
			{
				return reFVPP;
			}
			set
			{
				reFVPP = value;
			}
		}

		public int ResurectStaminaAmount
		{
			get
			{
				return reSA;
			}
			set
			{
				reSA = value;
			}
		}

		public int ResurectDecisionTime
		{
			get
			{
				return reDT;
			}
			set
			{
				reDT = value;
			}
		}

		public int ReviveCost
		{
			get
			{
				return rvC;
			}
			set
			{
				rvC = value;
			}
		}

		public int MaxRevivesPerPlay
		{
			get
			{
				return mRvPP;
			}
			set
			{
				mRvPP = value;
			}
		}

		public int RevivesForVideo
		{
			get
			{
				return rvFV;
			}
			set
			{
				rvFV = value;
			}
		}

		public int ReviveMinStaminaAmount
		{
			get
			{
				return rvSA;
			}
			set
			{
				rvSA = value;
			}
		}

		public int ReviveDecisionTime
		{
			get
			{
				return rvDT;
			}
			set
			{
				rvDT = value;
			}
		}
	}
}
