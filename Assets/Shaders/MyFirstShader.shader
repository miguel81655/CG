Shader "MyShaders/FirstShader"{
	Properties{
		_Color("Color", Color) = (1,1,1,1)
	}
		SubShader{
			Pass{
				//Here code
				HLSLPROGRAM
				#pragma vertex MyVertexProgram
				#pragma fragment MyFragmentProgram
				#include "UnityCG.cginc"

				struct VertexToFragment
				{
					float4 position : SV_POSITION;
					
					float4 localposition : TEXCOORD0;

				};

				float4 _Color;
				//Here program vertexes and then fragments without it, this thing wont work.
				VertexToFragment MyVertexProgram(float4 position : POSITION) 
				{
					VertexToFragment v2f;
					v2f.localposition = position;
					v2f.position = UnityObjectToClipPos(position);
					return v2f;
				}
					float4 MyFragmentProgram(VertexToFragment v2f) : SV_TARGET
				{
					return v2f.localposition * _Color;
				}



			ENDHLSL
		}
	}
}
