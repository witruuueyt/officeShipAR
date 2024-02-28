using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;
#if OPEN_XR_ENABLED
using UnityEditor.XR.Management;
using UnityEditor.XR.OpenXR.Features;
using UnityEngine.XR.OpenXR;
using UnityEngine.XR.OpenXR.Features.Interactions;
#endif

public class XRSettingsValidator: IPreprocessBuildWithReport
{
    public int callbackOrder => 0;

    const string HOLOLENS_FEATURE_SET_ID = "com.microsoft.openxr.featureset.hololens";
    const string HAND_TRACKING_FEATURE_ID = "com.microsoft.openxr.feature.handtracking";
    
    public void OnPreprocessBuild(BuildReport report)
    {
#if !OPEN_XR_ENABLED
        Debug.LogException(new BuildFailedException("Vuforia HoloLens 2 Sample requires the Open XR Plugin to be installed"));
#else
        var xrGeneralSettings = XRGeneralSettingsPerBuildTarget.XRGeneralSettingsForBuildTarget(BuildTargetGroup.WSA);
        if (!xrGeneralSettings.InitManagerOnStart)
            Debug.LogException(new BuildFailedException("Vuforia HoloLens 2 Sample requires the XR Loader to initialize on Startup."));

        var xrLoaders = xrGeneralSettings.AssignedSettings.activeLoaders;
        if (!xrLoaders.Any(l => l is OpenXRLoader))
            Debug.LogException(new BuildFailedException("Vuforia HoloLens 2 Sample requires the Open XR Loader to be enabled in the XR Plug-in Management Settings."));

        var featureSets = OpenXRFeatureSetManager.FeatureSetsForBuildTarget(BuildTargetGroup.WSA);
        var hlFeatureSet = featureSets.First(fs => fs.featureSetId.Equals(HOLOLENS_FEATURE_SET_ID));
        if (!hlFeatureSet.isEnabled)
            Debug.LogException(new BuildFailedException("Vuforia HoloLens 2 Sample requires the Microsoft HoloLens Feature Group to be enabled."));

        var openXRSettings = OpenXRSettings.GetSettingsForBuildTargetGroup(BuildTargetGroup.WSA);
        if (openXRSettings.depthSubmissionMode != OpenXRSettings.DepthSubmissionMode.Depth16Bit)
            Debug.LogWarning("Depth Submission Mode for HoloLens should be set to 16-Bit.");

        if (openXRSettings.renderMode != OpenXRSettings.RenderMode.SinglePassInstanced)
            Debug.LogException(new BuildFailedException("Vuforia HoloLens 2 Sample requires the Open XR Render Mode on UWP to be set to Single Pass Instanced."));
        
        var interactionFeatures = FeatureHelpers.GetFeatureWithIdForBuildTarget(BuildTargetGroup.WSA, MicrosoftHandInteraction.featureId);
        if (interactionFeatures == null || !interactionFeatures.enabled)
            Debug.LogException(new BuildFailedException("Vuforia HoloLens 2 Sample requires the Microsoft Hand Interaction Profile to be enabled."));

        var handTracking = FeatureHelpers.GetFeatureWithIdForBuildTarget(BuildTargetGroup.WSA, HAND_TRACKING_FEATURE_ID);
        if (!handTracking.enabled)
            Debug.LogException(new BuildFailedException("Vuforia HoloLens 2 Sample requires the Hand Tracking feature to be enabled."));
#endif
    }
}