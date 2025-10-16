Shader "InwaveEffect/TH-Unlit" {
	Properties {
		_ColorMark ("Color Mark", Vector) = (1,1,1,1)
		_ColorTile1 ("Color Tile 1", Vector) = (1,1,1,1)
		_ColorTile2 ("Color Tile 2", Vector) = (1,1,1,1)
		_MainTexture ("Main Texture", 2D) = "white" {}
		_OpacityTexture ("Opacity Texture", 2D) = "white" {}
		_Opacity ("Opacity", Range(0, 1)) = 1
		[Toggle(_RIMPLIGHT_ON)] _RimpLight ("Rimp Light", Float) = 1
		_RimpColor ("Rimp Color", Vector) = (0.004716992,0.7491221,1,0)
		_RimpPower ("Rimp Power", Float) = 1
		_RimpScale ("Rimp Scale", Float) = 2
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