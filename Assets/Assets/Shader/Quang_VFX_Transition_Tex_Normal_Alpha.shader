Shader "Quang_VFX/Transition_Tex_Normal_Alpha" {
	Properties {
		[NoScaleOffset] _Base_Texture_1 ("Base_Texture_1", 2D) = "gray" {}
		[HDR] _Base_Tint_1 ("Base_Tint_1", Vector) = (1,1,1,0)
		[NoScaleOffset] _Base_Texture_2 ("Base_Texture_2", 2D) = "gray" {}
		[HDR] _Base_Tint_2 ("Base_Tint_2", Vector) = (1,1,1,0)
		_Transition_Day_Noon ("Transition_Day_Noon", Range(0, 1)) = 1
		_Alpha ("Alpha", Range(0, 1)) = 0
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		Pass
		{
			HLSLPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			float4x4 unity_MatrixMVP;

			struct Vertex_Stage_Input
			{
				float3 pos : POSITION;
			};

			struct Vertex_Stage_Output
			{
				float4 pos : SV_POSITION;
			};

			Vertex_Stage_Output vert(Vertex_Stage_Input input)
			{
				Vertex_Stage_Output output;
				output.pos = mul(unity_MatrixMVP, float4(input.pos, 1.0));
				return output;
			}

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return float4(1.0, 1.0, 1.0, 1.0); // RGBA
			}

			ENDHLSL
		}
	}
	//CustomEditor "ASEMaterialInspector"
}