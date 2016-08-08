Shader "Archer/Effect"
{
	Properties
	{
		_MainTex ("特效精灵", 2D) = "white" {}
		_Color("RGB（A<0.5?RGB:纹理颜色）",Color) = (1,1,1,1)
		_TotalSpriteCount("总共帧数",float) = 0
		_SpriteRowCount("行数",float) = 0
		_SpriteColumnCount("列数",float) = 0
		_Speed("速度",Range(0.01,10)) = 4.0
		_Alpha("过滤黑色强度",Range(0.01,2)) = 1
		_Light("亮度增强(RGB无效)",Range(0.1,3)) = 1
	}
	SubShader
	{
		Tags{ "QUEUE" = "Transparent" "IGNOREPROJECTOR" = "true" "RenderType" = "Transparent" }
		// No culling or depth
		Cull Off ZWrite Off ZTest Always
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			uniform sampler2D _MainTex;
			uniform float4 _Color;
			uniform float _TotalSpriteCount;
			uniform float _SpriteRowCount;
			uniform float _SpriteColumnCount;
			uniform float _Speed;
			uniform float _Alpha;
			uniform float _Light;
			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}			

			fixed4 frag (v2f i) : SV_Target
			{
				float rowAvgPercent = 1 / _SpriteColumnCount;
				float columnAvgPercent = 1 / _SpriteRowCount;
				float SpriteIndex = fmod(_Time.y*_Speed, _TotalSpriteCount);
				SpriteIndex = floor(SpriteIndex);
				float columnIndex = fmod(SpriteIndex, _SpriteColumnCount);
				float rowIndex = SpriteIndex / _SpriteColumnCount;
				rowIndex = _SpriteRowCount - 1 - floor(rowIndex);
				float2 spriteUV = i.uv;
				
				spriteUV.x = (spriteUV.x + columnIndex)*rowAvgPercent;
				spriteUV.y = (spriteUV.y + rowIndex)*columnAvgPercent;

				fixed4 col = tex2D(_MainTex, spriteUV);
				if (i.uv.x < 0.02 || i.uv.y < 0.02)
				{
					///去掉边缘
					col.a = 0;
				}
				else
				{
					///修正背景黑色为透明
					fixed totalrgb = (col.r + col.g + col.b);
					col.a = totalrgb*(totalrgb + _Alpha);
				}
				///颜色选择
				if (_Color.a < 0.5)
				{
					col.rgb = _Color.rgb;
				}
				else
				{
					///色彩增强
					col.rgb = col.rgb*_Light;
				}
				return col;
			}
			ENDCG
		}
	}
}
