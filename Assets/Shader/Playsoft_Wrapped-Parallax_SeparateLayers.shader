// Reconstructed from the original GLES shader blob (AssetRipper dump).
// Main texture plus two scrolling overlay layers from separate textures.
Shader "Playsoft/Wrapped-Parallax_SeparateLayers" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Vector) = (1,1,1,1)
		[HideInInspector] _FirstLayerTex ("_FirstLayerTex", 2D) = "white" {}
		[HideInInspector] _SecondLayerTex ("_SecondLayerTex", 2D) = "white" {}
		[HideInInspector] _mainTexOffset ("_mainTexOffset", Vector) = (0,0,0,0)
		[HideInInspector] _secondLayerTexOffset ("_secondLayerTexOffset", Vector) = (0,0,0,0)
		[HideInInspector] _firstLayerTexOffset ("_firstLayerTexOffset", Vector) = (0,0,0,0)
		[HideInInspector] _secondLayerSize ("_secondLayerSize", Float) = 1
		[HideInInspector] _firstLayerSize ("_firstLayerSize", Float) = 1
		[HideInInspector] _BlendOffset ("_BlendOffset", Float) = 1.5
		[HideInInspector] _BlendBorder ("_BlendBorder", Float) = 0.2
		[HideInInspector] _BlendFulfillment ("_BlendFulfillment", Float) = 0.2
		[HideInInspector] _BlendColor ("_BlendColor", Vector) = (0,0,0,0)
		[HideInInspector] _BlendTex ("_BlendTex", 2D) = "white" {}
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[HideInInspector] _RendererColor ("RendererColor", Vector) = (1,1,1,1)
		[HideInInspector] _Flip ("Flip", Vector) = (1,1,1,1)
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[PerRendererData] _EnableExternalAlpha ("Enable External Alpha", Float) = 1
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
			sampler2D _FirstLayerTex;
			sampler2D _SecondLayerTex;
			float _EnableExternalAlpha;
			fixed4 _Color;
			fixed4 _RendererColor;
			float4 _mainTexOffset;
			float4 _secondLayerTexOffset;
			float4 _firstLayerTexOffset;
			float _firstLayerSize;
			float _secondLayerSize;

			// GLSL-style mod(x, 1.0)
			float2 wrap01 (float2 x) { return x - floor(x); }

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
				fixed4 baseCol = SampleSpriteTexture(wrap01(i.texcoord + _mainTexOffset.xy));
				fixed4 c = baseCol;

				if (i.texcoord.y >= _secondLayerSize * 0.5 && i.texcoord.y <= 1.0 - _secondLayerSize * 0.5) {
					float2 uv = float2(i.texcoord.x, (i.texcoord.y - _secondLayerSize * 0.5) / (1.0 - _secondLayerSize));
					fixed4 l = tex2D(_SecondLayerTex, wrap01(uv + _secondLayerTexOffset.xy));
					c.rgb = lerp(baseCol.rgb, l.rgb, l.a);
				}
				if (i.texcoord.y >= _firstLayerSize * 0.5 && i.texcoord.y <= 1.0 - _firstLayerSize * 0.5) {
					float2 uv = float2(i.texcoord.x, (i.texcoord.y - _firstLayerSize * 0.5) / (1.0 - _firstLayerSize));
					fixed4 l = tex2D(_FirstLayerTex, wrap01(uv + _firstLayerTexOffset.xy));
					c.rgb = lerp(c.rgb, l.rgb, l.a);
				}
				c.rgb *= (i.color * baseCol.a).rgb;
				return c;
			}
			ENDCG
		}
	}
}
