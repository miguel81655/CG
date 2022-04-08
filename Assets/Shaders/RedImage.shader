Shader "MyShaders/RedEffect"{
    Properties{
        _MainTex("Texture", 2D) = "white"{}
    }
        SubShader{
            Cull off
            ZWrite off
            ZTest Always
            Pass{
                HLSLPROGRAM
                #pragma vertex MyVertexProgram
                #pragma fragment MyFragmentProgram

                #include "UnityCG.cginc"

                struct VertexData {
                    float4 position : POSITION;
                    float2 uv : TEXCOORD0;
                };

                struct VertexToFragment {
                    float4 position : SV_POSITION;
                    float2 uv : TEXCOORD0;
                };

   
                sampler2D _MainTex;
                float4 _MainTex_ST;
                float _DissolveThreshold;            
                

                VertexToFragment MyVertexProgram(VertexData vertex)
                {
                    VertexToFragment v2f;
                    v2f.position = UnityObjectToClipPos(vertex.position);
                    v2f.uv = vertex.uv * _MainTex_ST.xy + _MainTex_ST.zw;
                    
                    return v2f;
                }

                float4 MyFragmentProgram(VertexToFragment v2f) : SV_TARGET
                {

                    float4 color = tex2D(_MainTex, v2f.uv);
                    

                    
                    return color * float4(1,0,0,1);
                }
            ENDHLSL
        }
        }
}