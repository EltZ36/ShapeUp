Shader "Unlit/CrackShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CrackTex ("CrackTexture", 2D) = "white" {}
        _WorleyNoise("WorleyNoise", 2D) = "white" {}
        _WorleyZoom("WorleyZoom", int) = 1
        _Threshold ("Threshold", float) = 1.0
    }
    SubShader
    {
        Tags {"Queue"="Transparent" "RenderType"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        ZWrite Off
        LOD 100

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

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _WorleyNoise;
            int _WorleyZoom;
            float4 _WorleyNoise_ST;

            sampler2D _CrackTex;
            float4 _CrackTex_ST;
            float _Threshold;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

 

            fixed4 frag (v2f i) : SV_Target
            {
                float2 current = i.uv;
                fixed4 col = tex2D(_MainTex, current);

                if(col.a<0.1){
                    return col;
                }

                float2 offsetCord = abs(fmod(current + _CrackTex_ST.zw, 1.0));
                float2 worleyOffset = abs(fmod(current + _WorleyNoise_ST.zw, 1.0));


                fixed4 noise = tex2D(_WorleyNoise, worleyOffset);
                fixed4 crack = tex2D(_CrackTex, offsetCord);

                if(noise.r>1-_Threshold&&crack.a==1){
                    return crack;
                }
                
                return col;
           
            }
            ENDCG
        }
    }
}
