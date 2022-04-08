Shader "MyShaders/TerrainManipulation"{
    Properties{
        _Color("Color", Color) = (1,1,1,1)
        _MainTex("Texture", 2D) = "white"{}
        _NoiseTex("Noise Texture", 2D) = "white"{}
        _DissolveThreshold("Dissolve Threshold", Range(0, 1)) = 0.5
        
    }
        SubShader{
            Pass{
                HLSLPROGRAM
                #pragma vertex MyVertexProgram
                #pragma fragment MyFragmentProgram

                #include "UnityCG.cginc"

                struct VertexData {
                    float4 position : POSITION;
                    float2 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct VertexToFragment {
                    float4 position : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float2 uvNoise : TEXCOORD1;
                };

                float4 _Color;
                sampler2D _MainTex, _NoiseTex;
                float4 _MainTex_ST, _NoiseTex_ST;
                float _DissolveThreshold;
                
                

                VertexToFragment MyVertexProgram(VertexData vertex)
                {
                    VertexToFragment v2f;
                    v2f.uvNoise = vertex.uv * _NoiseTex_ST.xy + _NoiseTex_ST.zw;
                    float4 noiseColor = tex2Dlod(_MainTex, float4(v2f.uvNoise,0,0));
                    vertex.position.y += noiseColor.r;
                    v2f.position = UnityObjectToClipPos(vertex.position);
                    v2f.uv = vertex.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                    
                    return v2f;
                }

                float4 MyFragmentProgram(VertexToFragment v2f) : SV_TARGET
                {
                    float4 color = tex2D(_MainTex, v2f.uv);
                    //float4 noise = tex2D(_NoiseTex, v2f.uvNoise);
                    //clip(noise.rgb - _DissolveThreshold);
                    return color * _Color;
                }
            ENDHLSL
        }
        }
}