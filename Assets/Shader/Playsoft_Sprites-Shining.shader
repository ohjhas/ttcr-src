// Reconstructed from the original GLES shader blob (AssetRipper dump).
Shader "Playsoft/Sprites-Shining" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Vector) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Vector) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 0
		_ShiningColor ("Shining", Vector) = (1,1,1,0)
		_Amplitude ("Amplitude", Float) = 0
		_Thickness ("Thickness", Range(0, 0.5)) = 0
		_ShineAlphaLimit ("ShineAlphaLimit", Range(0, 1)) = 0.9
	}
	SubShader {
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }
		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _EnableExternalAlpha;
			fixed4 _Color;
			fixed4 _RendererColor;
			fixed4 _ShiningColor;
			float _Amplitude;
			float _Thickness;
			float _ShineAlphaLimit;

			// GLSL-style mod (floor based), not HLSL fmod
			float glslMod (float x, float y) { return x - y * floor(x / y); }

			v2f vert (appdata_t v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color * _Color * _RendererColor;
				#ifdef PIXELSNAP_ON
				o.vertex = UnityPixelSnap(o.vertex);
				#endif
				return o;
			}

			fixed4 SampleSpriteTexture (float2 uv) {
				fixed4 color = tex2D(_MainTex, uv);
				#if ETC1_EXTERNAL_ALPHA
				color.a = lerp(color.a, tex2D(_AlphaTex, uv).r, _EnableExternalAlpha);
				#endif
				return color;
			}

			fixed4 frag (v2f i) : SV_Target {
				fixed4 col = SampleSpriteTexture(i.texcoord) * i.color;
				// diagonal sweep across the sprite
				float diag = (i.texcoord.x + 1.0 - i.texcoord.y) / 2.0;
				float pos = glslMod(_Time.y, _Amplitude) / _Amplitude;
				float halfThick = _Thickness * 0.5;
				float dist = (col.a >= _ShineAlphaLimit) ? abs(diag - pos) : halfThick;
				float shine = (halfThick - clamp(dist, 0.0, halfThick)) / halfThick;
				fixed4 c;
				c.a = col.a;
				c.rgb = (col.rgb + _ShiningColor.rgb * shine) * col.a;
				return c;
			}
			ENDCG
		}
	}
}
