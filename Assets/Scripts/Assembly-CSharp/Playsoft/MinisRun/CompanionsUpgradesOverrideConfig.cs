using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class CompanionsUpgradesOverrideConfig
	{
		[SerializeField]
		private int id;

		[SerializeField]
		private int l;

		[SerializeField]
		private int uCSC;

		[SerializeField]
		private int uCP;

		[SerializeField]
		private float aP;

		[SerializeField]
		private float aD;

		[SerializeField]
		private float aCT;

		public int Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
			}
		}

		public int Level
		{
			get
			{
				return l;
			}
			set
			{
				l = value;
			}
		}

		public int Upgradecostsc
		{
			get
			{
				return uCSC;
			}
			set
			{
				uCSC = value;
			}
		}

		public int Upgradecostparts
		{
			get
			{
				return uCP;
			}
			set
			{
				uCP = value;
			}
		}

		public float Abilityparameter
		{
			get
			{
				return aP;
			}
			set
			{
				aP = value;
			}
		}

		public float Abilityduration
		{
			get
			{
				return aD;
			}
			set
			{
				aD = value;
			}
		}

		public float Abilitychargetime
		{
			get
			{
				return aCT;
			}
			set
			{
				aCT = value;
			}
		}

		public CompanionsUpgradesBalancingData ConvertToBalancingData()
		{
			CompanionsUpgradesBalancingData companionsUpgradesBalancingData = new CompanionsUpgradesBalancingData();
			companionsUpgradesBalancingData.ID = id;
			companionsUpgradesBalancingData.Level = Level;
			companionsUpgradesBalancingData.Upgradecostparts = Upgradecostparts;
			companionsUpgradesBalancingData.Upgradecostsc = Upgradecostsc;
			companionsUpgradesBalancingData.Abilitychargetime = Abilitychargetime;
			companionsUpgradesBalancingData.Abilityduration = Abilityduration;
			companionsUpgradesBalancingData.Abilityparameter = Abilityparameter;
			return companionsUpgradesBalancingData;
		}
	}
}
