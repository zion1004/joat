Shader "Outlines/BackFaceOutlines" {
    Properties{
        _Thickness("Thickness", Float) = 1 // The amount to extrude the outline mesh
        _FullHealthColor("Full Health Color", Color) = (0, 1, 0, 1)
        _LowHealthColor("Low Health Color", Color) = (1, 0, 0, 1)
         _HealthRatio("Health Ratio", Range(0, 1)) = 1
        _DepthOffset("Depth offset", Range(0, 1)) = 0 // An offset to the clip space Z, pushing the outline back
        // If enabled, this shader will use "smoothed" normals stored in TEXCOORD1 to extrude along
        [Toggle(USE_PRECALCULATED_OUTLINE_NORMALS)]_PrecalculateNormals("Use UV1 normals", Float) = 0
    }
    SubShader{
        Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

        Pass {
            Name "Outlines"
            // Cull front faces
            Cull Front

            HLSLPROGRAM
            // Standard URP requirements
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x

            // Register our material keywords
            #pragma shader_feature USE_PRECALCULATED_OUTLINE_NORMALS

            // Register our functions
            #pragma vertex Vertex
            #pragma fragment Fragment

            // Include our logic file
            #include "BackFaceOutlines.hlsl"    

            ENDHLSL
        }
    }
}