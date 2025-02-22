Shader "Unlit/StarShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _StarTex ("Star Texture", 2D) = "black" {}
        _GridSize ("Grid Size", float) = 0.5
        _SpriteSize ("Sprite Size", float) = 0.2
        _Frequency ("Frequency", float) = 0.2

        _AnimationSpeed("Animation Speed", float) = 1

        _SpeedLeft ("Speed Left", float) = 0
        _SpeedRight ("Speed Right", float) = 0

    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }

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
                float3 worldPos : TEXCOORD1;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _StarTex;

            float _GridSize;
            float _SpriteSize;
            float _Frequency;

            float _AnimationSpeed;

            float _SpeedLeft;
            float _SpeedRight;
            
            //https://www.shadertoy.com/view/4djSRW PseudoRandom in shader 
            float2 hash22(float2 p){
                float3 p3 = frac(p.xyx * float3(.1031, .1030, .0973));
	            p3 += dot(p3, p3.yzx + 33.33);
                return frac((p3.xx+p3.yz)*p3.zy);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float2 worldCoords = i.worldPos.xy + float2(_Time.y*_SpeedLeft,_Time.y*_SpeedRight);

                float2 grid = floor(worldCoords/_GridSize);
                float2 gridPos = frac(worldCoords/_GridSize);

                float2 random = hash22(grid);
                

                if(random.x+random.y<=_Frequency*2){
                    
                    float2 offset = .5 + (random-0.5)*(1-_SpriteSize);

                    float mask = 0;

                    if(gridPos.x>offset.x-_SpriteSize/2&&gridPos.x<offset.x+_SpriteSize/2){
                        if(gridPos.y>offset.y-_SpriteSize/2&&gridPos.y<offset.y+_SpriteSize/2){
                            mask = 1;
                        }
                    }
                    
                    if(mask==1){
                        float2 starUV = (gridPos - (offset - _SpriteSize / 2)) / _SpriteSize;
                        
                        float2 mid = float2(0.5,0.5);
                        starUV = (starUV-mid)/_SpriteSize + mid;

                        float indexSize = (1.0/8.0);

                        starUV.x *= indexSize;
                        float starOffset = floor((_Time.y*_AnimationSpeed)%9);
                        starUV.x += indexSize*starOffset;
                        starUV.x = clamp(starUV.x,indexSize*starOffset,indexSize*(starOffset)+indexSize);

                        fixed4 star = tex2D(_StarTex,starUV);
                        if(star.a>0.1){
                            return star;
                        }
                        
                    }
                    
                    return col;
                }

                
                return col;
            }
            ENDCG
        }
    }
}
