using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	[RequireComponent(typeof(CanvasParticleEmitter))]
	public class StaminaSingleBarBehaviour : MonoBehaviour
	{
		[SerializeField]
		private Image _filling;

		[SerializeField]
		private CanvasParticleEmitter _particles;

		[SerializeField]
		private Transform _stamina0;

		[SerializeField]
		private Transform _stamina100;

		[SerializeField]
		private float _particlesBorderLow;

		[SerializeField]
		private float _particlesBorderHigh;

		[SerializeField]
		private float _maxHalfSizeY = 35f;

		[SerializeField]
		private float _minHalfSizeY = 15f;

		private void Start()
		{
			_particles.enabled = false;
			_particles.DeleteAllParticles();
		}

		private void FixedUpdate()
		{
			if (ShouldStopExecution())
			{
				_particles.enabled = false;
				return;
			}
			UpdateParticleEmitterPosition(_filling.fillAmount);
			UpdateParticleEmitterSizeX(_filling.fillAmount);
		}

		private bool ShouldStopExecution()
		{
			return Contexts.sharedInstance.game.playerEntity == null;
		}

		private void UpdateParticleEmitterPosition(float ratio)
		{
			float x = Mathf.Lerp(_stamina0.position.x, _stamina100.position.x, ratio);
			_particles.EmiterRate = ((ratio > 0f && ratio < 1f) ? 5 : 0);
			_particles.enabled = _particles.EmiterRate > 0f || _particles.NumberOfParticles > 0;
			_particles.rectTransform.position = new Vector3(x, _particles.rectTransform.position.y, _particles.rectTransform.position.z);
			_particles.rectTransform.offsetMin = new Vector2(_particles.rectTransform.offsetMin.x, 0f);
			_particles.rectTransform.offsetMax = new Vector2(_particles.rectTransform.offsetMax.x, 0f);
		}

		private void UpdateParticleEmitterSizeX(float ratio)
		{
			float t = 1f;
			if (ratio >= _particlesBorderHigh)
			{
				t = Mathf.InverseLerp(1f, _particlesBorderHigh, ratio);
			}
			else if (ratio <= _particlesBorderLow)
			{
				t = Mathf.InverseLerp(0f, _particlesBorderLow, ratio);
			}
			float num = Mathf.Lerp(_minHalfSizeY, _maxHalfSizeY, t);
			_particles.EmitterShapeLinePointA = new Vector2(0f, 0f - num);
			_particles.EmitterShapeLinePointB = new Vector2(0f, num);
		}
	}
}
