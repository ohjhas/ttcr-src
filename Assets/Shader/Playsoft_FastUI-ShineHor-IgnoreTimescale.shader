// Reconstructed from the original GLES shader blob (AssetRipper dump).
// "UnscaledTime" is set per-material by HorizontalShineEffect.cs.
Shader "Playsoft/FastUI-ShineHor-IgnoreTimescale" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Vector) = (1,1,1,1)
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
		ZTest [unity_GUIZTestMode]
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t {
				float4 vertex : POSITION;
				float4 color : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
				half2 texcoord : TEXCOORD0;
			};

			sampler2D _MainTex;
			fixed4 _Color;
			fixed4 _TextureSampleAdd;
			fixed4 _ShiningColor;
			float _Amplitude;
			float _Thickness;
			float _ShineAlphaLimit;
			float UnscaledTime;

			// GLSL-style mod (floor based), not HLSL fmod
			float glslMod (float x, float y) { return x - y * floor(x / y); }

			v2f vert (appdata_t v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.texcoord = v.texcoord;
				o.color = v.color * _Color;
				return o;
			}

			fixed4 frag (v2f i) : SV_Target {
				fixed4 col = (tex2D(_MainTex, i.texcoord) + _TextureSampleAdd) * i.color;
				float pos = glslMod(UnscaledTime, _Amplitude) / _Amplitude;
				float halfThick = _Thickness * 0.5;
				float dist = (col.a >= _ShineAlphaLimit) ? abs(i.texcoord.x - pos) : halfThick;
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
