Shader "Hidden/ToneMap"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		 Saturation("Saturation", Range(0 , 10)) = 0 // насыщеность в ярких местах 
		 _Disaturate("Disaturate", Range(0 , 1)) = 0 // насыщеность в ярких местах 
		PostExposure("PostExposure", Float) = 0
		Contrast("Contrast", Float) = 0
		_Min("Min", Range(-1 , 3)) = 0
		_Max("Max", Range(-1 , 3)) = 1 
			 Vignette("Vignette", 2D) = "white" {}
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always





		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float4 screenPos : TEXCOORD1;
				float2 depth : TEXCOORD2;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				float4 screenPos = ComputeScreenPos(o.vertex);
				o.screenPos = screenPos;
				UNITY_TRANSFER_DEPTH(o.depth);
				return o;
			}

			float PostExposure;
			float _Disaturate;
			float _Max;
			float _Min;
			float Contrast;
			sampler2D _MainTex;
			float Saturation;
			sampler2D _BlurTex;
			fixed _BlurAmount;
			int FepthOfField;
			UNITY_DECLARE_DEPTH_TEXTURE(_CameraDepthTexture);
			float BlurDistance;
			float BlurRange;
			sampler2D Vignette;
			float VignetteIntensity;
			float Remap(float value, float min1, float max1, float min2, float max2) {
				return (min2 + (value - min1) * (max2 - min2) / (max1 - min1));
			}

			float3 CalculateContrast(float contrastValue, float4 colorTarget)
			{
				float t = 0.5 * (1.0 - contrastValue);
				return  mul(float4x4(contrastValue, 0, 0, t, 0, contrastValue, 0, t, 0, 0, contrastValue, t, 0, 0, 0, 1), colorTarget).rgb;
			}

			float4 ToneMap(float4 MainColor,float brightness,float Disaturate,float _max,float _min,float contrast,float Satur) {

				

				

				
				fixed4 output = MainColor;
			//	output = output * brightness;
				output = output * brightness;
				output.rgb = CalculateContrast(contrast, float4(output.rgb,1));

				float4 disatur = dot(output, float3(0.299, 0.587, 0.114)); // Desaturate
				output = lerp(output, disatur, clamp(pow(((output.x + output.y + output.z) / 3) * Disaturate, 1.3), 0, 1));
					output.x = clamp(Remap(output.x, 0, 1, _Min, lerp(_Max, 1, 0.5)), 0, 1.5);
				output.y = clamp(Remap(output.y, 0, 1, _Min, lerp(_Max, 1, 0.5)), 0, 1.5);
					output.z = clamp(Remap(output.z, 0, 1, _Min, lerp(_Max,1,0.5)), 0, 1.5);
				
			//	output = CalculateContrast(clamp(1 - pow((output.x + output.y + output.z) / 3, 1),0,1) * 2, output);

				



					


					output = pow(output, contrast);
				
				//output = lerp(output * (1 - pow(disatur,2)), output, 1 * lerp(max,1,0.3) );

					

				//output = lerp(output, output - 0.5,  _Middle *  clamp( distance(0.8, disatur), 0, 1));

				output = lerp(clamp(output , 0, _max), output, pow(_max,4));



				output = lerp(smoothstep(output, -0.1, 0.25), output, (1 - distance(1, _max) * 2));

				
				output = lerp(dot(output, float3(0.299, 0.587, 0.114)),output, Satur);

				output = output * lerp(brightness,1,0.75);


				return output;


			}


				fixed4 frag(v2f i) : SV_Target
				{

					float Depth = clamp(pow(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.screenPos.xy),BlurRange) * ((BlurDistance * 1500) * pow(BlurRange,2)),0,1);
				    Depth = lerp(1, Depth, FepthOfField);
					fixed4 b = tex2D(_BlurTex, i.uv);
					


					fixed4 col = tex2D(_MainTex,i.uv);
					col = lerp(b, col, Depth);


			col = ToneMap(col,PostExposure,_Disaturate,_Max,_Min,Contrast, Saturation);


			


				//col = blur2;

			//col = col - 0.5;

				// just invert the colors
				//col = 1 - col;

			col = col * lerp(1, tex2D(Vignette, i.screenPos.xy), VignetteIntensity);
			
				return col;
			}
			ENDCG
		}


		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

				sampler2D _MainTex;
	sampler2D _BloomTex;
	fixed4 _MainTex_TexelSize;
	fixed _BlurAmount;
	struct appdata_ {
		fixed4 pos : POSITION;
		fixed2 uv : TEXCOORD0;
		fixed2 uv2 : TEXCOORD1;
	};

		struct v2f
		{
			float4 uv : TEXCOORD0;
			float4 uv2 : TEXCOORD1;
			float4 vertex : SV_POSITION;
			float4 screenPos : TEXCOORD2;
		};


	v2f vert(appdata_ i)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(i.pos);
		fixed2 offset = _MainTex_TexelSize.xy * _BlurAmount;
		o.uv = fixed4(i.uv - offset, i.uv + offset);
		offset = _MainTex_TexelSize.xy * _BlurAmount * 2;
		o.uv2 = fixed4(i.uv - offset, i.uv + offset);

		return o;
	}

	fixed4 frag(v2f i) : COLOR
	{
		fixed4 result = tex2D(_MainTex, i.uv.xy);
		result += tex2D(_MainTex, i.uv.xw);
		result += tex2D(_MainTex, i.uv.zy);
		result += tex2D(_MainTex, i.uv.zw);


		result += tex2D(_MainTex,  i.uv2.xy);
		result += tex2D(_MainTex,  i.uv2.xw);
		result += tex2D(_MainTex,  i.uv2.zy);
		result += tex2D(_MainTex,  i.uv2.zw);

		return result / 8;
	}


		/*	fixed4 frag(v2f i) : COLOR
	{
		float Depth = clamp(pow(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.screenPos.xy),8) * 15000,0,1);
		fixed4 c = tex2D(_MainTex, i.uv);
		fixed4 b = tex2D(_BloomTex, i.uv);
		//return Depth;
		return lerp(b,c, Depth);
		}*/

		ENDCG
		}


			/*
		
		Pass //0
		{
		  ZTest Always Cull Off ZWrite Off

		  Fog { Mode off }
CGPROGRAM
#pragma vertex vertBlur
#pragma fragment fragBloom
#pragma fragmentoption ARB_precision_hint_fastest
		  ENDCG
		}

			Pass //1
		{
		  ZTest Always Cull Off ZWrite Off

		  Fog { Mode off }
		  CGPROGRAM
#pragma vertex vertBlur
#pragma fragment fragBlur
#pragma fragmentoption ARB_precision_hint_fastest
		  ENDCG
		}*/
	}

}
