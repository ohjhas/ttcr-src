using System;

namespace Playsoft.MinisRun
{
	[Serializable]
	public class StaminaProp : AbstractProperty
	{
		public float Stamina;

		public StaminaProp()
		{
		}

		public StaminaProp(StaminaProp stamina)
		{
			IsEnabled = stamina.IsEnabled;
			Stamina = stamina.Stamina;
		}

		public override void AddToEntity(GameEntity entity)
		{
			if (IsEnabled)
			{
				entity.AddCollectibleStamina(Stamina);
			}
		}
	}
}
