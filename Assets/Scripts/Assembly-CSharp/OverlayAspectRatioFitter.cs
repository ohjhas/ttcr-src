using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AspectRatioFitter))]
[ExecuteInEditMode]
public class OverlayAspectRatioFitter : MonoBehaviour
{
	[SerializeField]
	private float _maxAspectRatio = 2f;

	private AspectRatioFitter _aspectRatioFitter;

	private void Start()
	{
		_aspectRatioFitter = GetComponent<AspectRatioFitter>();
		_aspectRatioFitter.aspectMode = AspectRatioFitter.AspectMode.HeightControlsWidth;
		_aspectRatioFitter.aspectRatio = Mathf.Min(_maxAspectRatio, (float)Screen.width / (float)Screen.height);
	}
}
