// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "FastWaterLowPoly/WaterStandardRender"
{
	Properties
	{
		_Colorwater("Color water", Color) = (0,0,0,0)
		_ColorDepth("Color Depth", Color) = (0,0,0,0)
		_ColorFoam("Color Foam", Color) = (0,0,0,0)
		_OpasityIntensity("OpasityIntensity", Range( 0 , 1)) = 1
		_Depth("Depth", Float) = 0
		_DepthRange("DepthRange", Range( 0 , 1)) = 0
		_SizeFoam("SizeFoam", Float) = 2
		_Gloss("Gloss", Range( 0 , 1)) = 0.9
		_Specular("Specular", Range( 0 , 1)) = 0.2
		_Noise("Noise", 2D) = "white" {}
		_Speed("Speed", Float) = 0
		_NoiseIntensity("NoiseIntensity", Float) = 0
		_ScaleNoise("ScaleNoise", Float) = 0
		_LowPolyToFlat("LowPolyToFlat", Range( 0 , 1)) = 0
		[Toggle]_WavesInXYZ("WavesInXYZ", Float) = 0
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityCG.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#ifdef UNITY_PASS_SHADOWCASTER
			#undef INTERNAL_DATA
			#undef WorldReflectionVector
			#undef WorldNormalVector
			#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
			#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
			#define WorldNormalVector(data,normal) half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))
		#endif
		struct Input
		{
			float3 worldPos;
			float3 worldNormal;
			INTERNAL_DATA
			float4 screenPosition13;
			float4 screenPos;
		};

		uniform sampler2D _Noise;
		uniform float _ScaleNoise;
		uniform float _WavesInXYZ;
		uniform float _Speed;
		uniform float _NoiseIntensity;
		uniform float _LowPolyToFlat;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _SizeFoam;
		uniform float4 _Colorwater;
		uniform float4 _ColorDepth;
		uniform float _Depth;
		uniform float _DepthRange;
		uniform float4 _ColorFoam;
		uniform float _Specular;
		uniform float _Gloss;
		uniform float _OpasityIntensity;

		void vertexDataFunc( inout appdata_full v, out Input o )
		{
			UNITY_INITIALIZE_OUTPUT( Input, o );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float lerpResult76 = lerp( 1.0 , ase_worldPos.y , _WavesInXYZ);
			float mulTime43 = _Time.y * _Speed;
			float4 appendResult51 = (float4(0.0 , ( tex2Dlod( _Noise, float4( ( ( (ase_worldPos).xz * _ScaleNoise * lerpResult76 ) + mulTime43 ), 0, 0.0) ).r * _NoiseIntensity ) , 0.0 , 0.0));
			v.vertex.xyz += appendResult51.xyz;
			v.vertex.w = 1;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float3 vertexPos13 = ase_vertex3Pos;
			float4 ase_screenPos13 = ComputeScreenPos( UnityObjectToClipPos( vertexPos13 ) );
			o.screenPosition13 = ase_screenPos13;
		}

		void surf( Input i , inout SurfaceOutputStandardSpecular o )
		{
			float3 ase_worldPos = i.worldPos;
			float3 normalizeResult68 = normalize( cross( ddy( ase_worldPos ) , ddx( ase_worldPos ) ) );
			float3 ase_worldNormal = WorldNormalVector( i, float3( 0, 0, 1 ) );
			float3 lerpResult70 = lerp( normalizeResult68 , ase_worldNormal , _LowPolyToFlat);
			float3 ase_worldTangent = WorldNormalVector( i, float3( 1, 0, 0 ) );
			float3 ase_worldBitangent = WorldNormalVector( i, float3( 0, 1, 0 ) );
			float3x3 ase_worldToTangent = float3x3( ase_worldTangent, ase_worldBitangent, ase_worldNormal );
			float3 worldToTangentDir74 = mul( ase_worldToTangent, lerpResult70);
			o.Normal = worldToTangentDir74;
			float4 ase_screenPos13 = i.screenPosition13;
			float4 ase_screenPosNorm13 = ase_screenPos13 / ase_screenPos13.w;
			ase_screenPosNorm13.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm13.z : ase_screenPosNorm13.z * 0.5 + 0.5;
			float screenDepth13 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm13.xy ));
			float distanceDepth13 = abs( ( screenDepth13 - LinearEyeDepth( ase_screenPosNorm13.z ) ) / ( _SizeFoam ) );
			float lerpResult17 = lerp( distanceDepth13 , ( distanceDepth13 * ( 1.0 - (lerpResult70).y ) ) , 0.998);
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth59 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth59 = abs( ( screenDepth59 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _Depth ) );
			float lerpResult62 = lerp( distanceDepth59 , (0.5 + (distanceDepth59 - -1.0) * (1.0 - 0.5) / (1.0 - -1.0)) , _DepthRange);
			float clampResult32 = clamp( lerpResult62 , 0.0 , 1.0 );
			float4 lerpResult30 = lerp( _Colorwater , _ColorDepth , clampResult32);
			float4 ifLocalVar7 = 0;
			if( lerpResult17 <= _SizeFoam )
				ifLocalVar7 = _ColorFoam;
			else
				ifLocalVar7 = lerpResult30;
			o.Albedo = ifLocalVar7.rgb;
			float3 temp_cast_1 = (_Specular).xxx;
			o.Specular = temp_cast_1;
			o.Smoothness = _Gloss;
			float4 ifLocalVar35 = 0;
			if( lerpResult17 <= _SizeFoam )
				ifLocalVar35 = _ColorFoam;
			float lerpResult33 = lerp( 1.0 , clampResult32 , _OpasityIntensity);
			float4 clampResult39 = clamp( ( ifLocalVar35 + lerpResult33 ) , float4( 0,0,0,0 ) , float4( 1,0,0,0 ) );
			o.Alpha = clampResult39.r;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf StandardSpecular alpha:fade keepalpha fullforwardshadows exclude_path:deferred vertex:vertexDataFunc 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float4 customPack1 : TEXCOORD1;
				float4 screenPos : TEXCOORD2;
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				vertexDataFunc( v, customInputData );
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				half3 worldTangent = UnityObjectToWorldDir( v.tangent.xyz );
				half tangentSign = v.tangent.w * unity_WorldTransformParams.w;
				half3 worldBinormal = cross( worldNormal, worldTangent ) * tangentSign;
				o.tSpace0 = float4( worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x );
				o.tSpace1 = float4( worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y );
				o.tSpace2 = float4( worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z );
				o.customPack1.xyzw = customInputData.screenPosition13;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.screenPos = ComputeScreenPos( o.pos );
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.screenPosition13 = IN.customPack1.xyzw;
				float3 worldPos = float3( IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w );
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = float3( IN.tSpace0.z, IN.tSpace1.z, IN.tSpace2.z );
				surfIN.internalSurfaceTtoW0 = IN.tSpace0.xyz;
				surfIN.internalSurfaceTtoW1 = IN.tSpace1.xyz;
				surfIN.internalSurfaceTtoW2 = IN.tSpace2.xyz;
				surfIN.screenPos = IN.screenPos;
				SurfaceOutputStandardSpecular o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutputStandardSpecular, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18500
0;6;1920;1013;-783.6924;-886.9929;1;True;True
Node;AmplifyShaderEditor.WorldPosInputsNode;64;707.3097,-229.3337;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DdyOpNode;66;1055.31,-159.3337;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DdxOpNode;65;1092.31,-334.3337;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CrossProductOpNode;67;1328.61,-222.0337;Inherit;True;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;68;1656.31,-176.6337;Inherit;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.WorldNormalVector;69;2059.85,-293.3838;Inherit;False;False;1;0;FLOAT3;0,0,1;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;72;1863.988,153.736;Inherit;False;Property;_LowPolyToFlat;LowPolyToFlat;13;0;Create;True;0;0;False;0;False;0;0.751;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;63;-2194.822,1041.915;Float;False;Property;_Depth;Depth;4;0;Create;True;0;0;False;0;False;0;1.9;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;70;2484.82,90.44559;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DepthFade;59;-1843.286,1165.781;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;58;-610.3984,140.198;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;8;-174.4654,299.2709;Float;False;Property;_SizeFoam;SizeFoam;6;0;Create;True;0;0;False;0;False;2;0.07;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldPosInputsNode;45;694.0103,1361.929;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ComponentMaskNode;71;2820.036,76.99588;Inherit;False;False;True;False;True;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;75;1314.383,1647.831;Inherit;False;Property;_WavesInXYZ;WavesInXYZ;14;1;[Toggle];Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DepthFade;13;216.2884,90.14276;Inherit;False;True;False;True;2;1;FLOAT3;0,0,0;False;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;27;311.731,378.2962;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;52;1161.892,1382.505;Float;False;Property;_ScaleNoise;ScaleNoise;12;0;Create;True;0;0;False;0;False;0;0.1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;76;1496.383,1515.831;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;44;1022.704,1499.557;Float;False;Property;_Speed;Speed;10;0;Create;True;0;0;False;0;False;0;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;61;-1539.286,861.7805;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0.5;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;54;1274.605,1268.694;Inherit;False;True;False;True;True;1;0;FLOAT3;0,0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;60;-1987.286,557.7805;Inherit;False;Property;_DepthRange;DepthRange;5;0;Create;True;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;18;638.5895,246.5706;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;1538.261,1285.97;Inherit;False;3;3;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;6;177.0331,599.5805;Float;False;Constant;_Lerp_;Lerp_;0;0;Create;True;0;0;False;0;False;0.998;0.998;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;62;-1411.286,685.7805;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;43;1200.704,1487.557;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;17;885.3664,375.2695;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-800.91,1145.026;Float;False;Property;_OpasityIntensity;OpasityIntensity;3;0;Create;True;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;42;1328.067,1034.249;Float;True;Property;_Noise;Noise;9;0;Create;True;0;0;False;0;False;None;eaa1c5562af66424092dc5b1f37208e9;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.ColorNode;12;-366.7387,743.674;Float;False;Property;_ColorFoam;Color Foam;2;0;Create;True;0;0;False;0;False;0,0,0,0;1,0.9931185,0.86,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;56;1765.838,1256.832;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.ClampOpNode;32;-780.2148,940.8075;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;31;-575.4737,369.9068;Float;False;Property;_ColorDepth;Color Depth;1;0;Create;True;0;0;False;0;False;0,0,0,0;0.04705872,0.1260222,0.1607842,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;53;2025.855,1166.695;Inherit;True;Property;_TextureSample0;Texture Sample 0;13;0;Create;True;0;0;False;0;False;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;35;1102.634,646.0739;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;11;-653.0463,617.0334;Float;False;Property;_Colorwater;Color water;0;0;Create;True;0;0;False;0;False;0,0,0,0;0.2316038,0.5137582,0.5607843,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;33;662.0513,1087.102;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;50;1727.652,1415.214;Float;False;Property;_NoiseIntensity;NoiseIntensity;11;0;Create;True;0;0;False;0;False;0;0.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;1941.652,1397.214;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;38;1413.607,903.7444;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;30;-79.04843,474.3805;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;57;2220.292,1511.856;Float;False;Constant;_Float0;Float 0;13;0;Create;True;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.WorldToObjectTransfNode;73;2654.77,356.4136;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ClampOpNode;39;1635.872,901.3589;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;1,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;7;1218.864,416.6523;Inherit;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TransformDirectionNode;74;2864.987,160.933;Inherit;True;World;Tangent;False;Fast;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.DynamicAppendNode;51;2416.419,1418.029;Inherit;False;FLOAT4;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;24;1580.093,440.4498;Float;False;Property;_Specular;Specular;8;0;Create;True;0;0;False;0;False;0.2;0.2;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;1587,535.1461;Float;False;Property;_Gloss;Gloss;7;0;Create;True;0;0;False;0;False;0.9;0.934;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;3202,594.0168;Float;False;True;-1;2;ASEMaterialInspector;0;0;StandardSpecular;FastWaterLowPoly/WaterStandardRender;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;ForwardOnly;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;66;0;64;0
WireConnection;65;0;64;0
WireConnection;67;0;66;0
WireConnection;67;1;65;0
WireConnection;68;0;67;0
WireConnection;70;0;68;0
WireConnection;70;1;69;0
WireConnection;70;2;72;0
WireConnection;59;0;63;0
WireConnection;71;0;70;0
WireConnection;13;1;58;0
WireConnection;13;0;8;0
WireConnection;27;0;71;0
WireConnection;76;1;45;2
WireConnection;76;2;75;0
WireConnection;61;0;59;0
WireConnection;54;0;45;0
WireConnection;18;0;13;0
WireConnection;18;1;27;0
WireConnection;55;0;54;0
WireConnection;55;1;52;0
WireConnection;55;2;76;0
WireConnection;62;0;59;0
WireConnection;62;1;61;0
WireConnection;62;2;60;0
WireConnection;43;0;44;0
WireConnection;17;0;13;0
WireConnection;17;1;18;0
WireConnection;17;2;6;0
WireConnection;56;0;55;0
WireConnection;56;1;43;0
WireConnection;32;0;62;0
WireConnection;53;0;42;0
WireConnection;53;1;56;0
WireConnection;35;0;17;0
WireConnection;35;1;8;0
WireConnection;35;3;12;0
WireConnection;35;4;12;0
WireConnection;33;1;32;0
WireConnection;33;2;34;0
WireConnection;48;0;53;1
WireConnection;48;1;50;0
WireConnection;38;0;35;0
WireConnection;38;1;33;0
WireConnection;30;0;11;0
WireConnection;30;1;31;0
WireConnection;30;2;32;0
WireConnection;73;0;70;0
WireConnection;39;0;38;0
WireConnection;7;0;17;0
WireConnection;7;1;8;0
WireConnection;7;2;30;0
WireConnection;7;3;12;0
WireConnection;7;4;12;0
WireConnection;74;0;70;0
WireConnection;51;0;57;0
WireConnection;51;1;48;0
WireConnection;51;2;57;0
WireConnection;0;0;7;0
WireConnection;0;1;74;0
WireConnection;0;3;24;0
WireConnection;0;4;23;0
WireConnection;0;9;39;0
WireConnection;0;11;51;0
ASEEND*/
//CHKSM=DE322C091299706C88879077A38DF4B24A31F582