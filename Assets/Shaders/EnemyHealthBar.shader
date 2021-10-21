Shader "UI/EnemyHealthBar" 
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _Fill("Fill", float) = 0
        _Tint("Tint", Color) = (1,1,1,1)
    }
        SubShader
        {
            Tags { "Queue" = "Overlay"}
           // ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha
            LOD 100

            Pass {
                ZTest Off

                CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #pragma multi_compile_instancing


            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                UNITY_VERTEX_INPUT_INSTANCE_ID
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                half4 tint : Color;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half4 _Tint;

            UNITY_INSTANCING_BUFFER_START(Props)
            UNITY_DEFINE_INSTANCED_PROP(float, _Fill)
            UNITY_INSTANCING_BUFFER_END(Props)

            v2f vert(appdata v) {
                v2f o;
                UNITY_SETUP_INSTANCE_ID(v);

                float fill = UNITY_ACCESS_INSTANCED_PROP(Props, _Fill);

                o.vertex = UnityObjectToClipPos(v.vertex);

                o.tint = _Tint;
                o.uv = v.uv;
                o.uv.x += 0.5 - fill;
                return o;
            }

            fixed4 frag(v2f i)  : Color
            {
                return tex2D(_MainTex, i.uv) * i.tint;
            }

            ENDCG
        }
        }
}