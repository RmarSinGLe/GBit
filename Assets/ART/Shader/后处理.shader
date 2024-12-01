// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "后处理"
{
	Properties
	{
		_MainTex ( "Screen", 2D ) = "black" {}
		_offset("offset", Vector) = (10,10,0,0)
		_NoiseT("NoiseT", 2D) = "white" {}
		_BlockSize("BlockSize", Float) = 0
		_TimeScaie("TimeScaie", Float) = 0
		_Float3("Float 3", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}

	}

	SubShader
	{
		LOD 0

		
		
		ZTest Always
		Cull Off
		ZWrite Off

		
		Pass
		{ 
			CGPROGRAM 

			

			#pragma vertex vert_img_custom 
			#pragma fragment frag
			#pragma target 3.0
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata_img_custom
			{
				float4 vertex : POSITION;
				half2 texcoord : TEXCOORD0;
				
			};

			struct v2f_img_custom
			{
				float4 pos : SV_POSITION;
				half2 uv   : TEXCOORD0;
				half2 stereoUV : TEXCOORD2;
		#if UNITY_UV_STARTS_AT_TOP
				half4 uv2 : TEXCOORD1;
				half4 stereoUV2 : TEXCOORD3;
		#endif
				
			};

			uniform sampler2D _MainTex;
			uniform half4 _MainTex_TexelSize;
			uniform half4 _MainTex_ST;
			
			uniform float2 _offset;
			uniform sampler2D _NoiseT;
			SamplerState sampler_NoiseT;
			uniform float _BlockSize;
			uniform float _Float3;
			uniform float _TimeScaie;


			v2f_img_custom vert_img_custom ( appdata_img_custom v  )
			{
				v2f_img_custom o;
				
				o.pos = UnityObjectToClipPos( v.vertex );
				o.uv = float4( v.texcoord.xy, 1, 1 );

				#if UNITY_UV_STARTS_AT_TOP
					o.uv2 = float4( v.texcoord.xy, 1, 1 );
					o.stereoUV2 = UnityStereoScreenSpaceUVAdjust ( o.uv2, _MainTex_ST );

					if ( _MainTex_TexelSize.y < 0.0 )
						o.uv.y = 1.0 - o.uv.y;
				#endif
				o.stereoUV = UnityStereoScreenSpaceUVAdjust ( o.uv, _MainTex_ST );
				return o;
			}

			half4 frag ( v2f_img_custom i ) : SV_Target
			{
				#ifdef UNITY_UV_STARTS_AT_TOP
					half2 uv = i.uv2;
					half2 stereoUV = i.stereoUV2;
				#else
					half2 uv = i.uv;
					half2 stereoUV = i.stereoUV;
				#endif	
				
				half4 finalColor;

				// ase common template code
				float2 uv_MainTex = i.uv.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float2 texCoord5 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float2 appendResult12 = (float2(_MainTex_TexelSize.x , _MainTex_TexelSize.y));
				float2 texCoord16 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float dotResult26 = dot( ( ceil( ( texCoord16 * _BlockSize ) ) * floor( ( _Time.y * 20.0 ) ) ) , float2( 17.13,3.17 ) );
				float RandomBlock33 = frac( ( sin( dotResult26 ) * 31245.0 ) );
				float2 texCoord42 = i.uv.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner43 = ( 1.0 * _Time.y * ( float2( 3,5 ) * saturate( (RandomBlock33*2.0 + -1.0) ) ) + texCoord42);
				float RandomNoise69 = (tex2D( _NoiseT, panner43 ).r*2.0 + -1.0);
				float LoopTime85 = step( _Float3 , saturate( ( ( frac( ( _Time.y * _TimeScaie ) ) - ( 0.5 * RandomBlock33 ) ) * 2.0 * RandomBlock33 ) ) );
				float2 temp_output_10_0 = ( appendResult12 * _offset * RandomNoise69 * LoopTime85 );
				float4 appendResult15 = (float4(tex2D( _MainTex, uv_MainTex ).r , tex2D( _MainTex, ( texCoord5 + temp_output_10_0 ) ).g , tex2D( _MainTex, ( texCoord5 - temp_output_10_0 ) ).b , 1.0));
				

				finalColor = appendResult15;

				return finalColor;
			} 
			ENDCG 
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18500
271;248;1304;592;2083.248;224.6649;1.819282;True;False
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-2806.948,-455.7771;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;19;-2768.073,-303.7524;Inherit;False;Property;_BlockSize;BlockSize;2;0;Create;True;0;0;False;0;False;0;10;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;22;-2843.101,-134.1348;Inherit;False;Constant;_RandomSpeed;RandomSpeed;2;0;Create;True;0;0;False;0;False;20;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;21;-2874.324,-201.5008;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;17;-2555.582,-454.0885;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-2640.629,-196.2795;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FloorOpNode;25;-2485.353,-187.8528;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CeilOpNode;18;-2380.174,-413.9952;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-2242.851,-398.9602;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.Vector2Node;27;-2298.769,-203.6803;Inherit;False;Constant;_Vector0;Vector 0;2;0;Create;True;0;0;False;0;False;17.13,3.17;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DotProductOpNode;26;-2098.453,-334.1933;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SinOpNode;28;-1960.983,-264.8227;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;29;-1962.584,2.571537;Inherit;False;Constant;_Float0;Float 0;2;0;Create;True;0;0;False;0;False;31245;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-1786.684,-127.0285;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;31;-1565.73,-121.9626;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;33;-1378.779,-115.4377;Inherit;False;RandomBlock;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;34;-3195.634,456.9607;Inherit;False;33;RandomBlock;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;74;-2899.251,856.2394;Inherit;False;Property;_TimeScaie;TimeScaie;3;0;Create;True;0;0;False;0;False;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;73;-2923.3,761.7995;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;87;-2699.043,1115.594;Inherit;False;33;RandomBlock;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;78;-2694.174,997.4192;Inherit;False;Constant;_Float1;Float 1;4;0;Create;True;0;0;False;0;False;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;75;-2669.251,813.2394;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;66;-2978.829,463.1405;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;86;-2519.043,995.594;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;67;-2732.57,490.0242;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FractNode;76;-2470.452,827.4892;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;65;-2796.569,360.56;Inherit;False;Constant;_NoiseTSpeed;NoiseTSpeed;7;0;Create;True;0;0;False;0;False;3,5;0,0;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.RangedFloatNode;80;-2297.174,1023.419;Inherit;False;Constant;_Float2;Float 2;4;0;Create;True;0;0;False;0;False;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;42;-2660.371,39.81048;Inherit;True;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;77;-2277.174,898.4192;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;64;-2569.149,372.2565;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;88;-2330.043,1110.594;Inherit;False;33;RandomBlock;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;79;-2094.174,930.4192;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PannerNode;43;-2325.538,110.7514;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;84;-2047.813,821.2036;Inherit;False;Property;_Float3;Float 3;4;0;Create;True;0;0;False;0;False;0;0.1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;45;-2083.171,254.3301;Inherit;True;Property;_NoiseT;NoiseT;1;0;Create;True;0;0;False;0;False;-1;None;b29873aa44a520148813207e8a7413ae;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;81;-1975.48,955.5183;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;82;-1733.813,910.2036;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScaleAndOffsetNode;68;-1764.508,350.0355;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;85;-1543.813,860.2036;Inherit;False;LoopTime;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;69;-1502.576,343.2976;Inherit;False;RandomNoise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;11;-924.4009,-95.8119;Inherit;False;0;0;_MainTex_TexelSize;Pass;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;70;-775.1453,388.5519;Inherit;False;69;RandomNoise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;9;-767.3132,180.9229;Inherit;False;Property;_offset;offset;0;0;Create;True;0;0;False;0;False;10,10;10,10;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.DynamicAppendNode;12;-672.6837,-49.67348;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;89;-769.7271,472.1201;Inherit;False;85;LoopTime;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;5;-626.2688,-182.8427;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-573.1025,123.2461;Inherit;False;4;4;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;7;-352.0255,-58.94589;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;13;-362.5832,147.4241;Inherit;False;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;1;-418.3328,-271.5621;Inherit;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-224.1441,-270.3401;Inherit;True;Property;_TextureSample2;Texture Sample 2;2;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-215.8145,-80.02482;Inherit;True;Property;_TextureSample1;Texture Sample 1;1;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-205.8217,114.9537;Inherit;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;15;161.0853,-53.77334;Inherit;False;COLOR;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;334.8211,-53.07384;Float;False;True;-1;2;ASEMaterialInspector;0;2;后处理;c71b220b631b6344493ea3cf87110c93;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;1;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;True;2;False;-1;True;7;False;-1;False;True;0;False;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;;False;0
WireConnection;17;0;16;0
WireConnection;17;1;19;0
WireConnection;23;0;21;0
WireConnection;23;1;22;0
WireConnection;25;0;23;0
WireConnection;18;0;17;0
WireConnection;20;0;18;0
WireConnection;20;1;25;0
WireConnection;26;0;20;0
WireConnection;26;1;27;0
WireConnection;28;0;26;0
WireConnection;30;0;28;0
WireConnection;30;1;29;0
WireConnection;31;0;30;0
WireConnection;33;0;31;0
WireConnection;75;0;73;0
WireConnection;75;1;74;0
WireConnection;66;0;34;0
WireConnection;86;0;78;0
WireConnection;86;1;87;0
WireConnection;67;0;66;0
WireConnection;76;0;75;0
WireConnection;77;0;76;0
WireConnection;77;1;86;0
WireConnection;64;0;65;0
WireConnection;64;1;67;0
WireConnection;79;0;77;0
WireConnection;79;1;80;0
WireConnection;79;2;88;0
WireConnection;43;0;42;0
WireConnection;43;2;64;0
WireConnection;45;1;43;0
WireConnection;81;0;79;0
WireConnection;82;0;84;0
WireConnection;82;1;81;0
WireConnection;68;0;45;1
WireConnection;85;0;82;0
WireConnection;69;0;68;0
WireConnection;12;0;11;1
WireConnection;12;1;11;2
WireConnection;10;0;12;0
WireConnection;10;1;9;0
WireConnection;10;2;70;0
WireConnection;10;3;89;0
WireConnection;7;0;5;0
WireConnection;7;1;10;0
WireConnection;13;0;5;0
WireConnection;13;1;10;0
WireConnection;4;0;1;0
WireConnection;3;0;1;0
WireConnection;3;1;7;0
WireConnection;2;0;1;0
WireConnection;2;1;13;0
WireConnection;15;0;4;1
WireConnection;15;1;3;2
WireConnection;15;2;2;3
WireConnection;0;0;15;0
ASEEND*/
//CHKSM=7F6E13EEB687D692F60D44C0E220682F526A4C44