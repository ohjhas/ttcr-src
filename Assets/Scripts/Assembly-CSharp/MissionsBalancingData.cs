using System;
using UnityEngine;

[Serializable]
public class MissionsBalancingData
{
	[SerializeField]
	private int chapter;

	[SerializeField]
	private int levelindex;

	[SerializeField]
	private string levelname;

	[SerializeField]
	private MissionType missiontype;

	[SerializeField]
	private int star1;

	[SerializeField]
	private int star2;

	[SerializeField]
	private int star3;

	[SerializeField]
	private string missionid;

	[SerializeField]
	private string background;

	[SerializeField]
	private string worldskin;

	[SerializeField]
	private int camerasize;

	[SerializeField]
	private int worldspeed;

	public int Chapter
	{
		get
		{
			return chapter;
		}
		set
		{
			chapter = value;
		}
	}

	public int Levelindex
	{
		get
		{
			return levelindex;
		}
		set
		{
			levelindex = value;
		}
	}

	public string Levelname
	{
		get
		{
			return levelname;
		}
		set
		{
			levelname = value;
		}
	}

	public MissionType MISSIONTYPE
	{
		get
		{
			return missiontype;
		}
		set
		{
			missiontype = value;
		}
	}

	public int Star1
	{
		get
		{
			return star1;
		}
		set
		{
			star1 = value;
		}
	}

	public int Star2
	{
		get
		{
			return star2;
		}
		set
		{
			star2 = value;
		}
	}

	public int Star3
	{
		get
		{
			return star3;
		}
		set
		{
			star3 = value;
		}
	}

	public string Missionid
	{
		get
		{
			return missionid;
		}
		set
		{
			missionid = value;
		}
	}

	public string Background
	{
		get
		{
			return background;
		}
		set
		{
			background = value;
		}
	}

	public string Worldskin
	{
		get
		{
			return worldskin;
		}
		set
		{
			worldskin = value;
		}
	}

	public int Camerasize
	{
		get
		{
			return camerasize;
		}
		set
		{
			camerasize = value;
		}
	}

	public int Worldspeed
	{
		get
		{
			return worldspeed;
		}
		set
		{
			worldspeed = value;
		}
	}
}
