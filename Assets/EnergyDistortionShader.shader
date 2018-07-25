// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

 // Unlit alpha-blended shader.
 // - no lighting
 // - no lightmap support
 // - no per-material color
 
 Shader "Custom/EnergyDistortionShader" {
 Properties {
     _MainTex ("Base (RGB) Trans (A)", 2D) = "black" {}
     _NoiseTex ("Noise Texture", 2D) = "green" {}
     _Color ("Main Color", Color) = (0.5,0.5,0.5,1)
     _DistortionSpreader ("Distortion Spreader", Float) = 50
     _DistortionDamper ("Distortion Damper", Float) = 50
     _TimeDamper ("Time Damper", Float) = 50
 }
 
 SubShader {
     Tags {"Queue"="Transparent-1" "IgnoreProjector"="True" "RenderType"="Transparent"}
     LOD 100
     
     ZWrite Off
     Blend SrcAlpha OneMinusSrcAlpha 

     Pass {  

     	Material {
     		Diffuse [_Color]
     	}

         CGPROGRAM
             #pragma vertex vert
             #pragma fragment frag
             #pragma multi_compile_fog
             
             #include "UnityCG.cginc"
 
             sampler2D _NoiseTex;

            int Random (int min, int max, float2 uv)    //Pass this function a minimum and maximum value, as well as your texture UV
 			{
     			if (min > max)
         			return 1;        //If the minimum is greater than the maximum, return a default value
 
     			float cap = max - min;    //Subtract the minimum from the maximum
     			int rand = tex2D (_NoiseTex, uv + _Time.x).r * cap + min;    //Make the texture UV random (add time) and multiply noise texture value by the cap, then add the minimum back on to keep between min and max 
     			return rand;    //Return this value
 			}

             struct appdata_t {
                 float4 vertex : POSITION;
                 float2 texcoord : TEXCOORD0;
             };
 
             struct v2f {
                 float4 vertex : SV_POSITION;
                 half2 texcoord : TEXCOORD0;
                 UNITY_FOG_COORDS(1)
                 float4 worldSpacePosition : TEXCOORD1;
             };
 
             sampler2D _MainTex;
             float4 _MainTex_ST;
             
             v2f vert (appdata_t v)
             {
                 v2f o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.texcoord = TRANSFORM_TEX(v.texcoord, _MainTex);
                 o.worldSpacePosition = mul(unity_ObjectToWorld, v.vertex);
                 UNITY_TRANSFER_FOG(o,o.vertex);
                 return o;
             }

             float _DistortionSpreader;
             float _DistortionDamper;
             float _TimeDamper;

             fixed4 frag (v2f IN) : SV_Target
             {
             	float4 p = UNITY_MATRIX_MVP[3];

             	p.xy /= p.w;

             	float2 offset = float2(
					tex2D(_NoiseTex,float2(IN.texcoord.y / _DistortionSpreader, _Time[1]/_TimeDamper)).g + (Random(-50,50,IN.worldSpacePosition.xy) / 40),
					tex2D(_NoiseTex,float2(_Time[1] / _TimeDamper, IN.texcoord.x / _DistortionSpreader)).r + (Random(-50,50,IN.worldSpacePosition.xy) / 40));

				offset -= 0.5;

				fixed4 col = tex2D(_MainTex, IN.texcoord + offset/_DistortionDamper);

                UNITY_APPLY_FOG(i.fogCoord, col);

                return col;
             }
         ENDCG
     }
 }
 
 }
