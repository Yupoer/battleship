Shader "Custom/RingFillShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Progress ("Progress", Range(0,1)) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Progress;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // 計算當前像素的角度 (從中心點 (0.5, 0.5) 計算)
                float2 uv = i.uv - 0.5;
                float angle = atan2(uv.y, uv.x); // -π 到 π
                float normalizedAngle = (angle + UNITY_PI) / (2.0 * UNITY_PI); // 0 到 1

                // 如果當前角度超過進度，則丟棄像素
                if (normalizedAngle > _Progress)
                {
                    discard;
                }

                // 顯示原圖像素
                return tex2D(_MainTex, i.uv);
            }
            ENDCG
        }
    }
}
