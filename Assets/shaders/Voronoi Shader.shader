// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/point Shader"
{
    Properties
    {
        _pointSize ("cell Size", Range(0, 2)) = 2
        _coreSize ("core Size", Range(0, 2)) = 0
        _smoothness("edge width", Float) = 1.0
        _borderWidth("border width", Float) = 1.0
        _cellColor("cell color", Color) = (1,1,1)
        _edgeColor("edge color", Color) = (1,1,1)
        _voidColor("void color", Color) = (1,1,1)
        _coreColor("core color", Color) = (1,1,1)
        _voidTex ("Void Texture", 2D) = "white" {}

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
            #include "UnityLightingCommon.cginc"

            

            struct vertIn
            {
                float4 vertex : POSITION;
				float4 normal : NORMAL;
                float4 tangent : TANGENT;
				float4 color : COLOR;
                float4 uv : TEXCOORD0;
            };

            struct vertOut
            {
                float4 vertex : SV_POSITION;
				float4 color : COLOR;
				float4 worldVertex : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
                float3 worldBinormal : TEXCOORD2;
                float3 worldTangent : TEXCOORD3;
                float4 uv : TEXCOORD4;
            };

            float _pointSize;
            float _smoothness;
            float _coreSize;
            float _borderWidth;
            float4 _cellColor;
            float4 _voidColor;
            float4 _edgeColor;
            float4 _coreColor;
            sampler2D _voidTex;

            float brownianMotion(float x)
            {

                float amplitude = x * 4 + x;
                float frequency = x * 5 + x;
                float y = sin(x * frequency);
                float t = 0.01*(-_Time.y*(130.0 + x * 2));
                y += sin(x*frequency*2.1 + t)*4.5 + x;
                y += sin(x*frequency*x + t*1.121)*4.0;
                y += sin(x*frequency*2.221 + t*0.437)*5.0 * x;
                y += sin(x*frequency*3.1122+ t*4.269)*x + x * 3;
                y *= amplitude*0.06;
                return y;
            }

            float MinkowskiDistance(float2 point1, float2 point2, float p)
            {
                // Calculate the absolute differences raised to the power of p
                float deltaX = abs(point1.x - point2.x);
                float deltaY = abs(point1.y - point2.y);
                
                float distance = pow(deltaX, p) + pow(deltaY, p);
                
                // Take the p-th root of the sum
                return pow(distance, 1.0 / p);
            }


            // exponential
            float smin( float a, float b, float k )
            {
                k *= 1.0;
                float r = exp2(-a/k) + exp2(-b/k);
                return -k*log2(r);
            }


            float4 voronoiSubtract(float2 value, float4 voidColor)
            {
                float2 tile = floor(value);
                float4 color = float4(0.0, 0.0, 0.0, 1.0);
            
                float minDistToCore = 10;
                float minDistToCoreSmooth = 10;
                float2 closestCore;
                float res;
                [unroll]
                for(int x=-2; x<=2; x++){
                    [unroll]
                    for(int y=-2; y<=2; y++){
                        float2 core = tile + float2(x, y);
                        core = core + 0.3f + 0.4f*rand2dTo2d(core) + 0.3f*cos((rand2dTo2d(core) + brownianMotion(rand2dTo1d(core))));

                        //euclidian
                        //float distToCore = length(core - value);

                        //manhattan
                        //float distToCore = abs(core.x - value.x) + abs(core.y - value.y);

                        //minkowski
                        float distToCore = MinkowskiDistance(core, value, 3);
                        
                        // get min distance
                        
                        //distToCore = sin(distToCore + _Time.y);
                        minDistToCore = min(distToCore, minDistToCore);
                        minDistToCoreSmooth = smin(distToCore, minDistToCoreSmooth, _smoothness);

                        if(distToCore == minDistToCore){
                            closestCore = core;
                        }
                    }
                }

                res = minDistToCore - minDistToCoreSmooth * 0.8;


                float contrast = 0.3f;
                float1 randomTint = (rand2dTo1d(floor(closestCore))) * 0.2f + 0.5f;
                
                if(res < 0.13f)
                {
                    color = float4(1-minDistToCore*0.8f, 1-minDistToCore*0.8f, 1-minDistToCore*0.8f, 1)*0.8f + _cellColor * randomTint;
                }
                else if(res > 0.13f + _borderWidth)
                {
                    color = voidColor * _voidColor;
                    
                }
                else
                {
                    color = float4(res,res,res,1) + _edgeColor;
                }

                if(minDistToCore < _coreSize)
                {
                    color = _coreColor;
                }
    

                return color;
            }

            vertOut vert (vertIn v)
            {
                float3 binormal = cross( v.normal, v.tangent.xyz ) * v.tangent.w;
				vertOut o;

				// Convert Vertex position and corresponding normal into world coords.
				// Note that we have to multiply the normal by the transposed inverse of the world 
				// transformation matrix (for cases where we have non-uniform scaling; we also don't
				// care about the "fourth" dimension, because translations don't affect the normal) 
				float4 worldVertex = mul(unity_ObjectToWorld, v.vertex);

				float3 worldNormal = normalize(mul(transpose((float3x3)unity_WorldToObject), v.normal.xyz));
                float3 worldBinormal = normalize(mul(transpose((float3x3)unity_WorldToObject), binormal.xyz));
                float3 worldTangent = normalize(mul(transpose((float3x3)unity_WorldToObject), v.tangent.xyz));


				// Transform vertex in world coordinates to camera coordinates, and pass colour
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.color = v.color;

				// Pass out the world vertex position and world normal to be interpolated
				// in the fragment shader (and utilised)
				o.worldVertex = worldVertex;
				o.worldNormal = worldNormal;
                o.worldBinormal = worldBinormal;
                o.worldTangent = worldTangent;
                o.uv = v.uv;

				return o;
            }

            fixed4 frag (vertOut v) : SV_Target
            {

                float2 value = float2(v.uv.x / _pointSize, v.uv.y / (_pointSize / 1.5));
                float4 voidColor = tex2D(_voidTex, v.uv);
                float4 noise = voronoiSubtract(value, voidColor);

                
                return noise;
            } 

            ENDCG
        }
    }
    FallBack "Standard"
    
}
