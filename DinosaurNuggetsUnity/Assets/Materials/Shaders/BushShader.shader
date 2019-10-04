Shader "Custom/BushShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Colour("Colour", Color) = (1,1,1,1)
        _Min("Min", Range (0.0, 2.0)) = 0.0
        _Max("Max", Range (0.0, 2.0)) = 1.0
    }
    SubShader
    {
        Tags { "LightMode"="LightweightForward" }
        LOD 100

        CGPROGRAM

        #pragma surface surf Standard fullforwardshadows vertex:vert

        sampler2D _MainTex;
        fixed4 _Colour;
        float _Min;
        float _Max;

        struct Input
        {
            float2 uv_MainTex;
            float multiplyValue;
        };

        

        float remap(float _in, float a_Min, float a_Max, float b_Min, float b_Max)
        {
            float perc = (_in - a_Min) / (a_Max - a_Min);
            float value = perc * (b_Max - b_Min) + b_Min;
            return value;
        }

        void vert(inout appdata_full v, out Input o)
        {
            //float multiplyValue = abs(sin(_Time * 10 + v.vertex.x)); //how much we want to multiply our vertex
            //v.vertex.x *= multiplyValue * v.normal.x;
            v.vertex.y *= remap(sin(_Time *30), -1.0, 1.0, _Min, _Max);
            UNITY_INITIALIZE_OUTPUT(Input,o);
        }

       

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 c = _Colour;
            o.Albedo = _Colour;

            //o.Alpha = c.a;
        }
        ENDCG
    }
    Fallback "Diffuse"
}
