using System;
using UnityEngine;

[Serializable]
public class DailyMissionsBalancingData
{
	[SerializeField]
	private int missionid;

	[SerializeField]
	private DailyMissionGroup dailymissiongroup;

	[SerializeField]
	private DailyMissionType dailymissiontype;

	[SerializeField]
	private string title;

	[SerializeField]
	private int dailymissiongoal;

	[SerializeField]
	private ItemType itemtype;

	[SerializeField]
	private int itemindex;

	[SerializeField]
	private int itemamount;

	public int Missionid
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

	public DailyMissionGroup DAILYMISSIONGROUP
	{
		get
		{
			return dailymissiongroup;
		}
		set
		{
			dailymissiongroup = value;
		}
	}

	public DailyMissionType DAILYMISSIONTYPE
	{
		get
		{
			return dailymissiontype;
		}
		set
		{
			dailymissiontype = value;
		}
	}

	public string Title
	{
		get
		{
			return title;
		}
		set
		{
			title = value;
		}
	}

	public int Dailymissiongoal
	{
		get
		{
			return dailymissiongoal;
		}
		set
		{
			dailymissiongoal = value;
		}
	}

	public ItemType ITEMTYPE
	{
		get
		{
			return itemtype;
		}
		set
		{
			itemtype = value;
		}
	}

	public int Itemindex
	{
		get
		{
			return itemindex;
		}
		set
		{
			itemindex = value;
		}
	}

	public int Itemamount
	{
		get
		{
			return itemamount;
		}
		set
		{
			itemamount = value;
		}
	}
}
