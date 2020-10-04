Shader "Custom/Stencil"
{

	Properties{}

		SubShader{

		Tags {
		 "RenderType" = "Opaque"
		 }
		Stencil{
			ref 1
			Comp Always
			pass Replace
		}
		 Pass{
		 ZWrite Off
		 }
	}
}