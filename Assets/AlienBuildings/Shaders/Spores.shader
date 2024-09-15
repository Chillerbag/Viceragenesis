Shader "Custom/Spores"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        //_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_MetallicSmoothness("Metallic(RGB) Smoothness(A)", 2D) = "black" {}
		_BumpMap("Normalmap", 2D) = "bump" {}
		_Chunkiness("Propagation Chunkiness", 2D) = "black" {}
		_CirclePos("Circle positions", Vector) = (0,0,0,0)
		_CircleSize("Circle sizes", Vector) = (0.15,0.15,0.15,0.15)
		_CircleBlur("Circle blur", float) = 0.05
    }
    SubShader
    {
		Tags {"Queue" = "AlphaTest+49" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
		LOD 100
		ZTest Less
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows alpha:fade
		#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight
        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex, _MetallicSmoothness, _BumpMap, _Chunkiness;

        struct Input
        {
            float2 uv_MainTex, uv_BumpMap;
        };

        //half _Glossiness;
        //half _Metallic;
        fixed4 _Color;
		float4 _CirclePos, _CircleSize;
		float _CircleBlur;
        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
			fixed4 ms = tex2D(_MetallicSmoothness, IN.uv_MainTex);
            // Metallic and smoothness come from slider variables
            o.Metallic = ms.r;
            o.Smoothness = ms.a;
			o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
			//don't batch by min, do 2 passes, max on alpha)
			//float dst = min(distance(IN.uv_MainTex, _CirclePos.xy), distance(IN.uv_MainTex, _CirclePos.zw));
			float dst1 = distance(IN.uv_BumpMap, _CirclePos.xy);
			float dst2 = distance(IN.uv_BumpMap, _CirclePos.zw);
			float alpha1=0, alpha2=0;
			float extranormal = 0;
			float chunk = tex2D(_Chunkiness, IN.uv_MainTex).r;
			if (dst1 < _CircleSize.x)
			{
				float dstEdge = _CircleSize.x - dst1;
				if (dstEdge> _CircleBlur)
				{
					alpha1 = 1;
				}
				else
				{
					float ta1 = dstEdge / _CircleBlur;
					extranormal = max(extranormal, (1.0 - abs(ta1*2.0 - 1.0)));
					alpha1 = dstEdge / _CircleBlur/1.5;
					alpha1 = clamp(alpha1 - chunk * (1 - (ta1*2)),0,1);
				}
			}
			if (dst2 < _CircleSize.y)
			{
				float dstEdge2 = _CircleSize.y - dst2;
				if (dstEdge2 > _CircleBlur)
				{
					alpha2 = 1;
					
				}
				else
				{
					float ta2 = dstEdge2 / _CircleBlur;
					extranormal = max(extranormal, (1.0 - abs(ta2*2.0 - 1.0)));
					alpha2 = dstEdge2 / _CircleBlur / 1.5;
					//alpha2 = dstEdge2 / _CircleBlur;
					alpha2 = clamp(alpha2 - chunk * (1 - (ta2 * 2)), 0, 1);
				}
				//alpha2 = 1;
			}
			o.Normal.y =  o.Normal.y+(extranormal*0.1);
			
            o.Alpha = clamp(max(alpha1,alpha2)-((o.Albedo.r+ o.Albedo.g+ o.Albedo.b)/3),0,1);
			//o.Normal = lerp(float3(0, 0, 0), o.Normal, o.Alpha);
        }
        ENDCG
    }
    FallBack "Diffuse"
}
