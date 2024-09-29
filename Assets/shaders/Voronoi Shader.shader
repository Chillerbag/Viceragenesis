// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/point Shader"
{
    Properties
    {
        _pointSize ("point Size", Range(0, 2)) = 2
        _NormalMap ("Normal Map", 2D) = "white" {}
		_Ka("Ka", Float) = 1.0
		_Kd("Kd", Float) = 1.0
		_Ks("Ks", Float) = 1.0
		_fAtt("fAtt", Float) = 1.0
		_specN("specN", Float) = 1.0
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

            uniform float _Ka;
			uniform float _Kd;
			uniform float _Ks;
			uniform float _fAtt;
			uniform float _specN;

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
            sampler2D _NormalMap;

            float brownianMotion(float x)
            {

                float amplitude = x * 4 + 2;
                float frequency = x * 2 + 2;
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
                float2 value = v.uv.xy / _pointSize;
			    float4 noise = voronoiNoise(value);

                float4 unlitColor = noise;
                float4 normal = tex2D(_NormalMap, v.uv) * 2 - 1;

                float3 worldNormal = v.worldNormal;
                float3 worldBinormal = v.worldBinormal;
                float3 worldTangent = v.worldTangent;

                float3 interpNormal = (normal.z * worldNormal) + (normal.x * -worldTangent) + (normal.y * worldBinormal);

                interpNormal = normalize(interpNormal);
				// Our interpolated normal might not be of length 1
				//float3 interpNormal = normalize(v.worldNormal);

				// Calculate ambient RGB intensities
				float Ka = _Ka;
				float3 amb = unlitColor.rgb * UNITY_LIGHTMODEL_AMBIENT.rgb * Ka;

				// Calculate diffuse RBG reflections, we save the results of L.N because we will use it again
				// (when calculating the reflected ray in our specular component)
				float fAtt = _fAtt;
				float Kd = _Kd;
				float3 L = _WorldSpaceLightPos0; // Q6: Using built-in Unity light data: _WorldSpaceLightPos0.
				                                 // Note that we are using a *directional* light in this instance,
												 // so _WorldSpaceLightPos0 is actually a direction rather than
												 // a point. Therefore there is no need to subtract the world
												 // space vertex position like in our point-light shaders.
				float LdotN = dot(L, interpNormal);
				float3 dif = fAtt * _LightColor0 * Kd * unlitColor.rgb * saturate(LdotN); // Q6: Using built-in Unity light data: _LightColor0

				// Calculate specular reflections
				float Ks = _Ks;
				float specN = _specN; // Values>>1 give tighter highlights
				float3 V = normalize(_WorldSpaceCameraPos - v.worldVertex.xyz);
                // Using classic reflection calculation:
				float3 R = normalize((2.0 * LdotN * interpNormal) - L);
				float3 spe = fAtt * Ks * pow(saturate(dot(V, R)), specN);
				// Using Blinn-Phong approximation:
				//specN = _specN; // We usually need a higher specular power when using Blinn-Phong
				//float3 H = normalize(V + L);
				//float3 spe = fAtt * _LightColor0 * Ks * pow(saturate(dot(interpNormal, H)), specN); // Q6: Using built-in Unity light data: _LightColor0

				// Combine Phong illumination model components
				float4 returnColor = float4(0.0f, 0.0f, 0.0f, 0.0f);
				returnColor.rgb = amb.rgb + dif.rgb + spe.rgb;
				returnColor.a = unlitColor.a;



                
                return returnColor;
            } 

            ENDCG
        }
    }
    FallBack "Standard"
    
}
