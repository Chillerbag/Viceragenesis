Shader "Unlit/Cell Shader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            

            // Get 2D rotation matrix given angle (radians).

            // c, -s, s, c = clockwise.
            // c, s, -s, c = counterclockwise.

            float2x2 Get2DRotationMatrix(float angle)
            {
                float c = cos(angle);
                float s = sin(angle);

                return float2x2(c, -s, s, c);
            }

            // Output this function directly (default values only for reference).

            float GetAnimatedOrganicFractal(float2 uv)
            {
                float scale = 6;
                float scaleMultStep = 1.2;
                float rotationStep = 5;
                int iterations = 10;
                float uvAnimationSpeed = 3.5;

                float rippleStrength = 0.9;
                float rippleMaxFrequency = 1.4;
                float rippleSpeed = 5;

                float brightness = 2;
                // Remap to [-1.0, 1.0].

                //uv = float2(uv - 0.5) * 2.0;

                float2 n, q;
                float invertedRadialGradient = pow(length(uv), 2.0);

                float output = 0.0;
                float2x2 rotationMatrix = Get2DRotationMatrix(rotationStep);

                float t = _Time.y;
                float uvTime = t * uvAnimationSpeed;

                // Ripples can be pre-calculated and passed from outside.
                // They don't need to be here in this function.

                //float ripples = sin((t * rippleSpeed) - (invertedRadialGradient * rippleMaxFrequency)) * rippleStrength;
                float ripples = 0.1f;

                for (int i = 0; i < iterations; i++)
                {
                    uv = mul(rotationMatrix, uv);
                    n = mul(rotationMatrix, n);

                    float2 animatedUV = (uv * scale) + uvTime;

                    q = animatedUV + ripples + i + n;
                    output += dot(cos(q) / scale, float2(1.0, 1.0) * brightness);

                    n -= sin(q);

                    scale *= scaleMultStep;
                }
                    
                return output;
            }

            inline float2 voronoi_noise_randomVector (float2 UV, float offset){
                float2x2 m = float2x2(15.27, 47.63, 99.41, 89.98);
                UV = frac(sin(mul(UV, m)) * 46839.32);
                return float2(sin(UV.y*+offset)*0.5+0.5, cos(UV.x*offset)*0.5+0.5);
            }
             
            void Voronoi_float(float2 UV, float AngleOffset, float CellDensity, out float Out, out float Cells) {
                float2 g = floor(UV * CellDensity);
                float2 f = frac(UV * CellDensity);
                float2 res = float2(8.0, 8.0);
                float2 ml = float2(0,0);
                float2 mv = float2(0,0);
             
                for(int y=-1; y<=1; y++){
                    for(int x=-1; x<=1; x++){
                        float2 lattice = float2(x, y);
                        float2 offset = voronoi_noise_randomVector(g + lattice, AngleOffset);
                        float2 v = lattice + offset - f;
                        float d = dot(v, v);
             
                        if(d < res.x){
                            res.x = d;
                            res.y = offset.x;
                            mv = v;
                            ml = lattice;
                        }
                    }
                }
             
                Cells = res.y;
             
                res = float2(8.0, 8.0);
                for(int y=-2; y<=2; y++){
                    for(int x=-2; x<=2; x++){
                        float2 lattice = ml + float2(x, y);
                        float2 offset = voronoi_noise_randomVector(g + lattice, AngleOffset);
                        float2 v = lattice + offset - f;
             
                        float2 cellDifference = abs(ml - lattice);
                        if (cellDifference.x + cellDifference.y > 0.1){
                            float d = dot(0.5*(mv+v), normalize(v-mv));
                            res.x = min(res.x, d);
                        }
                    }
                }
             
                Out = res.x;
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
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return GetAnimatedOrganicFractal(i.uv);
            }

            ENDCG
        }
    }

    
}
