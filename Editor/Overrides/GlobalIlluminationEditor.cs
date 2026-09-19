using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace UnityEditor.Rendering.Universal
{
    [CustomEditor(typeof(GlobalIllumination))]
    sealed class GlobalIlluminationEditor : VolumeComponentEditor
    {
        SerializedDataParameter m_Enable;
        SerializedDataParameter m_Tracing;
        SerializedDataParameter m_RayMiss;

        SerializedDataParameter m_FullResolutionSS;
        SerializedDataParameter m_DepthBufferThickness;
        SerializedDataParameter m_RaySteps;
        SerializedDataParameter m_RayLength;
        SerializedDataParameter m_SampleCount;
        SerializedDataParameter m_ClampValue;
        SerializedDataParameter m_TemporalDenoise;
        SerializedDataParameter m_TemporalDenoiseAccumulation;
        SerializedDataParameter m_SpatialDenoise;
        SerializedDataParameter m_TemporalReuse;
        SerializedDataParameter m_SpatialReuse;
        SerializedDataParameter m_SpatialSamples;
        SerializedDataParameter m_SpatialRadius;
        SerializedDataParameter m_MaxHistoryLength;
        SerializedDataParameter m_MaxSampleAge;
        SerializedDataParameter m_NormalThreshold;
        SerializedDataParameter m_DepthThreshold;
        SerializedDataParameter m_TemporalJacobian;
        SerializedDataParameter m_SpatialJacobian;
        SerializedDataParameter m_Intensity;
        SerializedDataParameter m_LayerMask;

        public override void OnEnable()
        {
            var o = new PropertyFetcher<GlobalIllumination>(serializedObject);

            m_Enable = Unpack(o.Find(x => x.enabled));
            m_Tracing = Unpack(o.Find(x => x.tracing));
            m_RayMiss = Unpack(o.Find(x => x.rayMiss));

            m_FullResolutionSS = Unpack(o.Find(x => x.fullResolutionSS));
            m_DepthBufferThickness = Unpack(o.Find(x => x.depthBufferThickness));
            m_RaySteps = Unpack(o.Find(x => x.maxRaySteps));
            m_RayLength = Unpack(o.Find(x => x.rayLength));
            m_SampleCount = Unpack(o.Find(x => x.sampleCount));
            m_ClampValue = Unpack(o.Find(x => x.clampValue));
            m_TemporalDenoise = Unpack(o.Find(x => x.temporalDenoise));
            m_TemporalDenoiseAccumulation = Unpack(o.Find(x => x.temporalDenoiseAccumulation));
            m_SpatialDenoise = Unpack(o.Find(x => x.spatialDenoise));
            m_TemporalReuse = Unpack(o.Find(x => x.restirTemporalReuse));
            m_SpatialReuse = Unpack(o.Find(x => x.restirSpatialReuse));
            m_SpatialSamples = Unpack(o.Find(x => x.restirSpatialSamples));
            m_SpatialRadius = Unpack(o.Find(x => x.restirSpatialRadius));
            m_MaxHistoryLength = Unpack(o.Find(x => x.restirMaxHistoryLength));
            m_MaxSampleAge = Unpack(o.Find(x => x.restirMaxSampleAge));
            m_NormalThreshold = Unpack(o.Find(x => x.restirNormalThreshold));
            m_DepthThreshold = Unpack(o.Find(x => x.restirDepthThreshold));
            m_TemporalJacobian = Unpack(o.Find(x => x.restirTemporalJacobian));
            m_SpatialJacobian = Unpack(o.Find(x => x.restirSpatialJacobian));
            m_Intensity = Unpack(o.Find(x => x.restirIntensity));
            m_LayerMask = Unpack(o.Find(x => x.layerMask));

            base.OnEnable();
        }

        static public readonly GUIContent k_Enabled = EditorGUIUtility.TrTextContent("State", "Enable Screen Space Global Illumination.");
        static public readonly GUIContent k_TracingText = EditorGUIUtility.TrTextContent("Tracing", "Controls the technique used to compute the global illumination. Ray marching uses a ray-marched screen-space solution, Ray tracing uses a hardware accelerated world-space solution. Mixed uses first Ray marching, then Ray tracing if it fails to intersect on-screen geometry.");
        static public readonly GUIContent k_FullResolutionSSText = EditorGUIUtility.TrTextContent("Full Resolution", "Controls if the screen space global illumination should be evaluated at full resolution.");
        static public readonly GUIContent k_DepthBufferThicknessText = EditorGUIUtility.TrTextContent("Depth Tolerance", "Controls the tolerance when comparing the depth of two pixels.");
        static public readonly GUIContent k_RayStepsText = EditorGUIUtility.TrTextContent("Max Ray Steps", "Sets the maximum number of steps used for ray marching. Affects both correctness and performance.");
        static public readonly GUIContent k_RayMissFallbackHierarchyText = EditorGUIUtility.TrTextContent("Ray Miss", "Controls the fallback hierarchy for indirect diffuse in case the ray misses.");

        public override void OnInspectorGUI()
        {
            PropertyField(m_Enable, k_Enabled);

            PropertyField(m_Tracing, k_TracingText);

            RayCastingMode tracingMode = m_Tracing.value.GetEnumValue<RayCastingMode>();
            bool rayTracingSettingsDisplayed = m_Tracing.overrideState.boolValue
                && tracingMode != RayCastingMode.RayMarching;

            if (rayTracingSettingsDisplayed)
            {
                EditorGUILayout.LabelField("ReSTIR GI", EditorStyles.boldLabel);
                PropertyField(m_LayerMask);
                PropertyField(m_RayLength);
                PropertyField(m_SampleCount);
                PropertyField(m_ClampValue);
                PropertyField(m_Intensity);
                PropertyField(m_TemporalReuse);
                using (new IndentLevelScope())
                {
                    PropertyField(m_MaxHistoryLength);
                    PropertyField(m_MaxSampleAge);
                    PropertyField(m_TemporalJacobian);
                }
                PropertyField(m_SpatialReuse);
                using (new IndentLevelScope())
                {
                    PropertyField(m_SpatialSamples);
                    PropertyField(m_SpatialRadius);
                    PropertyField(m_SpatialJacobian);
                }
                PropertyField(m_NormalThreshold);
                PropertyField(m_DepthThreshold);
                PropertyField(m_TemporalDenoise);
                using (new IndentLevelScope())
                {
                    PropertyField(m_TemporalDenoiseAccumulation);
                }
                PropertyField(m_SpatialDenoise);
            }
            else
            {
                PropertyField(m_FullResolutionSS, k_FullResolutionSSText);
                PropertyField(m_DepthBufferThickness, k_DepthBufferThicknessText);
                m_DepthBufferThickness.value.floatValue = Mathf.Clamp(m_DepthBufferThickness.value.floatValue, 0.001f, 0.5f);

                using (new IndentLevelScope())
                {
                    PropertyField(m_RaySteps, k_RayStepsText);
                    m_RaySteps.value.intValue = Mathf.Max(0, m_RaySteps.value.intValue);
                }

                PropertyField(m_RayMiss, k_RayMissFallbackHierarchyText);
            }
        }
    }
}
