Shader "InwaveEffect/Holofoil" {
	Properties {
		_Color ("Color", Vector) = (0.5019608,0.5019608,0.5019608,0)
		_Texture ("Texture", 2D) = "white" {}
		_Mask ("Mask", 2D) = "white" {}
		_FoilAlpha ("Foil Alpha", Range(0, 1)) = 1
		_FoilVibrance ("Foil Vibrance", Range(0, 1)) = 1
		_RimplightColor ("Rimplight Color", Vector) = (0.3773585,0.3773585,0.3773585,1)
		_RimpPower ("RimpPower", Float) = 1
		_RimpThreshole ("RimpThreshole", Float) = 1
		_WarpTexture ("Warp Texture", 2D) = "bump" {}
		_HoloLightTexture ("HoloLight Texture", 2D) = "white" {}
		_MapScale ("Map Scale", Float) = 1
		_MapLevel ("Map Level", Float) = 2
		_MapOffset ("Map Offset", Vector) = (0,0,0,0)
		_DistorsionDistance ("Distorsion Distance", Float) = 1
		[HideInInspector] _texcoord ("", 2D) = "white" {}
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