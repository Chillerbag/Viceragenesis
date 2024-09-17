Shader "Unlit/shader"
{
    Properties
    {
        _DisplacementTex ("Displacement Texture", 2D) = "white" {}
        _OverlayTex ("Overlay Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert alpha
            #pragma fragment frag alpha
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                //UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewdir : TEXCOORD2;

            };



            
            sampler2D _DisplacementTex;
            sampler2D _OverlayTex;
            //float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
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
                
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float fresnelAmount = dot(i.normal, i.viewdir);
                // sample the texture

                fixed4 col = tex2D(_OverlayTex, i.uv + _Time.y / 2);


                float alpha = clamp(1 - i.uv.x + col, 0.2f, 0.6f);
                float green = clamp(i.uv.x + col.x/2, 0.3f, 1);


                return fixed4(0, green,0, alpha);
                //return fresnelAmount;
            }
            ENDCG
        }
    }
}

