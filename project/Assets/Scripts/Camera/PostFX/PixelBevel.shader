
Shader "Custom/PixelBevel"
{
	Properties
		{
			_CamOffsetX("CamOffsetX", Float) = 0
			_CamOffsetY("CamOffsetY", Float) = 0
			_ScalePixel("ScalePixel", Float) = 3
			_ColorDelta("ColorDelta", Vector) = (0.5,0.5,0.5,1)
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

		 float _CamOffsetX;
		 float _CamOffsetY;
		 float _ScalePixel;
		 fixed4 _ColorDelta;

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
			float2 inpos = (i.sPos.xy / i.sPos.w);
			float2 inpos_scaled = i.sPos.xy;// / _ScalePixel;
			inpos_scaled.y = 1-inpos_scaled.y;
			float2 wcoords =  i.sPos.xy * _ScreenParams.xy;
			fixed4 col;
			fixed4 orgCol = tex2D(_MainTex, inpos_scaled);
			//fixed4 orgCol = tex2D(_MainTex, inpos);
			col = orgCol;
//			col = orgCol;
//
			float mod = fmod(wcoords.x*1, _ScalePixel);
			float xlvl, ylvl;
			xlvl = ylvl = 0;
			xlvl = ceil(mod);
			

			mod = fmod(wcoords.y*1, _ScalePixel);
			ylvl = ceil(mod);			
			col = orgCol + _ColorDelta / (xlvl + ylvl);



			return col;
         }
 
         ENDCG
      }
   }
}
