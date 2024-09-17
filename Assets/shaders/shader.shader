Shader "Unlit/shader"
{
    Properties
    {
        _DisplacementTex ("Displacement Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}

        _Color ("Color", Color) = (1,1,1,1)
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _Emission("Emission", float) = 0
        [HDR] _EmissionColor("Color", Color) = (0,0,0)
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        //ZWrite Off
        //Blend SrcAlpha OneMinusSrcAlpha
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
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewdir : TEXCOORD2;
                fixed4 color : COLOR;
                
                UNITY_VERTEX_OUTPUT_STEREO

            };



            
            sampler2D _DisplacementTex;
            sampler2D _OverlayTex;
            float _Emission;
            fixed4 _EmissionColor;
            uniform fixed4 _Color;
            //float4 _MainTex_ST;

            v2f vert (appdata v)
            {

                
                v2f o;

                UNITY_SETUP_INSTANCE_ID(v);
                UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(outpout);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewdir = normalize(WorldSpaceViewDir(v.vertex));


                o.uv = v.uv;
                float xMod = tex2Dlod(_DisplacementTex, float4(o.uv.xy, 0, 1));

                xMod = xMod * 2 - 1;
                o.uv.x = sin(xMod * 10 + _Time.y);

                float3 vert = v.vertex;
                float scale = 0.5f;
                //vert.y = o.uv.x;
                vert.x += o.normal.x * o.normal.x * o.uv.x * scale;
                vert.y += o.normal.y * o.normal.y * o.uv.x * scale;
                vert.z += o.normal.z * o.normal.z * o.uv.x * scale;

                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.vertex = UnityObjectToClipPos(vert);
                o.color = _Color;
                half4 outputvar = half4(_EmissionColor.rgb, _EmissionColor.a);
                half4 emission = tex2Dlod(_OverlayTex, float4(v.uv, 0, 0)) * _EmissionColor;///////////////
                outputvar.rgb += emission.rgb;////////////////////////
            
                o.color += outputvar;////////
                //o.Emission = _Emission * _EmissionColor;
                

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float fresnelAmount = dot(i.normal, i.viewdir);
                // sample the texture

                fixed4 col = tex2D(_OverlayTex, i.uv + _Time.y / 2);


                float alpha = clamp(1 - i.uv.x + col, 0.2f, 0.6f);
                float green = clamp(i.uv.x + col.x/2, 0.3f, 1);
                //Calculate Depth
                //float rawZ = SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(IN.screenPos));
                //float sceneZ = LinearEyeDepth(rawZ);
                //float partZ = IN.eyeDepth;

                //float fade = saturate(_SoftFactor * (sceneZ - partZ));

                //alpha = fade * 0.5;


                return fixed4(0, green ,0, alpha);
                //return i.color;
                //return fresnelAmount;
            }


            
            ENDCG
        }

    }
    FallBack "Diffuse"
}

