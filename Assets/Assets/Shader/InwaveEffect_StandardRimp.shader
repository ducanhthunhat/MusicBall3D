Shader "InwaveEffect/StandardRimp" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,0)
		[SingleLineTexture] _MainTexture ("Main Texture", 2D) = "white" {}
		[HDR] _EmissionColor ("Emission Color", Vector) = (0.3773585,0.3773585,0.3773585,0)
		[SingleLineTexture] _EmissionTexture ("Emission Texture", 2D) = "white" {}
		_RimplightColor ("Rimplight Color", Vector) = (0.3773585,0.3773585,0.3773585,1)
		_RimpPower ("RimpPower", Float) = 1
		_RimpThreshole ("RimpThreshole", Float) = 1
		[HideInInspector] _texcoord ("", 2D) = "white" {}
		[HideInInspector] __dirty ("", Float) = 1
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
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

			fixed4 _Color;

			float4 frag(Vertex_Stage_Output input) : SV_TARGET
			{
				return _Color; // RGBA
			}

			ENDHLSL
		}
	}
	//CustomEditor "ASEMaterialInspector"
}