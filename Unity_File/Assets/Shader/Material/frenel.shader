Shader "frenel"
{
    Properties
    {
        _Color ("Main Color", Color) = (1,1,1,1)
		_ShineColor ("Shine Color", Color) = (1,1,1,1)
		_ShineScale ("Shine Scale", Range(0, 1)) = 0.5
		_ShinePower ("Shine Power", Range(0, 1)) = 0.5
    }
    SubShader
    {
        Tags { "Queue"="Transparent" }

        LOD 200

        Pass
        {
			Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
			#include "Lighting.cginc"

			half4 _Color;
			half4 _ShineColor;
			float _ShineScale;
			float _ShinePower;

            struct a2v
            {
                float4 vertex : POSITION;
				float3 normal : NORMAL;
            };

            struct v2f
            {
				float4 pos : SV_POSITION;
				float3 normal : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
            };


            v2f vert (a2v v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
				o.normal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				half3 normal = normalize(i.normal);
				half3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				half3 lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));

				fixed4 col = fixed4(0,0,0,1);
				col.rgb = _LightColor0.rgb * _Color.rgb * (dot(lightDir, normal)*0.5+0.5);
				half shine = max(pow(1-dot(normal, viewDir), _ShinePower * 10), 0.001) * _ShineScale;
				col.rgb = lerp(col.rgb, _ShineColor.rgb, shine);
				
                return col;
            }
            ENDCG
        }
    }
}
