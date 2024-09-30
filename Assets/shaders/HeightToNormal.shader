Shader "Unlit/HeightToNormal"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "white" {}
        _Color("Color", Color) = (1,1,1,1)
        _NormalIntensity("NormalIntensity",Float) = 1
        _HeightMapSizeX("HeightMapSizeX",Float) = 1024
        _HeightMapSizeY("HeightMapSizeY",Float) = 1024
    }
    SubShader
    {

        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            struct VertexInput {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;
                float3 tangent : TANGENT;

            };

            struct VertexOutput {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float2 uv1 : TEXCOORD1;
                float4 normals : NORMAL;

                //float3 tangentSpaceLight: TANGENT;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Color;
            float _NormalIntensity;
            float _HeightMapSizeX;
            float _HeightMapSizeY;

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

            float4 frag(VertexOutput i) : COLOR
            {
                float me = tex2D(_MainTex,i.uv1).x;
                float n = tex2D(_MainTex,float2(i.uv1.x, i.uv1.y + 1.0 / _HeightMapSizeY)).x;
                float s = tex2D(_MainTex,float2(i.uv1.x, i.uv1.y - 1.0 / _HeightMapSizeY)).x;
                float e = tex2D(_MainTex,float2(i.uv1.x + 1.0 / _HeightMapSizeX,i.uv1.y)).x;
                float w = tex2D(_MainTex,float2(i.uv1.x - 1.0 / _HeightMapSizeX,i.uv1.y)).x;

                // defining starting normal as color has some interesting effects, generally makes this more flexible
                float3 norm = _Color;
                float3 temp = norm; //a temporary vector that is not parallel to norm
                if (norm.x == 1)
                temp.y += 0.5;
                else
                temp.x += 0.5;

                //form a basis with norm being one of the axes:
                float3 perp1 = normalize(cross(i.normals,temp));
                float3 perp2 = normalize(cross(i.normals,perp1));

                //use the basis to move the normal i its own space by the offset
                float3 normalOffset = -_NormalIntensity * (((n - me) - (s - me)) * perp1 + ((e - me) - (w - me)) * perp2);
                norm += normalOffset;
                norm = normalize(norm);

                float3 interpNormal = (norm.z * worldNormal) + (norm.x * -worldTangent) + (norm.y * worldBinormal);

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
				//returnColor.a = unlitColor.a;

                // it's also interesting to output temp, perp1, and perp1, or combinations of the float samples.
                return float4(norm, 1);
            }
            ENDCG
         }
    }
}