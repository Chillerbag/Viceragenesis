// Original Cg/HLSL code stub copyright (c) 2010-2012 SharpDX - Alexandre Mutel
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
// 
// Adapted for COMP30019 by Jeremy Nicholson, 10 Sep 2012
// Adapted further by Chris Ewin, 23 Sep 2013
// Adapted further (again) by Alex Zable (port to Unity), 19 Aug 2016

// Challenge: Task 6.
//
// This shader is a variation on PhongShader.shader to utilise built-in
// Unity lights in the shader. A full reference for the relevant uniform
// parameters can be found at (under "Lighting"): 
// https://docs.unity3d.com/Manual/SL-UnityShaderVariables.html
//
// The two variables we utilise are:
// _WorldSpaceLightPos0
// _LightColor0
//
// Note we are using a Unity "directional" light here. This means the 
// incoming light "position" data is actually a vector representing the
// light direction rather than an actual world-space position. As an 
// experiment, try moving the UnityLight object in the game and notice
// how it has no effect on the lighting for the cube using this shader. 
// (Only *rotating* it will have an effect.)

//UNITY_SHADER_NO_UPGRADE

Shader "Unlit/PhongShaderUnityLights"
{
	Properties
	{
        _MainTex ("Texture", 2D) = "white" {}
        _NormalMap ("Texture", 2D) = "white" {}
		_Ka("Ka", Float) = 1.0
		_Kd("Kd", Float) = 1.0
		_Ks("Ks", Float) = 1.0
		_fAtt("fAtt", Float) = 1.0
		_specN("specN", Float) = 1.0
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
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

            sampler2D _NormalMap;
            sampler2D _MainTex;

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
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
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
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

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
                float4 unlitColor = tex2D(_MainTex, v.uv);
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
}