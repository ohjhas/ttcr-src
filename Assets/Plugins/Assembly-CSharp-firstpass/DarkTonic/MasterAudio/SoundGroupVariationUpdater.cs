using UnityEngine;

namespace DarkTonic.MasterAudio
{
	[AudioScriptOrder(-15)]
	public class SoundGroupVariationUpdater : MonoBehaviour
	{
		private enum WaitForSoundFinishMode
		{
			None = 0,
			Delay = 1,
			Play = 2,
			WaitForEnd = 3,
			StopOrRepeat = 4,
			FxTailWait = 5
		}

		private const float FakeNegativeFloatValue = -10f;

		private Transform _objectToFollow;

		private GameObject _objectToFollowGo;

		private bool _isFollowing;

		private SoundGroupVariation _variation;

		private float _priorityLastUpdated = -10f;

		private bool _useClipAgePriority;

		private WaitForSoundFinishMode _waitMode;

		private float _soundPlayTime;

		private AudioSource _varAudio;

		private MasterAudioGroup _parentGrp;

		private Transform _trans;

		private int _frameNum = -1;

		private float _fadeOutStartTime = -5f;

		private bool _fadeInOutWillFadeOut;

		private bool _hasFadeInOutSetMaxVolume;

		private float _fadeInOutInFactor;

		private float _fadeInOutOutFactor;

		private int _fadeOutEarlyTotalFrames;

		private float _fadeOutEarlyFrameVolChange;

		private int _fadeOutEarlyFrameNumber;

		private float _fadeOutEarlyOrigVol;

		private float _fadeToTargetFrameVolChange;

		private int _fadeToTargetFrameNumber;

		private float _fadeToTargetOrigVol;

		private int _fadeToTargetTotalFrames;

		private float _fadeToTargetVolume;

		private bool _fadeOutStarted;

		private float _lastFrameClipTime = -1f;

		private float _fxTailEndTime = -1f;

		private bool _isPlayingBackward;

		private bool _hasStartedNextInChain;

		private bool _isWaitingForQueuedOcclusionRay;

		private int _framesPlayed;

		private static int _maCachedFromFrame = -1;

		private static MasterAudio _maThisFrame;

		private static Transform _listenerThisFrame;

		public int FramesPlayed
		{
			get
			{
				return _framesPlayed;
			}
		}

		public MasterAudio MAThisFrame
		{
			get
			{
				return _maThisFrame;
			}
		}

		public float MaxOcclusionFreq
		{
			get
			{
				if (GrpVariation.UsesOcclusion && ParentGroup.willOcclusionOverrideFrequencies)
				{
					return ParentGroup.occlusionMaxCutoffFreq;
				}
				return _maThisFrame.occlusionMaxCutoffFreq;
			}
		}

		public float MinOcclusionFreq
		{
			get
			{
				if (GrpVariation.UsesOcclusion && ParentGroup.willOcclusionOverrideFrequencies)
				{
					return ParentGroup.occlusionMinCutoffFreq;
				}
				return _maThisFrame.occlusionMinCutoffFreq;
			}
		}

		private Transform Trans
		{
			get
			{
				if (_trans != null)
				{
					return _trans;
				}
				_trans = GrpVariation.Trans;
				return _trans;
			}
		}

		private AudioSource VarAudio
		{
			get
			{
				if (_varAudio != null)
				{
					return _varAudio;
				}
				_varAudio = GrpVariation.VarAudio;
				return _varAudio;
			}
		}

		private MasterAudioGroup ParentGroup
		{
			get
			{
				if (_parentGrp != null)
				{
					return _parentGrp;
				}
				_parentGrp = GrpVariation.ParentGroup;
				return _parentGrp;
			}
		}

		private SoundGroupVariation GrpVariation
		{
			get
			{
				if (_variation != null)
				{
					return _variation;
				}
				_variation = GetComponent<SoundGroupVariation>();
				return _variation;
			}
		}

		private float RayCastOriginOffset
		{
			get
			{
				if (GrpVariation.UsesOcclusion && ParentGroup.willOcclusionOverrideRaycastOffset)
				{
					return ParentGroup.occlusionRayCastOffset;
				}
				return _maThisFrame.occlusionRayCastOffset;
			}
		}

		private bool IsOcclusionMeasuringPaused
		{
			get
			{
				return _isWaitingForQueuedOcclusionRay || MasterAudio.IsOcclusionFreqencyTransitioning(GrpVariation);
			}
		}

		public void FadeOverTimeToVolume(float targetVolume, float fadeTime)
		{
			GrpVariation.curFadeMode = SoundGroupVariation.FadeMode.GradualFade;
			float num = targetVolume - VarAudio.volume;
			float time = VarAudio.time;
			float length = VarAudio.clip.length;
			if (!VarAudio.loop && VarAudio.clip != null && fadeTime + time > length)
			{
				fadeTime = length - time;
			}
			_fadeToTargetTotalFrames = (int)(fadeTime / AudioUtil.FrameTime);
			_fadeToTargetFrameVolChange = num / (float)_fadeToTargetTotalFrames;
			_fadeToTargetFrameNumber = 0;
			_fadeToTargetOrigVol = VarAudio.volume;
			_fadeToTargetVolume = targetVolume;
		}

		public void FadeOutEarly(float fadeTime)
		{
			GrpVariation.curFadeMode = SoundGroupVariation.FadeMode.FadeOutEarly;
			if (!VarAudio.loop && VarAudio.clip != null && VarAudio.time + fadeTime > VarAudio.clip.length)
			{
				fadeTime = VarAudio.clip.length - VarAudio.time;
			}
			float num = AudioUtil.FrameTime;
			if (num == 0f)
			{
				num = AudioUtil.FixedDeltaTime;
			}
			_fadeOutEarlyTotalFrames = (int)(fadeTime / num);
			_fadeOutEarlyFrameVolChange = (0f - VarAudio.volume) / (float)_fadeOutEarlyTotalFrames;
			_fadeOutEarlyFrameNumber = 0;
			_fadeOutEarlyOrigVol = VarAudio.volume;
		}

		public void FadeInOut()
		{
			GrpVariation.curFadeMode = SoundGroupVariation.FadeMode.FadeInOut;
			_fadeOutStartTime = VarAudio.clip.length - GrpVariation.fadeOutTime * VarAudio.pitch;
			if (GrpVariation.fadeInTime > 0f)
			{
				VarAudio.volume = 0f;
				_fadeInOutInFactor = GrpVariation.fadeMaxVolume / GrpVariation.fadeInTime;
			}
			else
			{
				_fadeInOutInFactor = 0f;
			}
			_fadeInOutWillFadeOut = GrpVariation.fadeOutTime > 0f && !VarAudio.loop;
			if (_fadeInOutWillFadeOut)
			{
				_fadeInOutOutFactor = GrpVariation.fadeMaxVolume / (VarAudio.clip.length - _fadeOutStartTime);
			}
			else
			{
				_fadeInOutOutFactor = 0f;
			}
		}

		public void FollowObject(bool follow, Transform objToFollow, bool clipAgePriority)
		{
			_isFollowing = follow;
			if (objToFollow != null)
			{
				_objectToFollow = objToFollow;
				_objectToFollowGo = objToFollow.gameObject;
			}
			_useClipAgePriority = clipAgePriority;
			UpdateCachedObjects();
			UpdateAudioLocationAndPriority(false);
		}

		public void WaitForSoundFinish(float delaySound)
		{
			if (MasterAudio.IsWarming)
			{
				PlaySoundAndWait();
				return;
			}
			_waitMode = WaitForSoundFinishMode.Delay;
			float num = 0f;
			if (GrpVariation.useIntroSilence && GrpVariation.introSilenceMax > 0f)
			{
				float num2 = Random.Range(GrpVariation.introSilenceMin, GrpVariation.introSilenceMax);
				num += num2;
			}
			if (delaySound > 0f)
			{
				num += delaySound;
			}
			if (num == 0f)
			{
				_waitMode = WaitForSoundFinishMode.Play;
				return;
			}
			_soundPlayTime = AudioUtil.Time + num;
			GrpVariation.IsWaitingForDelay = true;
		}

		public void StopFading()
		{
			GrpVariation.curFadeMode = SoundGroupVariation.FadeMode.None;
			DisableIfFinished();
		}

		public void StopWaitingForFinish()
		{
			_waitMode = WaitForSoundFinishMode.None;
			GrpVariation.curDetectEndMode = SoundGroupVariation.DetectEndMode.None;
			DisableIfFinished();
		}

		public void StopFollowing()
		{
			_isFollowing = false;
			_useClipAgePriority = false;
			_objectToFollow = null;
			_objectToFollowGo = null;
			DisableIfFinished();
		}

		private void DisableIfFinished()
		{
			if (!_isFollowing && GrpVariation.curDetectEndMode != SoundGroupVariation.DetectEndMode.DetectEnd && GrpVariation.curFadeMode == SoundGroupVariation.FadeMode.None)
			{
				base.enabled = false;
			}
		}

		private void UpdateAudioLocationAndPriority(bool rePrioritize)
		{
			if (_isFollowing && _objectToFollow != null)
			{
				Trans.position = _objectToFollow.position;
			}
			if (_maThisFrame.prioritizeOnDistance && rePrioritize && !ParentGroup.alwaysHighestPriority && !(Time.realtimeSinceStartup - _priorityLastUpdated <= MasterAudio.ReprioritizeTime))
			{
				AudioPrioritizer.Set3DPriority(GrpVariation, _useClipAgePriority);
				_priorityLastUpdated = AudioUtil.Time;
			}
		}

		private void ResetToNonOcclusionSetting()
		{
			AudioLowPassFilter lowPassFilter = GrpVariation.LowPassFilter;
			if (lowPassFilter != null)
			{
				lowPassFilter.cutoffFrequency = 22000f;
			}
		}

		private void UpdateOcclusion()
		{
			if (!GrpVariation.UsesOcclusion)
			{
				MasterAudio.StopTrackingOcclusionForSource(GrpVariation.GameObj);
				ResetToNonOcclusionSetting();
			}
			else if (!(_listenerThisFrame == null) && !IsOcclusionMeasuringPaused)
			{
				MasterAudio.AddToQueuedOcclusionRays(this);
				_isWaitingForQueuedOcclusionRay = true;
			}
		}

		private void DoneWithOcclusion()
		{
			_isWaitingForQueuedOcclusionRay = false;
			MasterAudio.RemoveFromOcclusionFrequencyTransitioning(GrpVariation);
		}

		public bool RayCastForOcclusion()
		{
			DoneWithOcclusion();
			Vector3 vector = Trans.position;
			float rayCastOriginOffset = RayCastOriginOffset;
			if (rayCastOriginOffset > 0f)
			{
				vector = Vector3.MoveTowards(vector, _listenerThisFrame.position, rayCastOriginOffset);
			}
			Vector3 vector2 = _listenerThisFrame.position - vector;
			float magnitude = vector2.magnitude;
			if (magnitude > VarAudio.maxDistance)
			{
				MasterAudio.AddToOcclusionOutOfRangeSources(GrpVariation.GameObj);
				ResetToNonOcclusionSetting();
				return false;
			}
			MasterAudio.AddToOcclusionInRangeSources(GrpVariation.GameObj);
			bool flag = _maThisFrame.occlusionRaycastMode == MasterAudio.RaycastMode.Physics2D;
			if (GrpVariation.LowPassFilter == null)
			{
				AudioLowPassFilter lowPassFilter = GrpVariation.gameObject.AddComponent<AudioLowPassFilter>();
				GrpVariation.LowPassFilter = lowPassFilter;
			}
			bool queriesStartInColliders = Physics2D.queriesStartInColliders;
			if (flag)
			{
				Physics2D.queriesStartInColliders = _maThisFrame.occlusionIncludeStartRaycast2DCollider;
			}
			bool flag2 = true;
			flag2 = ((!flag) ? Physics.queriesHitTriggers : Physics2D.queriesHitTriggers);
			Vector3 vector3 = Vector3.zero;
			float? num = null;
			bool flag3 = false;
			if (_maThisFrame.occlusionUseLayerMask)
			{
				switch (_maThisFrame.occlusionRaycastMode)
				{
				case MasterAudio.RaycastMode.Physics3D:
				{
					RaycastHit hitInfo;
					if (Physics.Raycast(vector, vector2, out hitInfo, magnitude, _maThisFrame.occlusionLayerMask.value))
					{
						flag3 = true;
						vector3 = hitInfo.point;
						num = hitInfo.distance;
					}
					break;
				}
				case MasterAudio.RaycastMode.Physics2D:
				{
					RaycastHit2D raycastHit2D = Physics2D.Raycast(vector, vector2, magnitude, _maThisFrame.occlusionLayerMask.value);
					if (raycastHit2D.transform != null)
					{
						flag3 = true;
						vector3 = raycastHit2D.point;
						num = raycastHit2D.distance;
					}
					break;
				}
				}
			}
			else
			{
				switch (_maThisFrame.occlusionRaycastMode)
				{
				case MasterAudio.RaycastMode.Physics3D:
				{
					RaycastHit hitInfo2;
					if (Physics.Raycast(vector, vector2, out hitInfo2, magnitude))
					{
						flag3 = true;
						vector3 = hitInfo2.point;
						num = hitInfo2.distance;
					}
					break;
				}
				case MasterAudio.RaycastMode.Physics2D:
				{
					RaycastHit2D raycastHit2D2 = Physics2D.Raycast(vector, vector2, magnitude);
					if (raycastHit2D2.transform != null)
					{
						flag3 = true;
						vector3 = raycastHit2D2.point;
						num = raycastHit2D2.distance;
					}
					break;
				}
				}
			}
			if (flag)
			{
				Physics2D.queriesStartInColliders = queriesStartInColliders;
				Physics2D.queriesHitTriggers = flag2;
			}
			else
			{
				Physics.queriesHitTriggers = flag2;
			}
			if (_maThisFrame.occlusionShowRaycasts)
			{
				Vector3 end = ((!flag3) ? _listenerThisFrame.position : vector3);
				Color color = ((!flag3) ? Color.green : Color.red);
				Debug.DrawLine(vector, end, color, 0.1f);
			}
			if (!flag3)
			{
				MasterAudio.RemoveFromBlockedOcclusionSources(GrpVariation.GameObj);
				ResetToNonOcclusionSetting();
				return true;
			}
			MasterAudio.AddToBlockedOcclusionSources(GrpVariation.GameObj);
			float distRatio = num.Value / VarAudio.maxDistance;
			float occlusionCutoffFrequencyByDistanceRatio = AudioUtil.GetOcclusionCutoffFrequencyByDistanceRatio(distRatio, this);
			float occlusionFreqChangeSeconds = _maThisFrame.occlusionFreqChangeSeconds;
			if (occlusionFreqChangeSeconds <= 0.1f)
			{
				GrpVariation.LowPassFilter.cutoffFrequency = occlusionCutoffFrequencyByDistanceRatio;
				return true;
			}
			MasterAudio.GradualOcclusionFreqChange(GrpVariation, occlusionFreqChangeSeconds, occlusionCutoffFrequencyByDistanceRatio);
			return true;
		}

		private void PlaySoundAndWait()
		{
			GrpVariation.IsWaitingForDelay = false;
			if (!(VarAudio.clip == null))
			{
				VarAudio.Play();
				AudioUtil.ClipPlayed(VarAudio.clip, GrpVariation.GameObj);
				if (GrpVariation.useRandomStartTime)
				{
					float time = Random.Range(GrpVariation.randomStartMinPercent, GrpVariation.randomStartMaxPercent) * 0.01f * VarAudio.clip.length;
					VarAudio.time = time;
				}
				GrpVariation.LastTimePlayed = AudioUtil.Time;
				MasterAudio.DuckSoundGroup(ParentGroup.GameObjectName, VarAudio);
				_isPlayingBackward = GrpVariation.OriginalPitch < 0f;
				_lastFrameClipTime = ((!_isPlayingBackward) ? (-1f) : (VarAudio.clip.length + 1f));
				_waitMode = WaitForSoundFinishMode.WaitForEnd;
			}
		}

		private void StopOrChain()
		{
			SoundGroupVariation.PlaySoundParams playSoundParm = GrpVariation.PlaySoundParm;
			bool flag = playSoundParm.IsPlaying && playSoundParm.IsChainLoop;
			if (!VarAudio.loop || flag)
			{
				GrpVariation.Stop();
			}
			if (flag)
			{
				StopWaitingForFinish();
				MaybeChain();
			}
		}

		public void MaybeChain()
		{
			if (_hasStartedNextInChain)
			{
				return;
			}
			_hasStartedNextInChain = true;
			SoundGroupVariation.PlaySoundParams playSoundParm = GrpVariation.PlaySoundParm;
			int num = MasterAudio.RemainingClipsInGroup(ParentGroup.GameObjectName);
			int num2 = MasterAudio.VoicesForGroup(ParentGroup.GameObjectName);
			if (num == num2)
			{
				ParentGroup.FireLastVariationFinishedPlay();
			}
			if (ParentGroup.chainLoopMode == MasterAudioGroup.ChainedLoopLoopMode.NumberOfLoops && ParentGroup.ChainLoopCount >= ParentGroup.chainLoopNumLoops)
			{
				return;
			}
			float delaySoundTime = playSoundParm.DelaySoundTime;
			if (ParentGroup.chainLoopDelayMin > 0f || ParentGroup.chainLoopDelayMax > 0f)
			{
				delaySoundTime = Random.Range(ParentGroup.chainLoopDelayMin, ParentGroup.chainLoopDelayMax);
			}
			if (playSoundParm.AttachToSource || playSoundParm.SourceTrans != null)
			{
				if (playSoundParm.AttachToSource)
				{
					MasterAudio.PlaySound3DFollowTransform(playSoundParm.SoundType, playSoundParm.SourceTrans, playSoundParm.VolumePercentage, playSoundParm.Pitch, delaySoundTime, null, true);
				}
				else
				{
					MasterAudio.PlaySound3DAtTransform(playSoundParm.SoundType, playSoundParm.SourceTrans, playSoundParm.VolumePercentage, playSoundParm.Pitch, delaySoundTime, null, true);
				}
			}
			else
			{
				MasterAudio.PlaySound(playSoundParm.SoundType, playSoundParm.VolumePercentage, playSoundParm.Pitch, delaySoundTime, null, true);
			}
		}

		private void PerformFading()
		{
			switch (GrpVariation.curFadeMode)
			{
			case SoundGroupVariation.FadeMode.None:
				break;
			case SoundGroupVariation.FadeMode.FadeInOut:
			{
				if (!VarAudio.isPlaying)
				{
					break;
				}
				float time = VarAudio.time;
				if (GrpVariation.fadeInTime > 0f && time < GrpVariation.fadeInTime)
				{
					VarAudio.volume = time * _fadeInOutInFactor;
				}
				else if (time >= GrpVariation.fadeInTime && !_hasFadeInOutSetMaxVolume)
				{
					VarAudio.volume = GrpVariation.fadeMaxVolume;
					_hasFadeInOutSetMaxVolume = true;
					if (!_fadeInOutWillFadeOut)
					{
						StopFading();
					}
				}
				else if (_fadeInOutWillFadeOut && time >= _fadeOutStartTime)
				{
					if (GrpVariation.PlaySoundParm.IsChainLoop && !_fadeOutStarted)
					{
						MaybeChain();
						_fadeOutStarted = true;
					}
					VarAudio.volume = (VarAudio.clip.length - time) * _fadeInOutOutFactor;
				}
				break;
			}
			case SoundGroupVariation.FadeMode.FadeOutEarly:
				if (VarAudio.isPlaying)
				{
					_fadeOutEarlyFrameNumber++;
					VarAudio.volume = (float)_fadeOutEarlyFrameNumber * _fadeOutEarlyFrameVolChange + _fadeOutEarlyOrigVol;
					if (_fadeOutEarlyFrameNumber >= _fadeOutEarlyTotalFrames)
					{
						GrpVariation.curFadeMode = SoundGroupVariation.FadeMode.None;
						GrpVariation.Stop();
					}
				}
				break;
			case SoundGroupVariation.FadeMode.GradualFade:
				if (VarAudio.isPlaying)
				{
					_fadeToTargetFrameNumber++;
					if (_fadeToTargetFrameNumber >= _fadeToTargetTotalFrames)
					{
						VarAudio.volume = _fadeToTargetVolume;
						StopFading();
					}
					else
					{
						VarAudio.volume = (float)_fadeToTargetFrameNumber * _fadeToTargetFrameVolChange + _fadeToTargetOrigVol;
					}
				}
				break;
			}
		}

		private void OnEnable()
		{
			_fadeInOutWillFadeOut = false;
			_hasFadeInOutSetMaxVolume = false;
			_fadeOutStarted = false;
			_hasStartedNextInChain = false;
			_framesPlayed = 0;
			DoneWithOcclusion();
			MasterAudio.RegisterUpdaterForUpdates(this);
		}

		private void OnDisable()
		{
			if (!MasterAudio.AppIsShuttingDown)
			{
				_framesPlayed = 0;
				DoneWithOcclusion();
				MasterAudio.UnregisterUpdaterForUpdates(this);
			}
		}

		public void UpdateCachedObjects()
		{
			_frameNum = AudioUtil.FrameCount;
			if (_maCachedFromFrame < _frameNum)
			{
				_maCachedFromFrame = _frameNum;
				_maThisFrame = MasterAudio.Instance;
				_listenerThisFrame = MasterAudio.ListenerTrans;
			}
		}

		public void ManualUpdate()
		{
			UpdateCachedObjects();
			_framesPlayed++;
			if (_isFollowing && ParentGroup.targetDespawnedBehavior != MasterAudioGroup.TargetDespawnedBehavior.None && (_objectToFollowGo == null || !DTMonoHelper.IsActive(_objectToFollowGo)))
			{
				switch (ParentGroup.targetDespawnedBehavior)
				{
				case MasterAudioGroup.TargetDespawnedBehavior.Stop:
					GrpVariation.Stop();
					break;
				case MasterAudioGroup.TargetDespawnedBehavior.FadeOut:
					GrpVariation.FadeOutNow(ParentGroup.despawnFadeTime);
					break;
				}
				StopFollowing();
			}
			PerformFading();
			UpdateAudioLocationAndPriority(true);
			UpdateOcclusion();
			switch (_waitMode)
			{
			case WaitForSoundFinishMode.None:
				break;
			case WaitForSoundFinishMode.Delay:
				if (Time.realtimeSinceStartup >= _soundPlayTime)
				{
					_waitMode = WaitForSoundFinishMode.Play;
				}
				break;
			case WaitForSoundFinishMode.Play:
				PlaySoundAndWait();
				break;
			case WaitForSoundFinishMode.WaitForEnd:
			{
				bool flag = false;
				if (_isPlayingBackward)
				{
					if (VarAudio.time > _lastFrameClipTime)
					{
						flag = true;
					}
				}
				else if (VarAudio.time < _lastFrameClipTime)
				{
					flag = true;
				}
				_lastFrameClipTime = VarAudio.time;
				if (flag)
				{
					if (GrpVariation.fxTailTime > 0f)
					{
						_waitMode = WaitForSoundFinishMode.FxTailWait;
						_fxTailEndTime = AudioUtil.Time + GrpVariation.fxTailTime;
					}
					else
					{
						_waitMode = WaitForSoundFinishMode.StopOrRepeat;
					}
				}
				break;
			}
			case WaitForSoundFinishMode.FxTailWait:
				if (Time.realtimeSinceStartup >= _fxTailEndTime)
				{
					_waitMode = WaitForSoundFinishMode.StopOrRepeat;
				}
				break;
			case WaitForSoundFinishMode.StopOrRepeat:
				StopOrChain();
				break;
			}
		}
	}
}
