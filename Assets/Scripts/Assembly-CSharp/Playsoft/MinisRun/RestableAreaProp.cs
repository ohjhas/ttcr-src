using System;
using UnityEngine;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class RestableAreaProp : AbstractProperty
	{
		public Vector2 MoveVector;

		public RestableAreaProp()
		{
		}

		public RestableAreaProp(RestableAreaProp restable)
		{
			IsEnabled = restable.IsEnabled;
			MoveVector = restable.MoveVector;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddRestableArea(MoveVector, false);
			}
		}
	}
}
