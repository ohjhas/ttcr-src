using System;
using UnityEngine;

[Serializable]
public class PrivacyAndLegalBalancingData
{
	[SerializeField]
	private string contactadressandroid;

	[SerializeField]
	private string contactadressapple;

	[SerializeField]
	private string eulaadress;

	[SerializeField]
	private string privacyadress;

	[SerializeField]
	private string outfit7home;

	[SerializeField]
	private string otherscopyright;

	public string Contactadressandroid
	{
		get
		{
			return contactadressandroid;
		}
		set
		{
			contactadressandroid = value;
		}
	}

	public string Contactadressapple
	{
		get
		{
			return contactadressapple;
		}
		set
		{
			contactadressapple = value;
		}
	}

	public string Eulaadress
	{
		get
		{
			return eulaadress;
		}
		set
		{
			eulaadress = value;
		}
	}

	public string Privacyadress
	{
		get
		{
			return privacyadress;
		}
		set
		{
			privacyadress = value;
		}
	}

	public string Outfit7home
	{
		get
		{
			return outfit7home;
		}
		set
		{
			outfit7home = value;
		}
	}

	public string Otherscopyright
	{
		get
		{
			return otherscopyright;
		}
		set
		{
			otherscopyright = value;
		}
	}
}
