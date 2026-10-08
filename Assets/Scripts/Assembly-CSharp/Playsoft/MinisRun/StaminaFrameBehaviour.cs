using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Playsoft.MinisRun
{
	public class StaminaFrameBehaviour : MonoBehaviour
	{
		[SerializeField]
		private float _animationTime = 0.2f;

		[SerializeField]
		private Material _highlightMaterial;

		private Image _frame;

		private Material _normalMaterial;

		private float _previousPlayerValue;

		private Coroutine _corr;

		private void Awake()
		{
			_frame = GetComponent<Image>();
		}

		private void FixedUpdate()
		{
			GameEntity playerEntity = Contexts.sharedInstance.game.playerEntity;
			if (playerEntity != null)
			{
				float num = _previousPlayerValue - playerEntity.stamina.Value;
				StaminaSettingsBalancingDataComponent staminaSettingsBalancingData = Contexts.sharedInstance.state.balancingDataEntity.staminaSettingsBalancingData;
				float num2 = staminaSettingsBalancingData.Map[StaminaSettings.StaminaDecreasingFactor];
				if (num > num2 && _corr == null)
				{
					_corr = StartCoroutine(Highlight());
				}
				_previousPlayerValue = playerEntity.stamina.Value;
			}
		}

		private IEnumerator Highlight()
		{
			_normalMaterial = _frame.material;
			_frame.material = _highlightMaterial;
			float time = 0f;
			while (time < _animationTime)
			{
				time += Time.deltaTime;
				_frame.material.SetFloat("UnscaledTime", 0.5f * Mathf.Cos(time / _animationTime * 3.1415f / 2f));
				yield return null;
			}
			_frame.material = _normalMaterial;
			_corr = null;
		}
	}
}
