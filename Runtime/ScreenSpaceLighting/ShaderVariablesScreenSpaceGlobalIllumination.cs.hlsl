//
// This file was automatically generated. Please don't edit by hand. Execute Editor command [ Edit > Rendering > Generate Shader Includes ] instead
//

#ifndef SHADERVARIABLESSCREENSPACEGLOBALILLUMINATION_CS_HLSL
#define SHADERVARIABLESSCREENSPACEGLOBALILLUMINATION_CS_HLSL
// Generated from UnityEngine.Rendering.Universal.ShaderVariablesScreenSpaceGlobalIllumination
// PackingRules = Exact
CBUFFER_START(ShaderVariablesScreenSpaceGlobalIllumination)
    float4 _ColorPyramidUvScaleAndLimitPrevFrame;
    float4 _SSGIScreenSize;
    float _RayMarchingThicknessScale;
    float _RayMarchingThicknessBias;
    int _RayMarchingSteps;
    int _RayMarchingReflectsSky;
    int _RayMarchingFallbackHierarchy;
    int _IndirectDiffuseFrameIndex;
    int _SSGIColorPyramidMaxMip;
    int _SSGIDepthPyramidMaxMip;
    float4 _SSGITraceScreenSize;
    float _SSGIIntensity;
    float _SSGIDepthTolerance;
    float _SSGINormalTolerance;
    int _SSGITemporalMaxSamples;
    int _SSGIMaxSampleAge;
    float _SSGISpatialRadius;
    int _SSGISpatialSampleCount;
    int _SSGICandidateCount;
    int _SSGIFrameIndex;
    float _SSGIEnableTemporalJacobian;
    int _SSGIHistoryValid;
    float _SSGIEnableSpatialJacobian;
CBUFFER_END


#endif
