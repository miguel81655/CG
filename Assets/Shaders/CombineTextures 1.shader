Shader "MyShaders/Combine Texture"{
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Texture",2D) = "white"{}
		_DetailTex("Detail Texture",2D) = "white"{}
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
					float2 uvDetail : TEXCOORD1;

				};
				//ACCESS COLORS
				float4 _Color;
				//ACCESS TEXTURES
				sampler2D _MainTex, _DetailTex;
				//ACCESS TILING DATA
				float4 _MainTex_ST, _DetailTex_ST;
				//Here program vertexes and then fragments without it, this thing wont work.
				VertexToFragment MyVertexProgram(VertexData vertex) 
				{
					VertexToFragment v2f;
					v2f.position = UnityObjectToClipPos(vertex.position);
					v2f.uv = vertex.uv * _MainTex_ST.xy + _MainTex_ST.zw;
					v2f.uvDetail = vertex.uv * _DetailTex_ST.xy + _DetailTex_ST.zw;
					return v2f;
				}
				float4 MyFragmentProgram(VertexToFragment v2f) : SV_TARGET
				{
					float4 color = tex2D(_MainTex, v2f.uv);
					color *= tex2D(_DetailTex, v2f.uvDetail) * unity_ColorSpaceDouble;
					return color * _Color;
				}



			ENDHLSL
		}
	}
}
