Shader "Custom/OverlayShader"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Intensity("Intensity", Range(0,5)) = 1
		_Alpha("Alpha", Range(0,1)) = 0.3
		_AlphaSpeed("Alpha Speed", Range(0,500)) = 0
		_ScanningFrequency("Scanning Frequency", Range(0,100)) = 0
		_ScanningSpeed("Scanning Speed", Range(0,500)) = 0
		_Bias("Bias", Range(-1,3)) = 0
		_Direction("Direction", Vector) = (1,0,0)
		[MaterialToggle] _isX("X", Float) = 0
		[MaterialToggle] _isY("Y", Float) = 0
		[HideInInspector][MaterialToggle] _isZ("Z", Float) = 0
    }
    SubShader
    {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		Blend SrcAlpha One
		ZWrite Off
		AlphaTest Greater 0.1
		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
			};
			struct v2f 
			{
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 objVertex : TEXCOORD1;
			};


			float4 _Color;
			float4 _Color_ST;
			float _Intensity;
			float _Alpha;
			float _AlphaSpeed;
			float _ScanningFrequency;
			float _ScanningSpeed;
			float _Bias;

			float _isX;
			float _isY;
			float _isZ;
			
			float map(float input, float min, float max, float outMin, float outMax)
			{
				float perc = (input - min) / (max - min);

				float value = perc * (outMax - outMin) + outMin;
				return value;
			}

			v2f vert(appdata v)
			{
				v2f o;
				o.objVertex = mul(unity_ObjectToWorld, v.vertex);
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _Color);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float4 col = _Color * _Intensity;
				col.a = map(cos(_Time.x * _AlphaSpeed),-1,1, 0, _Alpha);


				float sideways;
				if(_isX == 1)
					sideways = i.objVertex.x;
				else if (_isY == 1)
					sideways = i.objVertex.y;
				else if (_isZ == 1)
					sideways = i.objVertex.z;

				col *= max(0, cos(sideways *_ScanningFrequency + _Time.x * _ScanningSpeed) + _Bias);


				return col;
			}

			ENDHLSL
		}
    }
}
