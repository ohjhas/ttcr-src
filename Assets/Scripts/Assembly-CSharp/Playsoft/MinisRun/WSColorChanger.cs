using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class WSColorChanger
	{
		[Serializable]
		public class GraphicObj
		{
			public Graphic Graphic;

			public Color originColor = Color.white;
		}

		[SerializeField]
		private List<GraphicObj> Graphics;

		public void SetColor(Color color)
		{
			for (int i = 0; i < Graphics.Count; i++)
			{
				Graphics[i].Graphic.color = Graphics[i].originColor * color;
			}
		}
	}
}
