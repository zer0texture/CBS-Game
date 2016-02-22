Shader "Font/TextContour"
{
Properties
{
    _MainTex ("Base (RGB), Alpha (A)", 2D) = "white" {}
	_Color("FontColor", Vector) = (1,1,1)
	_OutlineColor("OutlineColor", Vector) = (1,1,1)
 }

SubShader
{
    LOD 200

    Tags
    {
        "Queue" = "Transparent"
        "IgnoreProjector" = "True"
        "RenderType" = "Transparent"
    }

    Pass
    {
        Cull Off
        Lighting Off
        ZWrite Off
		ZTest off
        Fog { Mode Off }
        ColorMask RGB
        Blend SrcAlpha OneMinusSrcAlpha		

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
		float3 _Color;
		float3 _OutlineColor;

        struct appdata_t
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 id : TEXCOORD01;
        };

        struct v2f
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 worldcoord : TEXCOORD1;
			float4 outlinecord : TEXCOORD02;
        };

        v2f vert (appdata_t v)
        {
            v2f o;
				float4x4 scaleMat;
				scaleMat[0] = float4(1.0 + 0.15 / (v.vertex[0] * 10), 0, 0, 0);
				scaleMat[1] = float4(0, 1.0 + 0.15 / (v.vertex[1] * 10), 0, 0);
				scaleMat[2] = float4(0, 0, 1, 0);
				scaleMat[3] = float4(0, 0, 0, 1);
				o.vertex = mul( UNITY_MATRIX_MVP, mul (scaleMat, v.vertex));
			
            o.texcoord = v.texcoord;

			o.worldcoord = v.vertex;
			
            return o;
        }

        half4 frag (v2f IN) : COLOR
        {
				half4 texttex = half4(_OutlineColor.x, _OutlineColor.y, _OutlineColor.z, tex2D(_MainTex, IN.texcoord).a);
				return texttex;
        }
        ENDCG
    }

	Pass
    {
        Cull Off
        Lighting Off
        ZWrite Off
		ZTest off
        Fog { Mode Off }
        ColorMask RGB
        Blend SrcAlpha OneMinusSrcAlpha		

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
		float3 _Color;
		float3 _OutlineColor;

        struct appdata_t
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 id : TEXCOORD01;
        };

        struct v2f
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 worldcoord : TEXCOORD1;
			float4 outlinecord : TEXCOORD02;
        };

        v2f vert (appdata_t v)
        {
            v2f o;

			float4x4 scaleMat;
			scaleMat[0] = float4(1.0 + 0.15 / (-v.vertex[0] * 10), 0, 0, 0);
			scaleMat[1] = float4(0, 1.0 + 0.15 / (-v.vertex[1] * 10), 0, 0);
			scaleMat[2] = float4(0, 0, 1, 0);
			scaleMat[3] = float4(0, 0, 0, 1);

			o.vertex = mul( UNITY_MATRIX_MVP, mul (scaleMat, v.vertex));
			
            o.texcoord = v.texcoord;

			o.worldcoord = v.vertex;
			
            return o;
        }

         half4 frag (v2f IN) : COLOR
        {
				half4 texttex = half4(_OutlineColor.x, _OutlineColor.y, _OutlineColor.z, tex2D(_MainTex, IN.texcoord).a);
				return texttex;
        }
        ENDCG
    }

	Pass
    {
        Cull Off
        Lighting Off
        ZWrite Off
		ZTest off
        Fog { Mode Off }
        ColorMask RGB
        Blend SrcAlpha OneMinusSrcAlpha		

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
		float3 _Color;
		float3 _OutlineColor;

        struct appdata_t
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 id : TEXCOORD01;
        };

        struct v2f
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 worldcoord : TEXCOORD1;
			float4 outlinecord : TEXCOORD02;
        };

        v2f vert (appdata_t v)
        {
            v2f o;
			float4x4 scaleMat;
			scaleMat[0] = float4(1.0 + 0.15 / (-v.vertex[0] * 10), 0, 0, 0);
			scaleMat[1] = float4(0, 1.0 + 0.15 / (v.vertex[1] * 10), 0, 0);
			scaleMat[2] = float4(0, 0, 1, 0);
			scaleMat[3] = float4(0, 0, 0, 1);

			o.vertex = mul( UNITY_MATRIX_MVP, mul (scaleMat, v.vertex));
			
            o.texcoord = v.texcoord;

			o.worldcoord = v.vertex;
			
            return o;
        }

         half4 frag (v2f IN) : COLOR
        {
				half4 texttex = half4(_OutlineColor.x, _OutlineColor.y, _OutlineColor.z, tex2D(_MainTex, IN.texcoord).a);
				return texttex;
        }
        ENDCG
    }

	Pass
    {
        Cull Off
        Lighting Off
        ZWrite Off
		ZTest off
        Fog { Mode Off }
        ColorMask RGB
        Blend SrcAlpha OneMinusSrcAlpha		

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
		float3 _Color;
		float3 _OutlineColor;

        struct appdata_t
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 id : TEXCOORD01;
        };

        struct v2f
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 worldcoord : TEXCOORD1;
			float4 outlinecord : TEXCOORD02;
        };

        v2f vert (appdata_t v)
        {
            v2f o;
			float4x4 scaleMat;
			scaleMat[0] = float4(1.0 + 0.15 / (v.vertex[0] * 10), 0, 0, 0);
			scaleMat[1] = float4(0, 1.0 + 0.15 / (-v.vertex[1] * 10), 0, 0);
			scaleMat[2] = float4(0, 0, 1, 0);
			scaleMat[3] = float4(0, 0, 0, 1);

			o.vertex = mul( UNITY_MATRIX_MVP, mul (scaleMat, v.vertex));
			
            o.texcoord = v.texcoord;

			o.worldcoord = v.vertex;
			
            return o;
        }

         half4 frag (v2f IN) : COLOR
        {
				half4 texttex = half4(_OutlineColor.x, _OutlineColor.y, _OutlineColor.z, tex2D(_MainTex, IN.texcoord).a);
				return texttex;
        }
        ENDCG
    }

	Pass
    {
        Cull Off
        Lighting Off
        ZWrite Off
		ZTest off
        Fog { Mode Off }
        ColorMask RGB
        Blend SrcAlpha OneMinusSrcAlpha		

        CGPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "UnityCG.cginc"

        sampler2D _MainTex;
        float4 _MainTex_ST;
		float3 _Color;
		float3 _OutlineColor;

        struct appdata_t
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
        };

        struct v2f
        {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
			float4 worldcoord : TEXCOORD1;
			float4 outlinecord : TEXCOORD02;
			
        };

        v2f vert (appdata_t v)
        {
            v2f o;
            o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
            o.texcoord = v.texcoord;

			o.worldcoord = v.vertex;
			o.outlinecord = o.vertex + 0.1;
            return o;
        }

         half4 frag (v2f IN) : COLOR
        {
				half4 texttex = half4(_Color.x, _Color.y, _Color.z, tex2D(_MainTex, IN.texcoord).a);
				return texttex;
        }
        ENDCG
    }
}
}