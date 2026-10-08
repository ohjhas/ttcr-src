using System;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class FlyingElementEffectComponent : IComponent
	{
		public GameObject ElementPrefab;

		public RectTransform StartPoint;

		public RectTransform EndPoint;

		public int ElementsAmount;

		public float EffectDuration;

		public Action Callback;
	}
}
