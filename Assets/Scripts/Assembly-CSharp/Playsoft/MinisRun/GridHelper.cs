using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playsoft.MinisRun
{
	public class GridHelper : MonoBehaviour
	{
		[Serializable]
		private class LayerParams
		{
			public string Name;

			public float Height;

			public Color Color = Color.white;
		}

		[Header("Parameters")]
		[SerializeField]
		public bool IsEnabled = true;

		[SerializeField]
		public int levelLength = 100;

		[SerializeField]
		private List<LayerParams> Layers;

		private void OnDrawGizmos()
		{
			if (IsEnabled)
			{
				Color color = Gizmos.color;
				DrawLayers();
				Gizmos.color = color;
			}
		}

		private void DrawLayers()
		{
			for (int i = 0; i < Layers.Count; i++)
			{
				Gizmos.color = Layers[i].Color;
				Gizmos.DrawLine(base.transform.position + new Vector3(0f, Layers[i].Height, 0f), base.transform.position + new Vector3(levelLength, Layers[i].Height, 0f));
			}
		}
	}
}
