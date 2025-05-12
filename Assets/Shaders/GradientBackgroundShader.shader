Shader "Custom/VerticalGradientBackground"
{
    Properties
    {
        _TopColor ("Top Color", Color) = (0.2, 0.2, 0.2, 1)
        _BottomColor ("Bottom Color", Color) = (0.1, 0.1, 0.1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Background" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _TopColor;
            float4 _BottomColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = o.pos.xy * 0.5 + 0.5; // 归一化 UV 坐标
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                // 使用 Y 坐标来控制渐变效果
                float gradientFactor = i.uv.y; // 控制渐变的 Y 轴（从上到下）
                return lerp(_BottomColor, _TopColor, gradientFactor);
            }
            ENDCG
        }
    }
    Fallback "Unlit/Color"
}
