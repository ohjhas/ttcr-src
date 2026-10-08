using System;
using UnityEngine;

[Serializable]
public class ChaptersBalancingData
{
	[SerializeField]
	private int chapterid;

	[SerializeField]
	private string chaptername;

	[SerializeField]
	private int requiredstars;

	[SerializeField]
	private string assetname;

	public int Chapterid
	{
		get
		{
			return chapterid;
		}
		set
		{
			chapterid = value;
		}
	}

	public string Chaptername
	{
		get
		{
			return chaptername;
		}
		set
		{
			chaptername = value;
		}
	}

	public int Requiredstars
	{
		get
		{
			return requiredstars;
		}
		set
		{
			requiredstars = value;
		}
	}

	public string Assetname
	{
		get
		{
			return assetname;
		}
		set
		{
			assetname = value;
		}
	}
}
