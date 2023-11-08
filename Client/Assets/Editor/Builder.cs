using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.Build.Reporting;
using UnityEditor;
using UnityEngine;

namespace Assets.Editor
{
    public class Builder
    {
        private static void BuildEmbeddedLinux(EmbeddedLinuxArchitecture architecture)
        {
            // Set architecture in BuildSettings
            EditorUserBuildSettings.selectedEmbeddedLinuxArchitecture = architecture;

            // Setup build options (e.g. scenes, build output location)
            var options = new BuildPlayerOptions
            {
                // Change to scenes from your project
                scenes = new[]
                {
                    "Assets/Scenes/SampleScene.unity",
                },
                // Change to location the output should go
                locationPathName = "../Build/",
                options = BuildOptions.None,
                target = BuildTarget.EmbeddedLinux
            };

            var report = BuildPipeline.BuildPlayer(options);

            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"Build successful - Build written to {options.locationPathName}");
            }
            else if (report.summary.result == BuildResult.Failed)
            {
                Debug.LogError($"Build failed");
            }
        }

        // This function will be called from the build process
        public static void Build()
        {
            // Build EmbeddedLinux ARM64 Unity player
            BuildEmbeddedLinux(EmbeddedLinuxArchitecture.Arm64);
        }
    }
}
