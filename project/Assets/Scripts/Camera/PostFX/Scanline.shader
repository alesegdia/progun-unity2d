Shader "Custom/Scanline"
{
	Properties
		{
			_LinesColor("LinesColor", Color) = (0,0,0,1)
			_LinesSize("LinesSize", Range(1,10)) = 1
			_MainTex ("", 2D) = "white" {}
		}
   SubShader {
   
   				 Tags {"IgnoreProjector" = "True" "Queue" = "Overlay"}
				 Fog { Mode Off }

   
      Pass {
		
		ZTest Always
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha 

         CGPROGRAM

         #pragma vertex vert 
         #pragma fragment frag
		 #include "UnityCG.cginc"

		 fixed4 _LinesColor;
		 half _LinesSize;

		 struct v2f
		 {
			half4 pos:POSITION;
			fixed4 sPos:TEXCOORD;
		 };
 
         v2f vert(appdata_base v)
         {
			v2f o;
			o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
			o.sPos = ComputeScreenPos(o.pos);
            return o;
         }
         
         sampler2D _MainTex;
 
         fixed4 frag(v2f i) : COLOR
         {
			half2 p = i.sPos.xy / i.sPos.w;
			fixed4 orgCol = tex2D(_MainTex, p);
	
			if((int)(p.y*_ScreenParams.y/(int)_LinesSize)%2==0)
			{
				return orgCol;
			}
			else
			{
            	return (_LinesColor + orgCol ) / 2 ; 
         	}
         }
 
         ENDCG
      }
   }
}
