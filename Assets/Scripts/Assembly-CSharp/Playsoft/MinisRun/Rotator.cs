using UnityEngine;

namespace Playsoft.MinisRun
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField]
		private float animSpeed;

		[SerializeField]
		private Vector3 direction;

		[SerializeField]
		private bool useTimeScale = true;

		private void Update()
		{
			float num = ((!useTimeScale) ? Time.unscaledDeltaTime : Time.deltaTime);
			base.transform.Rotate(direction * num * animSpeed);
		}
	}
}
