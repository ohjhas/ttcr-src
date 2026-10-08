// Reconstructed from the original GLES shader blob (AssetRipper dump).
// "UnscaledTime" is set per-material by HighlightWithPulsating.cs.
Shader "Playsoft/FastUI_Blinking_IgnoreTimescale" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Vector) = (1,1,1,1)
		_BlinkingColor ("Blinking", Vector) = (1,1,1,0)
		_Period ("Period", Float) = 1
		BlinkAlphaLimit ("Blink Alpha Limit", Range(0, 1)) = 0.9
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
			fixed4 _BlinkingColor;
			float _Period;
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
				float2 d = i.texcoord - float2(0.5, 0.5);
				float cordFactor = 1.0 - sqrt(dot(d, d)) / 0.7071;
				cordFactor *= sin(glslMod(UnscaledTime, _Period) / _Period * 3.14159);
				fixed4 c;
				c.a = col.a;
				c.rgb = (col.rgb + _BlinkingColor.rgb * cordFactor) * col.a;
				return c;
			}
			ENDCG
		}
	}
}
