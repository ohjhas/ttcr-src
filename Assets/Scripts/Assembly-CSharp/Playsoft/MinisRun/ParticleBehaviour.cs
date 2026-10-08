using UnityEngine;

namespace Playsoft.MinisRun
{
	public class ParticleBehaviour : MonoBehaviour, IPoolSpawnedListener
	{
		public void OnSpawned(GameEntity e)
		{
			if (Contexts.sharedInstance.game.playerEntity.hasUpgradeScore)
			{
				ParticleSystem componentInChildren = GetComponentInChildren<ParticleSystem>();
				if (componentInChildren != null)
				{
					componentInChildren.Stop();
					componentInChildren.Play();
				}
			}
		}
	}
}
