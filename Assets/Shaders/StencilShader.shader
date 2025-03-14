Shader "Unlit/StencilShader"
{
    Properties
    {
        [IntRange] _StencilID ("Stencil ID", Range(0,255)) = 0
    }
    SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "RenderPipeline" = "UniversalPipeline"
            "Queue" = "Geometry"
        }
        

        Pass
        {
            Blend Zero One
            ZWrite Off

            Stencil
            {
                ref [_StencilID]      
                Comp Always
                Pass Replace
                Fail Keep
            }
        }
    }
}
