using UnityEngine;

namespace Playsoft.MinisRun
{
	[RequireComponent(typeof(RectTransform))]
	public class Positioner : MonoBehaviour
	{
		[SerializeField]
		private float animSpeed;

		[SerializeField]
		private float animAmplitude;

		[SerializeField]
		private Vector2 direction;

		private RectTransform RT;

		private Vector2 initPos;

		private void Awake()
		{
			RT = GetComponent<RectTransform>();
			initPos = RT.anchoredPosition;
		}

		private void Update()
		{
			RT.anchoredPosition = initPos + direction * GetAnimFactor() * animAmplitude;
		}

		private float GetAnimFactor()
		{
			return Mathf.Sin(Time.realtimeSinceStartup * animSpeed) * 0.5f + 0.5f;
		}
	}
}
