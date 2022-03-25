Shader "MyShaders/Texture"{
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture",2D) = "white"{}
	}
		SubShader{
			Pass{
				//Here code
				HLSLPROGRAM
				#pragma vertex MyVertexProgram
				#pragma fragment MyFragmentProgram
				#include "UnityCG.cginc"

				struct VertexData {
					float4 position : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				struct VertexToFragment
				{
					float4 position : SV_POSITION;
					
					float2 uv : TEXCOORD0;

				};
				//ACCESS COLORS
				float4 _Color;
				//ACCESS TEXTURES
				sampler2D _MainTex;
				//ACCESS TILING DATA
				float4 _MainTex_ST;
				//Here program vertexes and then fragments without it, this thing wont work.
				VertexToFragment MyVertexProgram(VertexData vertex) 
				{
					VertexToFragment v2f;
					v2f.position = UnityObjectToClipPos(vertex.position);
					v2f.uv = vertex.uv * _MainTex_ST.xy + _MainTex_ST.zw;
					return v2f;
				}
					float4 MyFragmentProgram(VertexToFragment v2f) : SV_TARGET
				{
					return  tex2D(_MainTex, v2f.uv)  * _Color;
				}



			ENDHLSL
		}
	}
}
