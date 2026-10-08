using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class CharactersUpgradesOverrideConfig
	{
		[SerializeField]
		private int id;

		[SerializeField]
		private int l;

		[SerializeField]
		private int s;

		[SerializeField]
		private int li;

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

		public int Stamina
		{
			get
			{
				return s;
			}
			set
			{
				s = value;
			}
		}

		public int Lifes
		{
			get
			{
				return li;
			}
			set
			{
				li = value;
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

		public CharactersUpgradesBalancingData ConvertToBalancingData()
		{
			CharactersUpgradesBalancingData charactersUpgradesBalancingData = new CharactersUpgradesBalancingData();
			charactersUpgradesBalancingData.ID = id;
			charactersUpgradesBalancingData.Level = Level;
			charactersUpgradesBalancingData.Lifes = Lifes;
			charactersUpgradesBalancingData.Stamina = Stamina;
			charactersUpgradesBalancingData.Upgradecostparts = Upgradecostparts;
			charactersUpgradesBalancingData.Upgradecostsc = Upgradecostsc;
			charactersUpgradesBalancingData.Abilitychargetime = Abilitychargetime;
			charactersUpgradesBalancingData.Abilityduration = Abilityduration;
			charactersUpgradesBalancingData.Abilityparameter = Abilityparameter;
			return charactersUpgradesBalancingData;
		}
	}
}
