using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[UI]
	public sealed class ScreenMapComponent : IComponent
	{
		public GameObject Root;

		public Dictionary<Type, GameObject> ScreenMap;
	}
}
