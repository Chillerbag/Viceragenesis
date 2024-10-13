Shader "Unlit Acid Shader/shader"
{
    Properties
    {
        _DisplacementTex ("Displacement Texture", 2D) = "white" {}
        _DisplacementStrength  ("Displacement Strength", float) = 1
        _Tiling  ("Tiling", int) = 1
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
        [HDR] _EmissionColor("Color", Color) = (0,0,0)
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Blend One One
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            ColorMask RGB
            CGPROGRAM
            #pragma vertex vert alpha
            #pragma fragment frag alpha
            #include "UnityCG.cginc"



            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewdir : TEXCOORD2;
                fixed4 color : COLOR;
                UNITY_VERTEX_OUTPUT_STEREO
            };

            sampler2D _DisplacementTex;
            sampler2D _OverlayTex;
            fixed4 _EmissionColor;
            float _DisplacementStrength;
            int _Tiling;

            v2f vert (appdata v)
            {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(outpout);
                o.normal = v.normal;
                o.viewdir = normalize(WorldSpaceViewDir(v.vertex));

                // moving displacement map
                o.uv = v.uv;
                float xMod = tex2Dlod(_DisplacementTex, float4(o.uv.x * _Tiling, o.uv.y * _Tiling, 0, 1));
                xMod = xMod * 2 - 1;
                o.uv.x = sin(xMod * 10 + _Time.y);

                // displace perpendicular to normal
                float3 vert = v.vertex;
                float scale = 0.5f;
                vert.x += o.normal.x * o.normal.x * o.uv.x * _DisplacementStrength;
                vert.y += o.normal.y * o.normal.y * o.uv.x * _DisplacementStrength;
                vert.z += o.normal.z * o.normal.z * o.uv.x * _DisplacementStrength;
                o.vertex = UnityObjectToClipPos(vert);

                // apply HDR vertex color
                o.color = float4(0, 0, 0, 0);
                half4 outputvar = half4(_EmissionColor.rgb, _EmissionColor.a);
                half4 emission = tex2Dlod(_OverlayTex, float4(v.uv, 0, 0)) * _EmissionColor;///////////////
                outputvar.rgb += emission.rgb;
                o.color += outputvar;

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                //float fresnelAmount = dot(i.normal, i.viewdir);

                fixed4 col = tex2D(_OverlayTex, i.uv + _Time.y / 2);
                float alpha = clamp(1 - i.uv.x + col, 0.3f, 0.8f);
                float saturation = clamp(i.uv.x + col.x/2, 0.3f, 1);

                return fixed4(saturation * i.color.x, saturation * i.color.y ,saturation * i.color.z, alpha) ;
            }


            
            ENDCG
        }

    }
    FallBack "Diffuse"
}

