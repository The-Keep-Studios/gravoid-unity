// Modifications:
//
// Date:
// What was done

// 7/25/2013
// Created the Asteroid Shader which works with 3 textures to create a mixed asteroid

Shader "Asteroid Shader" {
	Properties {
		_MainTex ("Texture for white (1)", 2D) = "white" {}
		_OtherTex ("Texture for black (0)", 2D) = "white" {}
		_MixTex ("Black and White map to mix", 2D) = "white" {}
	}
	
	SubShader {
		Pass {
				
			CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag		
			
			uniform sampler2D _MainTex; //Will be the texture applied to white
			uniform sampler2D _OtherTex; //Will be the texture applied to black
			uniform sampler2D _MixTex;
			
			struct vertexInput {
				float4 vertex : POSITION;
				float4 texcoord : TEXCOORD0;
			};
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 tex : TEXCOORD0;							
			};
			
			vertexOutput vert (vertexInput vertIn)
			{
				vertexOutput vertOut;
				
				float4x4 modelMatrix = _Object2World;
				float4x4 modelMatrixInverse = _World2Object;
				
				vertOut.tex = vertIn.texcoord;
				vertOut.pos = mul(UNITY_MATRIX_MVP, vertIn.vertex);
				
				return vertOut;							
			}
			
			
			float4 frag (vertexOutput vertIn) : COLOR
			{
			
				float4 mixColor = tex2D(_MixTex, vertIn.tex); //Color from b&w mix map
				float4 mainColor = tex2D(_MainTex, vertIn.tex); //white (1)
				float4 otherColor = tex2D(_OtherTex, vertIn.tex); //black (0)
				
 
				
				return lerp(otherColor, mainColor, mixColor.x);
				
											
			
			}
			ENDCG
		}
	}
		
}
