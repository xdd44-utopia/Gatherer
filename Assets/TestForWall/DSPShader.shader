Shader "Custom/DSPShader"
{
    Properties {
		//主图
		_MainTex("MainTex",2D)="white"{}
		//漫反射
		_Diffuse("Diffuse",Color)=(1,1,1,1)
		//高光颜色
        _SpecularColor ("SpecularColor", Color) = (1,1,1,1)
		//高光比例
        _SpecularScale ("_SpecularScale", range(1,32)) = 2
    }
    SubShader {
        Pass {
            Tags{ "LightMode" = "Forwardbase" }
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase 

            #include "unitycg.cginc"
            #include "lighting.cginc"
            #include "autolight.cginc"  

            fixed4 _SpecularColor;
            float _SpecularScale;
			sampler2D _MainTex;
            float4 _MainTex_ST;
			float4 _Diffuse;

			struct a2v
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal:NORMAL;
            };

            struct v2f{
                float4 pos:POSITION;
                float3 normal:NORMAL;
                float4 vertex:TEXCOORD2;
				float2 uv:TEXCOORD3;
                SHADOW_COORDS(0)       
            };

			//顶点着色器
            v2f vert(a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = normalize(v.normal);
                o.vertex = v.vertex;
				o.uv = v.uv;
                TRANSFER_SHADOW(o)
                return o;
            }

			//片元着色器
            fixed4 frag(v2f v):COLOR
            {
				float3 wpos = mul(unity_ObjectToWorld, v.vertex).xyz;
                UNITY_LIGHT_ATTENUATION(atten, IN, wpos)  //5.

                //漫反射
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				fixed3 worldNormal = normalize(mul(v.normal,(float3x3)unity_WorldToObject));
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);
				fixed3 diffuse = _LightColor0.rgb*_Diffuse.rgb*saturate(dot(worldNormal,worldLightDir));

				//纹理贴图
				float4 tex= tex2D(_MainTex,v.uv);

                //高光反射
                float3 worldVector = normalize(WorldSpaceViewDir(v.vertex));
                float3 Specular = 2 * dot(worldNormal, worldLightDir)*worldNormal - worldLightDir; //phong
                float3 HeightLight = normalize(worldVector + worldLightDir);    //blinphong
                float specScale = pow(saturate(dot(Specular, worldVector)), _SpecularScale);  //phong
                specScale = pow(saturate(dot(HeightLight, worldNormal)), _SpecularScale);        //blinphong
				Specular+=_SpecularColor*specScale;


				//点光记算
				float3 pointLight = Shade4PointLights(unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
				    unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
				    unity_4LightAtten0,
				    wpos, worldNormal);

				//把上面得到的值在这里做计算吧
				diffuse *=Specular;
                diffuse = tex*diffuse;
				diffuse.rgb += pointLight;
				diffuse.rgb *= atten;    //6.
				
                diffuse += UNITY_LIGHTMODEL_AMBIENT;

                return float4(diffuse,1);
            }
			
            ENDCG
		}

		//下面的用于接收与产生点光原的阴影Shader
		Pass {
			Tags{ "Lightmode" = "Forwardadd" }  
			Blend one one
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#include "lighting.cginc"
				#include "autolight.cginc"
		
				//阴影主要来源于这句
				#pragma multi_compile_fwdadd_fullshadows
		
				struct a2v
				{
					float4 vertex : POSITION;
					float2 uv : TEXCOORD0;
					float3 normal:NORMAL;
				};
		
				struct v2f {
				    float4 pos:POSITION;
				    float3 normal:NORMAL;
				    float4 vertex:TEXCOORD2;
				    SHADOW_COORDS(0)
				};
		
				v2f vert(a2v v)
				{
				    v2f o;
				    o.pos = UnityObjectToClipPos(v.vertex);
				    o.normal = normalize(v.normal);
				    o.vertex = v.vertex;
				    TRANSFER_SHADOW(o)
				    return o;
				}
		
				fixed4 frag(v2f i) :COLOR
				{
				    float3 wpos = mul(unity_ObjectToWorld, i.vertex).xyz;
				    UNITY_LIGHT_ATTENUATION(atten, i, wpos)
		
				    //漫反射
				    float3 worldNormal = UnityObjectToWorldNormal(i.normal);
				    float3 worldLight = normalize(_WorldSpaceLightPos0).xyz;
				    float ndotl = saturate(dot(worldNormal, worldLight));
				    fixed4 diffuse = _LightColor0*ndotl;
		
				    //衰减
				    diffuse.rgb *= atten;
				    return diffuse;
				}
		
            ENDCG
        }
    }
	Fallback "Diffuse"
}
