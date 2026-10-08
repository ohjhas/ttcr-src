using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class PlayParticleRandomly : MonoBehaviour
	{
		[SerializeField]
		private CanvasParticleEmitter[] particles;

		[SerializeField]
		private float delayTime;

		private float lastTime;

		private void Update()
		{
			if (Time.realtimeSinceStartup - lastTime > delayTime)
			{
				lastTime = Time.realtimeSinceStartup;
				int num = Random.Range(0, particles.Length);
				particles[num].StartEmitter();
			}
		}
	}
}
