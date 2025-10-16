Shader "Anzu/Video LED Playback Shader" {
	Properties {
		_ShouldFlipX ("Should Flip X", Float) = 0
		_ShouldFlipY ("Should Flip Y", Float) = 0
		_ShouldSwitchRB ("Should switch R/B", Float) = 0
		_HorizonalLeds ("Horizontal Resolution", Float) = 80
		_VerticalLeds ("Vertical Resolution", Float) = 60
		_Color ("Glow Color", Vector) = (1,1,1,1)
		_Glow ("Glow Intensity", Range(0, 3)) = 1
		_LuminanceSteps ("Luma Steps", Float) = 12
		_Brightness ("Brightness", Float) = 0
		_Contrast ("Contrast", Float) = 1
		_MainTex ("Base (RGB)", 2D) = "black" {}
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

			Texture2D<float4> _MainTex;
			SamplerState _MainTex_sampler;
			fixed4 _Color;

			struct Fragment_Stage_Input
			{
				float2 uv : TEXCOORD0;
			};

			float4 frag(Fragment_Stage_Input input) : SV_TARGET
			{
				return _MainTex.Sample(_MainTex_sampler, float2(input.uv.x, input.uv.y)) * _Color;
			}

			ENDHLSL
		}
	}
	Fallback "Diffuse"
}