#ifndef URP_MINIMALIST_SHADOWCASTER_INCLUDED
    #define URP_MINIMALIST_SHADOWCASTER_INCLUDED

    #include "MinimalistInput.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
    
    

    float3 _LightDirection;
    
    struct Attributes
    {
        float4 positionOS   : POSITION;
        float3 normalOS     : NORMAL;
        float2 texcoord     : TEXCOORD0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
    };
    
    struct Varyings
    {
        float2 uv           : TEXCOORD0;
        float4 positionCS   : SV_POSITION;
    };
    
    float4 GetShadowPositionHClip(Attributes input)
    {
        float3 positionWS = TransformObjectToWorld(input.positionOS.xyz);
        float3 normalWS = TransformObjectToWorldNormal(input.normalOS);
    
        float4 positionCS = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, _LightDirection));
    
        #if UNITY_REVERSED_Z
            positionCS.z = min(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
        #else
            positionCS.z = max(positionCS.z, positionCS.w * UNITY_NEAR_CLIP_VALUE);
        #endif
    
        return positionCS;
    }
    
    Varyings ShadowPassVertex(Attributes input)
    {
        Varyings output;
        UNITY_SETUP_INSTANCE_ID(input);
    
        output.uv = TRANSFORM_TEX(input.texcoord, _BaseMap);
        output.positionCS = GetShadowPositionHClip(input);
        return output;
    }
    
    half4 ShadowPassFragment(Varyings input) : SV_TARGET
    {
        half alpha = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, input.uv).a;
        #if defined(_ALPHATEST_ON)
            clip(alpha - 0.05);
        #endif
        return 0;
    }
#endif