namespace UnityEngine.Rendering.Universal
{
    [GenerateHLSL(needAccessors = false, generateCBuffer = true)]
    unsafe struct ShaderVariablesScreenSpaceGlobalIllumination
    {
        public Vector4 _ColorPyramidUvScaleAndLimitPrevFrame;
        public Vector4 _SSGIScreenSize;

        public float _RayMarchingThicknessScale;
        public float _RayMarchingThicknessBias;
        public int _RayMarchingSteps;
        public int _RayMarchingReflectsSky;

        public int _RayMarchingFallbackHierarchy;
        public int _IndirectDiffuseFrameIndex;
        public int _SSGIColorPyramidMaxMip;
        public int _SSGIDepthPyramidMaxMip;

        public Vector4 _SSGITraceScreenSize;
        public float _SSGIIntensity;
        public float _SSGIDepthTolerance;
        public float _SSGINormalTolerance;
        public int _SSGITemporalMaxSamples;

        public int _SSGIMaxSampleAge;
        public float _SSGISpatialRadius;
        public int _SSGISpatialSampleCount;
        public int _SSGICandidateCount;

        public int _SSGIFrameIndex;
        public float _SSGIEnableTemporalJacobian;
        public int _SSGIHistoryValid;
        public float _SSGIEnableSpatialJacobian;
    }
}
