Shader "JSGraphics/DiffuseShader_test"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,1)
        _AmbientJSColorLOL("Ambient Color", Color) = (1,1,1,1)
        _JimSMySliderValue("This is a Slider", Range(0,10)) = 2.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex; // Optional
        };

        uniform float4 _Color;
        uniform float _AmbientJSColorLOL;
        uniform float _JimSMySliderValue;

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Οι παρακάτω μεταβλητές τραβιούνται από τα Properties αυτόματα
            fixed4 col = pow((_Color + _AmbientJSColorLOL), _JimSMySliderValue);
            o.Albedo = col.rgb;
            o.Alpha = col.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
