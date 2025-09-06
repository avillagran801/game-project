Shader "Custom/PixelOutline"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (1,1,0,1)
        _OutlineSize ("Outline Size (px)", Range(1,5)) = 1
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
            };

            struct v2f {
                float4 vertex : SV_POSITION;
                float2 texcoord : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _OutlineColor;
            float _OutlineSize;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.texcoord = v.texcoord;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                const float alpha_threshold = 0.01;

                fixed4 c = tex2D(_MainTex, i.texcoord);

                // If the pixel is not transparent, return its original color.
                if (c.a > alpha_threshold)
                {
                    return c;
                }

                // Expand alpha in 4 directions
                float2 offset = _OutlineSize * _MainTex_TexelSize.xy;

                float a = 0;
                a = max(a, tex2D(_MainTex, i.texcoord + float2(offset.x,0)).a);
                a = max(a, tex2D(_MainTex, i.texcoord - float2(offset.x,0)).a);
                a = max(a, tex2D(_MainTex, i.texcoord + float2(0,offset.y)).a);
                a = max(a, tex2D(_MainTex, i.texcoord - float2(0,offset.y)).a);

                // If a neighboring pixel has alpha, this is an outline pixel
                if (a > alpha_threshold)
                {
                    return fixed4(_OutlineColor.rgb, 1.0);
                }

                // Otherwise, the pixel should be fully transparent
                return fixed4(0,0,0,0);
            }
            ENDCG
        }
    }
}