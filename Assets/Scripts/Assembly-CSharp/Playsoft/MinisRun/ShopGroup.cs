using UnityEngine;

namespace Playsoft.MinisRun
{
	public abstract class ShopGroup : MonoBehaviour
	{
		public abstract ShopGroupType GroupType { get; }

		public virtual void SetData(UIEntity parameters)
		{
		}

		public virtual void AdjustBasedOnAspect(bool isLowAspect)
		{
		}
	}
}
