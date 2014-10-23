Shader "Custom/TestShader"
{
	Properties
		{
			_OffsetX("CamOffsetX", Float) = 0
			_OffsetY("CamOffsetY", Float) = 0
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

		 float _OffsetX;
		 float _OffsetY;

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
			//float scale = 3.0;
			//float2 inpos_scaled = inpos.xy / scale;
			//fixed4 orgCol = tex2D(_MainTex, inpos_scaled);
			
			float2 wcoords = 0.5*(i.sPos.xy+1.0) * _ScreenParams.xy;
			wcoords = i.sPos.xy * _ScreenParams.xy;
			fixed4 col;
			if( false )// _ScreenParams.x  == 800 && _ScreenParams.y == 600)
				col = fixed4(0,0,1,1);
			else if ( wcoords.x < 400.0 && wcoords.y < 300.0 )
				col = fixed4(0,0,1,1);
			else col = fixed4(i.sPos.xy, 0, 1.0);
			return col;
			//return fixed4(inpos.xy, 0, 1.0);
         }
 
         ENDCG
      }
   }
}
