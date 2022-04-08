Shader "MyShaders/Blured"{
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

                    float4 color1 = tex2D(_MainTex, v2f.uv);
                    float4 color2 = tex2D(_MainTex, v2f.uv + 0.005);
                    float4 color3 = tex2D(_MainTex, v2f.uv - 0.005);
                    float4 finalColor = (color1 + color2 + color3) / 3;

                    
                    return finalColor;
                }
            ENDHLSL
        }
        }
}