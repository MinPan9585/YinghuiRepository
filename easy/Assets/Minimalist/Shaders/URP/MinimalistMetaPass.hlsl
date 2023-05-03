#ifndef URP_MINIMALIST_META_INCLUDED
    #define URP_MINIMALIST_META_INCLUDED

 #include "MinimalistInput.hlsl"
 #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UniversalMetaPass.hlsl"

     Varyings MinimalistVertexMeta(Attributes input)
     {
         Varyings output = (Varyings)0;
         output.positionCS = UnityMetaVertexPosition(input.positionOS.xyz, input.uv1, input.uv2);
         output.uv = TRANSFORM_TEX(input.uv0, _BaseMap);
         return output;
     }
     
     half4 MinimalistFragmentMeta(Varyings input) : SV_Target
     {
         float3 surfaceColor = float3(1, 1, 1); // Get custom shading color here
         MetaInput metaInput;
         metaInput.Albedo = surfaceColor * SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv).rgb;
         metaInput.Emission = half3(0, 0, 0);
         return MetaFragment(metaInput);
     }
     
#endif