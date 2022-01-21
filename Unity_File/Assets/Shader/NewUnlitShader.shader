Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ShineColor("Shine Color", Color) = (1,1,1,1)
        _ShineScale("Shine Scale", Range(0, 1)) = 0.5
        _ShinePower("Shine Power", Range(0, 1)) = 0.5
        _frenel("Frenel Inter", Range(0, 1)) = 0.0
        _Color ("Main Color", Color) = (1,1,1,1)
        _frenelpower("FrenelColor Power", Range(0, 10)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"
            #include "Lighting.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                // float4 pos : SV_POSITION;
                float3 normal : TEXCOORD2;
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Color;
            half4 _ShineColor;
            float _ShineScale;
            float _ShinePower;
            float _frenel;
            float _frenelpower;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

                // o.pos = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                

                half3 normal = normalize(i.normal);
				half3 viewDir = normalize(UnityWorldSpaceViewDir(i.worldPos));
				half3 lightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));

                fixed4 colfrenel = fixed4(0, 0, 0, 1);
                colfrenel.rgb = _LightColor0.rgb * _Color.rgb * (dot(lightDir, normal) * 0.5 + 0.5);
                half shine = max(pow(1 - dot(normal, viewDir), _ShinePower * 10), 0.001) * _ShineScale;
                colfrenel.rgb = lerp(colfrenel.rgb, _ShineColor.rgb * _frenelpower, shine);
                half3 final_output;
                final_output = lerp(col, colfrenel.rgb, _frenel);

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, final_output);

                return float4(final_output, 1.0);
            }
            ENDCG
        }
    }
}
