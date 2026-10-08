using UnityEngine;
using UnityEngine.Events;

namespace Playsoft.MinisRun
{
	public class CounterEffect : MonoBehaviour
	{
		public enum EffectVariant
		{
			FixedRate = 0,
			FixedDuration = 1
		}

		[SerializeField]
		private int currentCount;

		[SerializeField]
		private float duration = 1f;

		[SerializeField]
		private bool ignoreTimeScale = true;

		private int lastMaxCount;

		private int lastTargetCount;

		private float lastUpdateTime;

		private float lastRate;

		private float delay;

		[SerializeField]
		private int maxCount;

		[SerializeField]
		private int targetCount;

		[SerializeField]
		private float rate = 1f;

		[SerializeField]
		private EffectVariant variant;

		[SerializeField]
		private UnityEvent onStart;

		[SerializeField]
		private UnityEvent onChange;

		[SerializeField]
		private UnityEvent onComplete;

		public static CounterEffect Current { get; private set; }

		public int CurrentCount
		{
			get
			{
				return currentCount;
			}
			set
			{
				if (value != currentCount)
				{
					currentCount = value;
					Changing();
				}
			}
		}

		public int MaxCount
		{
			get
			{
				return maxCount;
			}
			set
			{
				if (value != maxCount)
				{
					maxCount = value;
					Changing();
				}
			}
		}

		public int TargetCount
		{
			get
			{
				return targetCount;
			}
			set
			{
				if (value != targetCount)
				{
					targetCount = value;
					Changing();
				}
			}
		}

		public UnityEvent OnStart
		{
			get
			{
				return onStart;
			}
		}

		public UnityEvent OnChange
		{
			get
			{
				return onChange;
			}
		}

		public UnityEvent OnComplete
		{
			get
			{
				return onComplete;
			}
		}

		private float GetRate()
		{
			return (variant != EffectVariant.FixedDuration) ? rate : lastRate;
		}

		private float GetTime()
		{
			return (!ignoreTimeScale) ? Time.time : Time.unscaledTime;
		}

		protected virtual void Starting()
		{
			CounterEffect current = Current;
			Current = this;
			onStart.Invoke();
			Current = current;
		}

		protected virtual void Changing()
		{
			CounterEffect current = Current;
			Current = this;
			onChange.Invoke();
			Current = current;
		}

		protected virtual void Completing()
		{
			CounterEffect current = Current;
			Current = this;
			onComplete.Invoke();
			Current = current;
		}

		protected virtual void Update()
		{
			if (targetCount == currentCount && maxCount == lastMaxCount)
			{
				return;
			}
			if (GetTime() <= lastUpdateTime)
			{
				if (maxCount != lastMaxCount)
				{
					lastMaxCount = maxCount;
					Changing();
				}
				return;
			}
			if (targetCount != lastTargetCount)
			{
				Starting();
				if (variant == EffectVariant.FixedDuration)
				{
					lastRate = (float)Mathf.Abs(targetCount - currentCount) / duration;
				}
				if (currentCount == lastTargetCount)
				{
					lastUpdateTime = GetTime();
				}
				lastTargetCount = targetCount;
			}
			lastMaxCount = maxCount;
			if (delay > 0f)
			{
				float num = GetTime() - lastUpdateTime;
				delay -= num;
				lastUpdateTime = GetTime();
				if (delay > 0f)
				{
					return;
				}
			}
			int num2 = Mathf.FloorToInt((GetTime() - lastUpdateTime) * GetRate()) * ((targetCount >= currentCount) ? 1 : (-1));
			if (Mathf.Abs(num2) > Mathf.Abs(targetCount - currentCount))
			{
				num2 = targetCount - currentCount;
			}
			lastUpdateTime += (float)Mathf.Abs(num2) / GetRate();
			if (num2 != 0)
			{
				currentCount += num2;
				Changing();
				if (targetCount == currentCount)
				{
					Completing();
				}
			}
		}

		protected virtual void Start()
		{
			lastUpdateTime = GetTime();
			Update();
		}

		public void Reset(int value)
		{
			if (value != currentCount || value != maxCount || value != targetCount)
			{
				if (targetCount != currentCount)
				{
					Completing();
				}
				currentCount = value;
				maxCount = value;
				targetCount = value;
				lastUpdateTime = GetTime();
				lastTargetCount = targetCount;
				Changing();
			}
		}

		public void SetDelay(float _delay)
		{
			delay = _delay;
		}
	}
}
