using System.Collections.Generic;

namespace UnityEngine.UI
{
	public class CanvasParticle
	{
		public Vector2 ParticlePosition;

		public float ParticleSize;

		public float ParticleRandomSize;

		public Vector2 ParticleVelocity;

		public Vector2[] ParticleVertices;

		public CanvasParticleEmitter ParticleEmiter;

		public float ParticleLife;

		public float ParticleTotalLife;

		public float ParicleRotation;

		public float ParticleSpinVel;

		public Color ParticleColor;

		public Color ParticleOriginalColor;

		public float ParticleAlpha;

		public Texture ParticleTexture;

		public List<Texture> ParticleTextureAnim;

		public int ParticleTextureFrame;

		public int ParticleTextureFrameRate;

		public float ParticleAnimTime;
	}
}
