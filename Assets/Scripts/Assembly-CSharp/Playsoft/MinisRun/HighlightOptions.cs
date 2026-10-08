using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class HighlightOptions
	{
		public bool enable;

		public float delay;

		public Material higlightMaterial;

		public float minScale = 1f;

		public float maxScale = 1.05f;

		public float animSpeed = 1f;
	}
}
