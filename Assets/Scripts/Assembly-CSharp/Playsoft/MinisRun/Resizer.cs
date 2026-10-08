using UnityEngine;

namespace Playsoft.MinisRun
{
	public class Resizer : MonoBehaviour
	{
		[SerializeField]
		private float animSpeed;

		[SerializeField]
		private float animAmplitude;

		[SerializeField]
		private Vector3 direction;

		private Vector3 initScale;

		private void Start()
		{
			initScale = base.transform.localScale;
		}

		private void Update()
		{
			base.transform.localScale = initScale + direction * Mathf.Sin(Time.realtimeSinceStartup * animSpeed) * animAmplitude;
		}
	}
}
