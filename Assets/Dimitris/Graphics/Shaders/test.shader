Shader "Custom/ForwardLitShader"
{
    Properties
    {
        _Color("Base Color", Color) = (1, 1, 1, 1)
        _AmbientColor("Ambient Color", Color) = (0.1, 0.1, 0.1, 1)
        _JimSMySliderValue("Slider", Range(0,10)) = 2.5
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        Pass
        {
            Tags { "LightMode"="ForwardBase" }

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase
            #pragma target 3.0

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            fixed4 _Color;
            fixed4 _AmbientColor;
            float _JimSMySliderValue;

            // Define light and transformation matrices
            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldNormal : TEXCOORD0;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldNormal = UnityObjectToWorldNormal(v.normal);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // Normalize the normal vector
                float3 normal = normalize(i.worldNormal);

                // Get light direction and color from the Unity's built-in light system
                float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;

                // Lambertian lighting calculation (Dot product of normal and light direction)
                float NdotL = max(0, dot(normal, lightDir));

                // Final color calculation, applying light intensity and ambient color
                fixed4 baseColor = pow((_Color + _AmbientColor), _JimSMySliderValue);
                float3 litColor = baseColor.rgb * NdotL * lightColor; // Diffuse lighting contribution
                float3 finalColor = litColor + _AmbientColor.rgb;  // Add ambient light

                return fixed4(finalColor, baseColor.a);
            }
            ENDCG
        }
    }

    FallBack "Diffuse"
}
