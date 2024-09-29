// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/point Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _pointSize ("point Size", Range(0, 2)) = 2
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            // Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members worldPos)
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Random.cginc"

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


            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _pointSize;

            float brownianMotion(float x)
            {

                float amplitude = x * 4;
                float frequency = x * 100;
                float y = sin(x * frequency);
                float t = 0.01*(-_Time.y*130.0);
                y += sin(x*frequency*2.1 + t)*4.5;
                y += sin(x*frequency*1.72 + t*1.121)*4.0;
                y += sin(x*frequency*2.221 + t*0.437)*5.0;
                y += sin(x*frequency*3.1122+ t*4.269)*2.5;
                y *= amplitude*0.06;
                return y;
            }


            float4 voronoiNoise(float2 value){
                float2 tile = floor(value);
                float4 color = float4(0.0, 0.0, 0.0, 1.0);
            
                float minDistToCore = 10;
                float2 closestCore;
                [unroll]
                for(int x=-1; x<=1; x++){
                    [unroll]
                    for(int y=-1; y<=1; y++){
                        // get neighbor point
                        float2 core = tile + float2(x, y);
                        // random position
                        core = core + 0.2f + 0.6f*rand2dTo2d(core) + 0.2f*cos((rand2dTo2d(core) + brownianMotion(rand2dTo1d(core))));
                        //corePosition = 0.5f + 0.5f*corePosition;

                        // vector distance between the pixel and the point core
                        float distToCore = length(core - value);
                        // get min distance
                        
                        //distToCore = sin(distToCore + _Time.y);
                        if(distToCore < minDistToCore){
                            minDistToCore = distToCore;
                            closestCore = core;
                        }
                    }
                }
                float1 randomTint = (rand2dTo1d(floor(closestCore))) * 0.1f + 0.6f;

                color = float4(1 - 0.1f*randomTint, 1-minDistToCore, 1-minDistToCore, 1.0) * randomTint;
                return color;
            }       
            

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float3 worldpos = mul(unity_ObjectToWorld, i.vertex);
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);


                float2 value = i.uv.xy / _pointSize;
			    float4 noise = voronoiNoise(value);
                return noise;
            } 

            ENDCG
        }
    }
    FallBack "Standard"
    
}
