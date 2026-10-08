using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	[AddComponentMenu("UI/Canvas Particle Emitter", 16)]
	public class CanvasParticleEmitter : MaskableGraphic, ISerializationCallbackReceiver
	{
		public enum EmitterShapes
		{
			Point = 0,
			Directional = 1,
			RectEmitter = 2,
			CircleEmitter = 3,
			LineEmitter = 4
		}

		public enum EmitterShapeLineTypes
		{
			Above = 0,
			Positive = 1,
			Negative = 2,
			Both = 3
		}

		public enum EmitterShapeRectTypes
		{
			Area = 0,
			Edges = 1,
			Vertices = 2
		}

		public enum EmitterShapeCircleTypes
		{
			Edge = 0,
			Vertices = 1
		}

		[SerializeField]
		private bool m_UseScaledTime = true;

		[SerializeField]
		private bool m_playOnAwake = true;

		[SerializeField]
		private bool m_isActive = true;

		[SerializeField]
		private bool m_isPlaying = true;

		[SerializeField]
		private float m_Duration;

		[SerializeField]
		private float m_elapsedEmitterTime;

		[SerializeField]
		private float m_editorEmitterTime;

		[SerializeField]
		private int m_MaxParticles;

		[SerializeField]
		private int m_numberOfParticles;

		[SerializeField]
		private int m_ParticlesPerEmission = 10;

		[SerializeField]
		private float m_EmitterRate = 10f;

		[Range(0f, 360f)]
		[SerializeField]
		private float m_EmitterSpread = 20f;

		[Range(0f, 360f)]
		[SerializeField]
		private float m_EmitterDirection;

		[SerializeField]
		private Vector2 m_Gravity = Vector2.zero;

		[SerializeField]
		private Vector2 m_Wave = new Vector2(100f, 100f);

		[SerializeField]
		private Vector2 m_WaveAmplitude = Vector2.zero;

		[SerializeField]
		private Vector2 m_Turbulence = new Vector2(100f, 100f);

		[SerializeField]
		private Vector2 m_TurbulenceAmplitude = Vector2.zero;

		[SerializeField]
		private EmitterShapes m_EmitterShape;

		[SerializeField]
		private EmitterShapeLineTypes m_EmitterShapeLineType;

		[SerializeField]
		private Vector2 m_EmitterShapeLinePointA;

		[SerializeField]
		private Vector2 m_EmitterShapeLinePointB;

		[SerializeField]
		private float m_EmitterShapeLineLength;

		[SerializeField]
		private float m_EmitterShapeLineAngle;

		[SerializeField]
		private EmitterShapeRectTypes m_EmitterShapeRectType;

		[SerializeField]
		private Rect m_EmitterShapeRect = new Rect(0f, 0f, 100f, 100f);

		[SerializeField]
		private RectTransform m_EmitterShapeRectTransform;

		[SerializeField]
		private EmitterShapeCircleTypes m_EmitterShapeCircleType;

		[SerializeField]
		private float m_EmitterShapeCircleRadius;

		[Range(3f, 100f)]
		[SerializeField]
		private int m_EmitterShapeCircleSegments = 20;

		[SerializeField]
		private float m_ParticleLife = 3f;

		[SerializeField]
		private float m_ParticleLifeRandMin;

		[SerializeField]
		private float m_ParticleLifeRandMax = 1f;

		[SerializeField]
		private AnimationCurve m_particleSizeCurve = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 1f));

		[SerializeField]
		private float m_particleSize = 10f;

		[SerializeField]
		private float m_ParticleSizeRandomMax = 4f;

		[SerializeField]
		private float m_ParticleSpeed = 0.2f;

		[SerializeField]
		private float m_ParticleSpeedRandMin;

		[SerializeField]
		private float m_ParticleSpeedRandMax = 0.2f;

		[SerializeField]
		private bool m_AlignedRotation;

		[SerializeField]
		private float m_ParticleAngle;

		[SerializeField]
		private float m_ParticleAngleRandMin;

		[SerializeField]
		private float m_ParticleAngleRandMax;

		[SerializeField]
		private float m_ParticleSpinVel;

		[SerializeField]
		private float m_ParticleSpinVelRandMin;

		[SerializeField]
		private float m_ParticleSpinVelRandMax;

		[SerializeField]
		private bool m_useLifeColor;

		[SerializeField]
		private Gradient m_ParticleColorRamp = new Gradient();

		[SerializeField]
		private Color m_ParticleColor = Color.black;

		[SerializeField]
		private Color[] m_ParticleColorArray;

		[Range(0f, 1f)]
		[SerializeField]
		private float m_ParticleColorRandonR = 0.2f;

		[Range(0f, 1f)]
		[SerializeField]
		private float m_ParticleColorRandonG = 0.2f;

		[Range(0f, 1f)]
		[SerializeField]
		private float m_ParticleColorRandonB = 0.8f;

		[Range(0f, 1f)]
		[SerializeField]
		private float m_ParticleAlpha = 1f;

		[SerializeField]
		private AnimationCurve m_particleAlphaCurve = new AnimationCurve(new Keyframe(0f, 1f), new Keyframe(1f, 0f));

		[SerializeField]
		private Rect m_UVRect = new Rect(0f, 0f, 1f, 1f);

		[SerializeField]
		private Texture[] m_TextureArray;

		private List<Vector4> AtlasUVs;

		[SerializeField]
		private Texture m_Texture;

		[SerializeField]
		private bool m_isAnimated;

		[SerializeField]
		private bool m_isRandomStartFrame;

		private Texture m_AtlasTexture;

		private int m_GlobalParticleFrame;

		private float m_GlobalParticleTime;

		[SerializeField]
		private float m_ParticleFrameRate = 5f;

		private float m_TimeFromLastEmition;

		private List<CanvasParticle> ParticleList = new List<CanvasParticle>();

		private float scaledTimeSinceStart;

		public bool UseScaledTime
		{
			get
			{
				return m_UseScaledTime;
			}
			set
			{
				m_UseScaledTime = value;
			}
		}

		private float deltaTime
		{
			get
			{
				return (!UseScaledTime) ? Time.unscaledDeltaTime : Time.deltaTime;
			}
		}

		private float timeSinceFrameStart
		{
			get
			{
				return (!UseScaledTime) ? Time.unscaledTime : Time.time;
			}
		}

		public bool PlayOnAwake
		{
			get
			{
				return m_playOnAwake;
			}
			set
			{
				m_playOnAwake = value;
			}
		}

		public bool IsEmitterActive
		{
			get
			{
				return m_isActive;
			}
		}

		public bool IsEmitterPlaying
		{
			get
			{
				return m_isPlaying;
			}
		}

		public float Duration
		{
			get
			{
				return m_Duration;
			}
			set
			{
				m_Duration = value;
			}
		}

		public float ElapsedEmitterTime
		{
			get
			{
				return m_elapsedEmitterTime;
			}
		}

		public float EditorEmitterTime
		{
			get
			{
				return m_editorEmitterTime;
			}
			set
			{
				m_editorEmitterTime = value;
			}
		}

		public int MaxParticles
		{
			get
			{
				return m_MaxParticles;
			}
			set
			{
				m_MaxParticles = value;
			}
		}

		public int NumberOfParticles
		{
			get
			{
				return m_numberOfParticles;
			}
		}

		public int ParticlesPerEmission
		{
			get
			{
				return m_ParticlesPerEmission;
			}
			set
			{
				m_ParticlesPerEmission = value;
			}
		}

		public float EmiterRate
		{
			get
			{
				return m_EmitterRate;
			}
			set
			{
				m_EmitterRate = value;
			}
		}

		public float EmitterSpread
		{
			get
			{
				return m_EmitterSpread;
			}
			set
			{
				m_EmitterSpread = value;
			}
		}

		public float EmitterDirection
		{
			get
			{
				return m_EmitterDirection;
			}
			set
			{
				m_EmitterDirection = value;
			}
		}

		public Vector2 Gravity
		{
			get
			{
				return m_Gravity;
			}
			set
			{
				m_Gravity = value;
			}
		}

		public Vector2 Wave
		{
			get
			{
				return m_Wave;
			}
			set
			{
				m_Wave = value;
			}
		}

		public Vector2 WaveAmplitude
		{
			get
			{
				return m_WaveAmplitude;
			}
			set
			{
				m_WaveAmplitude = value;
			}
		}

		public Vector2 Turbulence
		{
			get
			{
				return m_Turbulence;
			}
			set
			{
				m_Turbulence = value;
			}
		}

		public Vector2 TurbulenceAmplitude
		{
			get
			{
				return m_TurbulenceAmplitude;
			}
			set
			{
				m_TurbulenceAmplitude = value;
			}
		}

		public EmitterShapes EmitterShape
		{
			get
			{
				return m_EmitterShape;
			}
			set
			{
				m_EmitterShape = value;
			}
		}

		public EmitterShapeLineTypes EmitterShapeLineType
		{
			get
			{
				return m_EmitterShapeLineType;
			}
			set
			{
				m_EmitterShapeLineType = value;
			}
		}

		public Vector2 EmitterShapeLinePointA
		{
			get
			{
				return m_EmitterShapeLinePointA;
			}
			set
			{
				m_EmitterShapeLinePointA = value;
			}
		}

		public Vector2 EmitterShapeLinePointB
		{
			get
			{
				return m_EmitterShapeLinePointB;
			}
			set
			{
				m_EmitterShapeLinePointB = value;
			}
		}

		public float EmitterShapeLineLength
		{
			get
			{
				return m_EmitterShapeLineLength;
			}
			set
			{
				m_EmitterShapeLineLength = value;
			}
		}

		public float EmitterShapeLineAngle
		{
			get
			{
				return m_EmitterShapeLineAngle;
			}
			set
			{
				m_EmitterShapeLineAngle = value;
			}
		}

		public EmitterShapeRectTypes EmitterShapeRectType
		{
			get
			{
				return m_EmitterShapeRectType;
			}
			set
			{
				m_EmitterShapeRectType = value;
			}
		}

		public Rect EmitterShapeRect
		{
			get
			{
				return m_EmitterShapeRect;
			}
			set
			{
				m_EmitterShapeRect = value;
			}
		}

		public RectTransform EmitterShapeRectTransform
		{
			get
			{
				return m_EmitterShapeRectTransform;
			}
			set
			{
				m_EmitterShapeRectTransform = value;
			}
		}

		public EmitterShapeCircleTypes EmitterShapeCircleType
		{
			get
			{
				return m_EmitterShapeCircleType;
			}
			set
			{
				m_EmitterShapeCircleType = value;
			}
		}

		public float EmitterShapeCircleRadius
		{
			get
			{
				return m_EmitterShapeCircleRadius;
			}
			set
			{
				m_EmitterShapeCircleRadius = value;
			}
		}

		public int EmitterShapeCircleSegments
		{
			get
			{
				return m_EmitterShapeCircleSegments;
			}
			set
			{
				m_EmitterShapeCircleSegments = value;
			}
		}

		public float ParticleLife
		{
			get
			{
				return m_ParticleLife;
			}
			set
			{
				m_ParticleLife = value;
			}
		}

		public float ParticleLifeRandomMin
		{
			get
			{
				return m_ParticleLifeRandMin;
			}
			set
			{
				m_ParticleLifeRandMin = value;
			}
		}

		public float ParticleLifeRandomMax
		{
			get
			{
				return m_ParticleLifeRandMax;
			}
			set
			{
				m_ParticleLifeRandMax = value;
			}
		}

		public AnimationCurve ParticleSizeCurve
		{
			get
			{
				return m_particleSizeCurve;
			}
			set
			{
				m_particleSizeCurve = value;
			}
		}

		public float ParticleSize
		{
			get
			{
				return m_particleSize;
			}
			set
			{
				m_particleSize = value;
				if (value < 0f)
				{
					m_particleSize = 0f;
				}
			}
		}

		public float ParticleSizeRandomMax
		{
			get
			{
				return m_ParticleSizeRandomMax;
			}
			set
			{
				m_ParticleSizeRandomMax = value;
			}
		}

		public float ParticleSpeed
		{
			get
			{
				return m_ParticleSpeed;
			}
			set
			{
				m_ParticleSpeed = value;
			}
		}

		public float ParticleSpeedRandomMin
		{
			get
			{
				return m_ParticleSpeedRandMin;
			}
			set
			{
				m_ParticleSpeedRandMin = value;
			}
		}

		public float ParticleSpeedRandomMax
		{
			get
			{
				return m_ParticleSpeedRandMax;
			}
			set
			{
				m_ParticleSpeedRandMax = value;
			}
		}

		public bool AlignedRotation
		{
			get
			{
				return m_AlignedRotation;
			}
			set
			{
				m_AlignedRotation = value;
			}
		}

		public float PartcileAngle
		{
			get
			{
				return m_ParticleAngle;
			}
			set
			{
				m_ParticleAngle = value;
			}
		}

		public float ParticleAngleRandomMin
		{
			get
			{
				return m_ParticleAngleRandMin;
			}
			set
			{
				m_ParticleAngleRandMin = value;
			}
		}

		public float ParticleAngleRandomMax
		{
			get
			{
				return m_ParticleAngleRandMax;
			}
			set
			{
				m_ParticleAngleRandMax = value;
			}
		}

		public float ParticleSpinVelocity
		{
			get
			{
				return m_ParticleSpinVel;
			}
			set
			{
				m_ParticleSpinVel = value;
			}
		}

		public float ParticleSpinVelocityRandomMin
		{
			get
			{
				return m_ParticleSpinVelRandMin;
			}
			set
			{
				m_ParticleSpinVelRandMin = value;
			}
		}

		public float ParticleSpinVelocityRandomMax
		{
			get
			{
				return m_ParticleSpinVelRandMax;
			}
			set
			{
				m_ParticleSpinVelRandMax = value;
			}
		}

		public bool UseLifeColor
		{
			get
			{
				return m_useLifeColor;
			}
			set
			{
				m_useLifeColor = value;
			}
		}

		public Gradient ParticleColorRamp
		{
			get
			{
				return m_ParticleColorRamp;
			}
			set
			{
				m_ParticleColorRamp = value;
			}
		}

		public Color ParticleColor
		{
			get
			{
				return m_ParticleColor;
			}
			set
			{
				m_ParticleColor = value;
			}
		}

		public Color[] ParticleColorsArray
		{
			get
			{
				return m_ParticleColorArray;
			}
			set
			{
				m_ParticleColorArray = value;
			}
		}

		public float ParticleColorRandomR
		{
			get
			{
				return m_ParticleColorRandonR;
			}
			set
			{
				m_ParticleColorRandonR = value;
			}
		}

		public float ParticleColorRandomG
		{
			get
			{
				return m_ParticleColorRandonG;
			}
			set
			{
				m_ParticleColorRandonG = value;
			}
		}

		public float ParticleColorRandomB
		{
			get
			{
				return m_ParticleColorRandonB;
			}
			set
			{
				m_ParticleColorRandonB = value;
			}
		}

		public float ParticleAlpha
		{
			get
			{
				return m_ParticleAlpha;
			}
			set
			{
				m_ParticleAlpha = value;
			}
		}

		public AnimationCurve ParticleAlphaCurve
		{
			get
			{
				return m_particleAlphaCurve;
			}
			set
			{
				m_particleAlphaCurve = value;
			}
		}

		public Rect uvRect
		{
			get
			{
				return m_UVRect;
			}
			set
			{
				if (!(m_UVRect == value))
				{
					m_UVRect = value;
					SetVerticesDirty();
				}
			}
		}

		public Texture[] TextureArray
		{
			get
			{
				return (m_TextureArray != null) ? m_TextureArray : new Texture[1] { mainTexture };
			}
			set
			{
				if (m_TextureArray != value)
				{
					m_TextureArray = value;
					SetVerticesDirty();
					SetMaterialDirty();
				}
			}
		}

		public Texture texture
		{
			get
			{
				return m_Texture;
			}
			set
			{
				if (!(m_Texture == value))
				{
					m_Texture = value;
					SetVerticesDirty();
					SetMaterialDirty();
				}
			}
		}

		public override Texture mainTexture
		{
			get
			{
				if (m_TextureArray != null && m_TextureArray.Length > 1)
				{
					return m_AtlasTexture;
				}
				if (m_TextureArray.Length == 1)
				{
					return m_TextureArray[0];
				}
				return (!(m_Texture == null)) ? m_Texture : Graphic.s_WhiteTexture;
			}
		}

		public bool IsAnimated
		{
			get
			{
				return m_isAnimated;
			}
			set
			{
				m_isAnimated = value;
			}
		}

		public bool IsRandomStartFrame
		{
			get
			{
				return m_isRandomStartFrame;
			}
			set
			{
				m_isRandomStartFrame = value;
			}
		}

		public float ParticleFrameRate
		{
			get
			{
				return m_ParticleFrameRate;
			}
			set
			{
				m_ParticleFrameRate = value;
			}
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Color.white;
			Gizmos.DrawLine(base.transform.position + new Vector3(0f, -5f, 0f), base.transform.position + new Vector3(0f, 5f, 0f));
			Gizmos.DrawLine(base.transform.position + new Vector3(-5f, 0f, 0f), base.transform.position + new Vector3(5f, 0f, 0f));
		}

		[ContextMenu("Play")]
		public void StartEmitter()
		{
			m_isActive = true;
			m_elapsedEmitterTime = 0f;
			m_TimeFromLastEmition = float.MaxValue;
		}

		[ContextMenu("Pause")]
		public void PauseEmitter()
		{
			m_isActive = false;
		}

		public void FreezeEmitter()
		{
			m_isPlaying = false;
		}

		public void UnFreezeEmitter()
		{
			m_isPlaying = true;
		}

		[ContextMenu("Stop")]
		public void StopEmitter()
		{
			m_isPlaying = true;
			DeleteAllParticles();
			m_isActive = false;
		}

		[ContextMenu("Restart")]
		public void RestartEmitter()
		{
			m_isPlaying = true;
			StopEmitter();
			StartEmitter();
		}

		public void DeleteAllParticles()
		{
			ParticleList.Clear();
			m_numberOfParticles = 0;
			base.canvasRenderer.Clear();
		}

		private new void Awake()
		{
			if (m_TextureArray != null && m_TextureArray.Length > 1)
			{
				CreateTextureAtlas();
			}
			scaledTimeSinceStart = timeSinceFrameStart;
		}

		private new void Start()
		{
			if (m_playOnAwake)
			{
				StartEmitter();
			}
		}

		private new void OnEnable()
		{
			if (ParticleList == null)
			{
				ParticleList = new List<CanvasParticle>();
			}
		}

		private new void OnDisable()
		{
			if (m_TextureArray != null && m_TextureArray.Length > 1)
			{
				m_AtlasTexture = null;
			}
		}

		private void Update()
		{
			m_TimeFromLastEmition += deltaTime;
			scaledTimeSinceStart += deltaTime;
			UpdateEmitter();
		}

		private void CreateTextureAtlas()
		{
			float num = 2f;
			List<int> list = new List<int>();
			Vector2 vector = new Vector2(num, num);
			AtlasUVs = new List<Vector4>();
			float num2 = 0f;
			for (int i = 0; i < m_TextureArray.Length; i++)
			{
				if (m_TextureArray != null)
				{
					if (num2 < (float)m_TextureArray[i].height)
					{
						num2 = (float)m_TextureArray[i].height + num * 2f;
					}
					vector = new Vector2(vector.x + (float)m_TextureArray[i].width + num, num2);
					list.Add(i);
				}
			}
			Texture2D texture2D = new Texture2D(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y), TextureFormat.ARGB32, false);
			Color clear = Color.clear;
			Color[] array = new Color[texture2D.width * texture2D.height];
			for (int j = 0; j < array.Length; j++)
			{
				array[j] = clear;
			}
			texture2D.SetPixels(array);
			int num3 = Mathf.RoundToInt(num);
			for (int k = 0; k < list.Count; k++)
			{
				RenderTexture active = RenderTexture.active;
				Texture texture = m_TextureArray[list[k]];
				RenderTexture temporary = RenderTexture.GetTemporary(texture.width, texture.height, 0, RenderTextureFormat.Default, RenderTextureReadWrite.Linear);
				Graphics.Blit(texture, temporary);
				RenderTexture.active = temporary;
				texture2D.ReadPixels(new Rect(0f, 0f, temporary.width, temporary.height), num3, Mathf.RoundToInt(num));
				texture2D.Apply();
				Vector4 item = new Vector4(((float)num3 - num / 2f) / (float)texture2D.width, 0f, ((float)num3 - num / 2f + (float)m_TextureArray[k].width) / (float)texture2D.width, 1f);
				AtlasUVs.Add(item);
				RenderTexture.ReleaseTemporary(temporary);
				RenderTexture.active = active;
				num3 += texture.width + Mathf.RoundToInt(num);
			}
			m_AtlasTexture = texture2D;
		}

		public void UpdateFromEditor()
		{
			m_isActive = true;
			m_isPlaying = true;
			m_editorEmitterTime += 0.1f;
			Rebuild(CanvasUpdate.PreRender);
			AddParticle();
			GraphicRegistry.RegisterGraphicForCanvas(base.canvas, this);
			SetAllDirty();
			Canvas.ForceUpdateCanvases();
		}

		public virtual void OnBeforeSerialize()
		{
		}

		public virtual void OnAfterDeserialize()
		{
		}

		private void UpdateEmitter()
		{
			if (m_isPlaying)
			{
				if (m_elapsedEmitterTime >= m_Duration && m_Duration > 0f)
				{
					m_isActive = false;
				}
				m_elapsedEmitterTime += deltaTime;
				if (m_TimeFromLastEmition >= 1f / m_EmitterRate && m_isActive)
				{
					m_TimeFromLastEmition = 0f;
					AddParticle();
				}
				SetVerticesDirty();
				SetMaterialDirty();
			}
		}

		private void AddParticle()
		{
			int num = m_ParticlesPerEmission;
			if (m_MaxParticles > 0 && m_numberOfParticles + num > m_MaxParticles)
			{
				num = m_MaxParticles - m_numberOfParticles;
			}
			float num2 = m_particleSize * m_particleSizeCurve.Evaluate(m_particleSizeCurve.keys[0].time);
			float particleAlpha = m_ParticleAlpha * m_particleAlphaCurve.Evaluate(m_particleAlphaCurve.keys[0].time);
			for (int i = 0; i < num; i++)
			{
				CanvasParticle canvasParticle = new CanvasParticle();
				canvasParticle.ParticleRandomSize = Random.Range(0f, m_ParticleSizeRandomMax);
				canvasParticle.ParticleSize = num2 + canvasParticle.ParticleRandomSize;
				canvasParticle.ParticleVertices = new Vector2[4]
				{
					Vector2.zero,
					Vector2.zero,
					Vector2.zero,
					Vector2.zero
				};
				canvasParticle.ParticleTotalLife = m_ParticleLife + Random.Range(m_ParticleLifeRandMin, m_ParticleLifeRandMax);
				canvasParticle.ParticleLife = canvasParticle.ParticleTotalLife;
				canvasParticle.ParticleSpinVel = m_ParticleSpinVel + Random.Range(m_ParticleSpinVelRandMin, m_ParticleSpinVelRandMax);
				canvasParticle.ParicleRotation = m_ParticleAngle + Random.Range(m_ParticleAngleRandMin, m_ParticleAngleRandMax);
				canvasParticle.ParticleAlpha = particleAlpha;
				if (!m_useLifeColor)
				{
					canvasParticle.ParticleOriginalColor = new Color(m_ParticleColor.r, m_ParticleColor.g, m_ParticleColor.b, canvasParticle.ParticleAlpha);
				}
				else
				{
					canvasParticle.ParticleOriginalColor = m_ParticleColorRamp.Evaluate(m_ParticleColorRamp.colorKeys[0].time);
				}
				if (m_ParticleColorArray.Length > 0)
				{
					canvasParticle.ParticleOriginalColor = m_ParticleColorArray[Random.Range(0, m_ParticleColorArray.Length - 1)];
				}
				Color particleColor = new Color(canvasParticle.ParticleOriginalColor.r + Random.Range(0f, m_ParticleColorRandonR), canvasParticle.ParticleOriginalColor.g + Random.Range(0f, m_ParticleColorRandonG), canvasParticle.ParticleOriginalColor.b + Random.Range(0f, m_ParticleColorRandonB), canvasParticle.ParticleAlpha);
				canvasParticle.ParticleColor = particleColor;
				float num3 = m_ParticleSpeed + Random.Range(m_ParticleSpeedRandMin, m_ParticleSpeedRandMax);
				if (m_TextureArray.Length > 0)
				{
					canvasParticle.ParticleTextureFrame = Random.Range(0, m_TextureArray.Length);
					if (!m_isRandomStartFrame && m_isAnimated)
					{
						canvasParticle.ParticleTextureFrame = m_GlobalParticleFrame;
					}
				}
				switch (m_EmitterShape)
				{
				case EmitterShapes.Directional:
					canvasParticle.ParticlePosition = Vector2.zero;
					canvasParticle.ParticleVelocity = new Vector2(Mathf.Sin((float)Math.PI / 180f * (m_EmitterDirection + Random.Range((0f - m_EmitterSpread) / 4f, m_EmitterSpread / 4f))), Mathf.Cos((float)Math.PI / 180f * (m_EmitterDirection + Random.Range((0f - m_EmitterSpread) / 4f, m_EmitterSpread / 4f)))).normalized * num3;
					break;
				case EmitterShapes.Point:
					canvasParticle.ParticlePosition = new Vector2(Random.Range(0, 0), Random.Range(0, 0));
					canvasParticle.ParticleVelocity = new Vector2(Mathf.Sin(Random.Range(0, 360)), Mathf.Sin(Random.Range(0f, 100f))).normalized * num3;
					break;
				case EmitterShapes.RectEmitter:
				{
					Rect rect = m_EmitterShapeRect;
					if (m_EmitterShapeRectTransform != null)
					{
						rect = GetRectCanvasSpace(m_EmitterShapeRectTransform);
					}
					switch (m_EmitterShapeRectType)
					{
					case EmitterShapeRectTypes.Area:
						canvasParticle.ParticlePosition = new Vector2(Random.Range(rect.xMin, rect.xMax), Random.Range(rect.yMin, rect.yMax));
						break;
					case EmitterShapeRectTypes.Edges:
						canvasParticle.ParticlePosition = Vector2.zero;
						switch (Random.Range(0, 4))
						{
						case 0:
							canvasParticle.ParticlePosition = new Vector2(Random.Range(rect.xMin, rect.xMax), rect.yMin);
							break;
						case 1:
							canvasParticle.ParticlePosition = new Vector2(rect.xMin, Random.Range(rect.yMin, rect.yMax));
							break;
						case 2:
							canvasParticle.ParticlePosition = new Vector2(Random.Range(rect.xMin, rect.xMax), rect.yMax);
							break;
						case 3:
							canvasParticle.ParticlePosition = new Vector2(rect.xMax, Random.Range(rect.yMin, rect.yMax));
							break;
						default:
							canvasParticle.ParticlePosition = new Vector2(rect.xMax, Random.Range(rect.yMin, rect.yMax));
							break;
						}
						break;
					case EmitterShapeRectTypes.Vertices:
					{
						Vector2[] array = new Vector2[4]
						{
							new Vector2(rect.xMin, rect.yMin),
							new Vector2(rect.xMin, rect.yMax),
							new Vector2(rect.xMax, rect.yMax),
							new Vector2(rect.xMax, rect.yMin)
						};
						canvasParticle.ParticlePosition = array[Random.Range(0, array.Length)];
						break;
					}
					}
					canvasParticle.ParticleVelocity = new Vector2(Mathf.Sin(Random.Range(0, 360)), Mathf.Sin(Random.Range(0f, 100f))).normalized * num3;
					break;
				}
				case EmitterShapes.LineEmitter:
					canvasParticle.ParticlePosition = GetPointInSegment(m_EmitterShapeLinePointA, m_EmitterShapeLinePointB, (float)Random.Range(0, 100) / 100f);
					canvasParticle.ParticleVelocity = new Vector2(Mathf.Sin((float)Math.PI / 180f * (m_EmitterDirection + Random.Range((0f - m_EmitterSpread) / 4f, m_EmitterSpread / 4f))), Mathf.Cos((float)Math.PI / 180f * (m_EmitterDirection + Random.Range((0f - m_EmitterSpread) / 4f, m_EmitterSpread / 4f)))).normalized * num3;
					break;
				case EmitterShapes.CircleEmitter:
					switch (m_EmitterShapeCircleType)
					{
					case EmitterShapeCircleTypes.Edge:
					{
						int num4 = Random.Range(0, m_EmitterShapeCircleSegments);
						int num5 = num4 + 1;
						if (num5 >= m_EmitterShapeCircleSegments)
						{
							num5 = 0;
						}
						canvasParticle.ParticlePosition = GetPointInSegment(CircleShape(m_EmitterShapeCircleSegments, m_EmitterShapeCircleRadius)[num4], CircleShape(m_EmitterShapeCircleSegments, m_EmitterShapeCircleRadius)[num5], (float)Random.Range(0, 100) / 100f);
						break;
					}
					case EmitterShapeCircleTypes.Vertices:
						canvasParticle.ParticlePosition = CircleShape(m_EmitterShapeCircleSegments, m_EmitterShapeCircleRadius)[Random.Range(0, m_EmitterShapeCircleSegments)];
						break;
					}
					canvasParticle.ParticleVelocity = new Vector2(Mathf.Sin(Random.Range(0, 360)), Mathf.Sin(Random.Range(0f, 100f))).normalized * num3;
					break;
				}
				ParticleList.Add(canvasParticle);
			}
			m_numberOfParticles = ParticleList.Count;
		}

		private List<Vector2> CircleShape(int sides, float radius)
		{
			List<Vector2> list = new List<Vector2>();
			for (int i = 0; i < sides; i++)
			{
				float f = (float)Math.PI * 2f * ((float)i / (float)sides);
				float x = Mathf.Cos(f) * radius;
				float y = Mathf.Sin(f) * radius;
				Vector2 item = new Vector2(x, y);
				list.Add(item);
			}
			return list;
		}

		private void RemoveParticle(CanvasParticle particle)
		{
			ParticleList.Remove(particle);
			m_numberOfParticles = ParticleList.Count;
		}

		private Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Vector3 angles)
		{
			Vector3 vector = point - pivot;
			vector = Quaternion.Euler(angles) * vector;
			point = vector + pivot;
			return point;
		}

		private Vector2 GetPointInSegment(Vector2 pointA, Vector2 pointB, float percent)
		{
			return new Vector2(pointA.x + percent * (pointB.x - pointA.x), pointA.y + percent * (pointB.y - pointA.y));
		}

		private Rect GetRectCanvasSpace(RectTransform rectTransform)
		{
			Vector3[] array = new Vector3[4];
			Vector3[] array2 = new Vector3[2];
			Canvas canvas = GetCanvas();
			rectTransform.GetWorldCorners(array);
			if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
			{
				array2[0] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, array[1]);
				array2[1] = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, array[3]);
			}
			else
			{
				array2[0] = RectTransformUtility.WorldToScreenPoint(null, array[1]);
				array2[1] = RectTransformUtility.WorldToScreenPoint(null, array[3]);
			}
			float x = array2[0].x - canvas.GetComponent<RectTransform>().sizeDelta.x * rectTransform.pivot.x;
			float y = array2[0].y - canvas.GetComponent<RectTransform>().sizeDelta.y * rectTransform.pivot.y;
			float width = array2[1].x - array2[0].x;
			float height = array2[1].y - array2[0].y;
			return new Rect(x, y, width, height);
		}

		private Canvas GetCanvas()
		{
			GameObject gameObject = base.gameObject;
			Canvas canvas = ((!(gameObject != null)) ? null : gameObject.GetComponentInParent<Canvas>());
			if (canvas != null && canvas.gameObject.activeInHierarchy)
			{
				return canvas;
			}
			canvas = Object.FindObjectOfType(typeof(Canvas)) as Canvas;
			if (canvas != null && canvas.gameObject.activeInHierarchy)
			{
				return canvas;
			}
			return null;
		}

		public override void Rebuild(CanvasUpdate update)
		{
			float time = m_particleSizeCurve.keys[m_particleSizeCurve.length - 1].time;
			float time2 = m_particleAlphaCurve.keys[m_particleAlphaCurve.length - 1].time;
			for (int i = 0; i < ParticleList.Count; i++)
			{
				Vector2 vector = new Vector2(Mathf.Sin(scaledTimeSinceStart * m_Wave.x) * (m_WaveAmplitude.x / 1000f), Mathf.Sin(scaledTimeSinceStart * m_Wave.y) * (m_WaveAmplitude.y / 1000f));
				Vector2 vector2 = new Vector2(Mathf.PerlinNoise(scaledTimeSinceStart * m_Turbulence.x, 0f) * (m_TurbulenceAmplitude.x / 100f) - m_TurbulenceAmplitude.x / 200f, Mathf.PerlinNoise(scaledTimeSinceStart * m_Turbulence.y, 0f) * (m_TurbulenceAmplitude.y / 100f) - m_TurbulenceAmplitude.y / 200f);
				Vector2 gravity = m_Gravity;
				Vector2 vector3 = gravity + vector + vector2;
				Vector2 particlePosition = ParticleList[i].ParticlePosition;
				ParticleList[i].ParticlePosition += ParticleList[i].ParticleVelocity * deltaTime + vector3 * deltaTime * deltaTime * 0.5f;
				ParticleList[i].ParticleVelocity += vector3 * deltaTime;
				ParticleList[i].ParticleLife -= deltaTime;
				ParticleList[i].ParicleRotation += ParticleList[i].ParticleSpinVel;
				if (m_AlignedRotation)
				{
					float paricleRotation = Mathf.Atan2(ParticleList[i].ParticlePosition.y - particlePosition.y, ParticleList[i].ParticlePosition.x - particlePosition.x) * 180f / (float)Math.PI;
					ParticleList[i].ParicleRotation = paricleRotation;
				}
				float num = ParticleList[i].ParticleLife / ParticleList[i].ParticleTotalLife;
				ParticleList[i].ParticleSize = (m_particleSize + ParticleList[i].ParticleRandomSize) * m_particleSizeCurve.Evaluate((1f - num) * time);
				ParticleList[i].ParticleAlpha = m_ParticleAlpha * m_particleAlphaCurve.Evaluate((1f - num) * time2);
				if (!m_useLifeColor)
				{
					ParticleList[i].ParticleColor = new Color(ParticleList[i].ParticleColor.r, ParticleList[i].ParticleColor.g, ParticleList[i].ParticleColor.b, ParticleList[i].ParticleAlpha);
				}
				else
				{
					Color color = m_ParticleColorRamp.Evaluate((1f - num) * m_ParticleColorRamp.colorKeys[m_ParticleColorRamp.colorKeys.Length - 1].time);
					ParticleList[i].ParticleColor = new Color(color.r, color.g, color.b, ParticleList[i].ParticleAlpha);
				}
				if (ParticleList[i].ParticleLife <= 0f && m_ParticleLife != 0f)
				{
					RemoveParticle(ParticleList[i]);
				}
			}
			base.Rebuild(update);
		}

		protected override void OnPopulateMesh(Mesh mesh)
		{
			mesh.Clear(false);
			VertexHelper vertexHelper = new VertexHelper(mesh);
			OnPopulateMesh(vertexHelper);
			vertexHelper.FillMesh(mesh);
		}

		protected void SetMesh(VertexHelper vh, Vector2[] vertices, Vector2[] uvs, Color partColor)
		{
			int[] array = new int[vertices.Length];
			for (int i = 0; i < vertices.Length; i++)
			{
				UIVertex simpleVert = UIVertex.simpleVert;
				simpleVert.color = partColor;
				simpleVert.position = vertices[i];
				simpleVert.uv0 = uvs[i];
				vh.AddVert(simpleVert);
				array[i] = vh.currentVertCount - 1;
			}
			vh.AddTriangle(array[0], array[1], array[2]);
			vh.AddTriangle(array[2], array[3], array[0]);
		}

		protected override void OnPopulateMesh(VertexHelper vh)
		{
			vh.Clear();
			for (int i = 0; i < ParticleList.Count; i++)
			{
				CanvasParticle canvasParticle = new CanvasParticle();
				canvasParticle = ParticleList[i];
				Vector2 particlePosition = canvasParticle.ParticlePosition;
				Vector2 vector = particlePosition + new Vector2((0f - canvasParticle.ParticleSize) / 2f, (0f - canvasParticle.ParticleSize) / 2f);
				Vector2 vector2 = particlePosition + new Vector2(canvasParticle.ParticleSize / 2f, canvasParticle.ParticleSize / 2f);
				canvasParticle.ParticleVertices[0] = new Vector2(vector.x, vector.y);
				canvasParticle.ParticleVertices[1] = new Vector2(vector.x, vector2.y);
				canvasParticle.ParticleVertices[2] = new Vector2(vector2.x, vector2.y);
				canvasParticle.ParticleVertices[3] = new Vector2(vector2.x, vector.y);
				canvasParticle.ParticleVertices[0] = RotatePointAroundPivot(canvasParticle.ParticleVertices[0], canvasParticle.ParticlePosition, new Vector3(0f, 0f, canvasParticle.ParicleRotation));
				canvasParticle.ParticleVertices[1] = RotatePointAroundPivot(canvasParticle.ParticleVertices[1], canvasParticle.ParticlePosition, new Vector3(0f, 0f, canvasParticle.ParicleRotation));
				canvasParticle.ParticleVertices[2] = RotatePointAroundPivot(canvasParticle.ParticleVertices[2], canvasParticle.ParticlePosition, new Vector3(0f, 0f, canvasParticle.ParicleRotation));
				canvasParticle.ParticleVertices[3] = RotatePointAroundPivot(canvasParticle.ParticleVertices[3], canvasParticle.ParticlePosition, new Vector3(0f, 0f, canvasParticle.ParicleRotation));
				Vector2[] uvs;
				if (m_TextureArray.Length > 1)
				{
					if (m_isAnimated)
					{
						canvasParticle.ParticleAnimTime += 1f;
						m_GlobalParticleTime += 1f;
						if (!m_isRandomStartFrame)
						{
							if (m_GlobalParticleTime >= (float)(30 * ParticleList.Count) / m_ParticleFrameRate)
							{
								m_GlobalParticleFrame++;
								if (m_GlobalParticleFrame >= m_TextureArray.Length)
								{
									m_GlobalParticleFrame = 0;
								}
								m_GlobalParticleTime = 0f;
							}
							canvasParticle.ParticleTextureFrame = m_GlobalParticleFrame;
						}
						else if (canvasParticle.ParticleAnimTime >= 30f / m_ParticleFrameRate)
						{
							canvasParticle.ParticleTextureFrame++;
							if (canvasParticle.ParticleTextureFrame >= m_TextureArray.Length)
							{
								canvasParticle.ParticleTextureFrame = 0;
							}
							canvasParticle.ParticleAnimTime = 0f;
						}
					}
					Vector4 vector3 = AtlasUVs[canvasParticle.ParticleTextureFrame];
					Vector2 vector4 = new Vector2(vector3.x, vector3.y);
					Vector2 vector5 = new Vector2(vector3.x, vector3.w);
					Vector2 vector6 = new Vector2(vector3.z, vector3.y);
					Vector2 vector7 = new Vector2(vector3.z, vector3.w);
					uvs = new Vector2[4] { vector5, vector4, vector6, vector7 };
				}
				else
				{
					Vector2 zero = Vector2.zero;
					Vector2 vector8 = new Vector2(0f, 1f);
					Vector2 vector9 = new Vector2(1f, 0f);
					Vector2 vector10 = new Vector2(1f, 1f);
					uvs = new Vector2[4] { vector8, zero, vector9, vector10 };
				}
				SetMesh(vh, canvasParticle.ParticleVertices, uvs, canvasParticle.ParticleColor);
			}
		}
	}
}
