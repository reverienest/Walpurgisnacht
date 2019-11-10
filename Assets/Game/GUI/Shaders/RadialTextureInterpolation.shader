Shader "GUI/RadialTextureInterpolation"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _AltTex ("Alt Texture", 2D) = "white" {}
        _t ("t", Range(0, 1)) = 0
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        ZTest [unity_GUIZTestMode]
        Blend SrcAlpha OneMinusSrcAlpha

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
                float4 color : COLOR;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.color = v.color;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _AltTex;
            float _t;

            fixed4 frag (v2f i) : SV_Target
            {
                float2 centeredCoords = i.uv * 2 - 1; // In the range (-1, -1) to (1, 1)
                float sqrt2 = 1.414213562;

                float radius = sqrt(pow(centeredCoords.x, 2) + pow(centeredCoords.y, 2)) / sqrt2; // In the range 0 to 1/sqrt2
                fixed4 col = radius > _t ? tex2D(_AltTex, i.uv) : tex2D(_MainTex, i.uv);
                return col * i.color;
            }
            ENDCG
        }
    }
}
