// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/point Shader"
{
    Properties
    {
        _pointSize ("point Size", Range(0, 2)) = 2
        _NormalMap ("Normal Map", 2D) = "white" {}
        _smoothness("test value", Float) = 1.0
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

            uniform float _smoothness;
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

            float MinkowskiDistance(float2 point1, float2 point2, float p)
            {
                // Calculate the absolute differences raised to the power of p
                float deltaX = abs(point1.x - point2.x);
                float deltaY = abs(point1.y - point2.y);
                
                float distance = pow(deltaX, p) + pow(deltaY, p);
                
                // Take the p-th root of the sum
                return pow(distance, 1.0 / p);
            }



            float4 voronoiNoise(float2 value){
                float2 tile = floor(value);
                float2 tileFrac = frac( value );
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

                        //euclidian
                        float distToCore = length(core - value);

                        //manhattan
                        //float distToCore = abs(core.x - value.x) + abs(core.y - value.y);

                        //minkowski
                        //float distToCore = MinkowskiDistance(core, value, 3);
                        
                        // get min distance
                        
                        //distToCore = sin(distToCore + _Time.y);
                        if(distToCore < minDistToCore){
                            minDistToCore = distToCore;

                            closestCore = core;
                        }
                    }
                }


                if(minDistToCore < 0.07f)
                {
                    minDistToCore -= 0.9f;
                }

                float1 randomTint = (rand2dTo1d(floor(closestCore))) * 0.2f + 0.5f;

                float contrast = 0.3f;

                color = float4(1 - 0.4f*minDistToCore, 0.7f-minDistToCore * contrast, 0.7f-minDistToCore * contrast, 1.0) * randomTint;
                //color = float4(border,1,1,1);
                return color;
            }

            float voronoiDistance( float2 x )
            {
                float2 p = floor( x );
                float2 f = frac( x );

                float2 mb;
                float2 mr;

                float res = 8.0;
                for( int j=-1; j<=1; j++ )
                {
                    for( int i=-1; i<=1; i++ )
                    {
                        float2 b = float2(i, j);
                        //float2  r = float2(b) + rand2dTo2d(p+b)-f;
                        float2  r = b + 0.2f + 0.6f*rand2dTo2d(b+p) +0.2f*cos((rand2dTo2d(b+p) + brownianMotion(rand2dTo1d(b+p))));
                        r = MinkowskiDistance(r, f, 3);
                        
                        //float2  r = float2(b) + 0.2f + 0.6f*rand2dTo2d(p+b) + 0.2f*cos((rand2dTo2d(p+b) + brownianMotion(rand2dTo1d(p+b))))-f;
                        float d = dot(r,r);
                        //float d = length(r);
                        d = dot(d,d);

                        if( d < res )
                        {
                            res = d;
                            mr = r;
                            mb = b;
                        }
                    }
                }

                /*
                if(res > 0.2)
                {
                    res = 1;
                }
                else
                {
                    res = 0;
                }*/
                
                /*
                res = 8.0;
                for( int j=-2; j<=2; j++ )
                {
                    for( int i=-2; i<=2; i++ )
                    {
                        float2 b = mb + float2(i, j);
                        float2  r = b + 0.2f + 0.6f*rand2dTo2d(b+p) +0.2f*cos((rand2dTo2d(b+p) + brownianMotion(rand2dTo1d(b+p))));
                        //r = MinkowskiDistance(r, f, 3);
                        //float d = length(r - x);
                        float d = dot(0.5*(mr+r), normalize(r-mr));

                        res = min( res, d );
                    }
                }
                    */
                    
                

                float border = 1.0 - smoothstep(0.0,0.2,res);

                return res;
            }

            // exponential
            float smin( float a, float b, float k )
            {
                k *= 1.0;
    float r = exp2(-a/k) + exp2(-b/k);
    return -k*log2(r);
            }


            float voronoiSubtract(float2 value)
            {
                float2 tile = floor(value);
            
                float minDistToCore = 10;
                float minDistToCoreSmooth = 10;
                float2 closestCore;
                float res;
                [unroll]
                for(int x=-2; x<=2; x++){
                    [unroll]
                    for(int y=-2; y<=2; y++){
                        float2 core = tile + float2(x, y);
                        core = core + 0.2f + 0.9f*rand2dTo2d(core) + 0.4f*cos((rand2dTo2d(core) + brownianMotion(rand2dTo1d(core))));

                        //float distToCore = length(core - value);

                        //manhattan
                        //float distToCore = abs(core.x - value.x) + abs(core.y - value.y);

                        //minkowski
                        float distToCore = MinkowskiDistance(core, value, 3);
                        
                        // get min distance
                        
                        //distToCore = sin(distToCore + _Time.y);
                        minDistToCore = min(distToCore, minDistToCore);
                        minDistToCoreSmooth = smin(distToCore, minDistToCoreSmooth, _smoothness);
                        //res = min(distToCore, minDistToCore) - smin(distToCore, minDistToCore, _smoothness)/2;
                        //res = smin(distToCore, minDistToCore, _smoothness);
                        if(distToCore < minDistToCore){
                            //minDistToCore = distToCore;

                            //closestCore = core;
                        }
                    }
                }

                res = minDistToCore - minDistToCoreSmooth * 0.8;

                
                if(res < 0.13f)
                {
                    res = 1;
                }
                else if(res > 0.3f)
                {
                    res = 0;
                }
                    

                return res;
            }

            float getBorder( float2 p )
            {
                float d = voronoiDistance( p );

                return 1.0 - smoothstep(0.0,0.05,d);
            }

            float3 PerturbNormal ( float3 position ,float3 normal , float height )
            {
                float3 dpdx = ddx(position);
                float3 dpdy = ddy(position);
            
                float dhdx = ddx(height);
                float dhdy = ddy(height);

                float3 r1 = cross(dpdy, normal);
                float3 r2 = cross(normal, dpdx);

                float3 surfaceGradient = (r1 * dhdx + r2 * dhdy) / dot(dpdx, r1);

                float3 perturbNormal = normalize(normal - surfaceGradient);

                return perturbNormal;
            }

            float3 Unity_NormalFromHeight_World_float(float In, float Strength, float3 Position, float3 normal)
            {
                float3 worldDerivativeX = ddx(Position);
                float3 worldDerivativeY = ddy(Position);

                float3 crossX = cross(normal, worldDerivativeX);
                float3 crossY = cross(worldDerivativeY, normal);
                float d = dot(worldDerivativeX, crossY);
                float sgn = d < 0.0 ? (-1.0f) : 1.0f;
                float surface = sgn / max(0.000000000000001192093f, abs(d));

                float dHdx = ddx(In);
                float dHdy = ddy(In);
                float3 surfGrad = surface * (dHdx*crossY + dHdy*crossX);
                
                float3 returnNormal = normalize(normal - (Strength * surfGrad));
                return mul(returnNormal, normal);
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
			    //float4 noise = voronoiNoise(value);
                float4 noise = voronoiSubtract(value);

                float4 unlitColor = noise;
                float4 normal = tex2D(_NormalMap, v.uv) * 2 - 1;
                float heightMap = tex2D(_NormalMap, v.uv);
                float height = noise.y * 0.1f;
                //float3 normal = PerturbNormal(v.uv, v.worldNormal, heightMap);
                //float3 normal = Unity_NormalFromHeight_World_float(heightMap, 0.01f, v.uv, v.worldNormal);
                //float3 normal = float4(heightMap, heightMap, heightMap, 1);
                //float3 normal = v.worldNormal;


                float3 worldNormal = v.worldNormal;
                float3 worldBinormal = v.worldBinormal;
                float3 worldTangent = v.worldTangent;
                //float3 interpNormal = float4(noise.x, noise.y, noise.y, 1);
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


                //float4 testcolor = float4(height, height, height, 1);
                float4 testcolor = float4(normal.x, normal.y, normal.z, 1);
                float4 diffuse = dot(normal, _WorldSpaceLightPos0);
                return noise;
            } 

            ENDCG
        }
    }
    FallBack "Standard"
    
}
