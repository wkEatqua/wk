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
    internal static class Builder
    {
        public static void Build()
        {
            var options = GetValidatedOptions();

            if (options["buildMode"] == "debug")
            {
                EditorUserBuildSettings.development = true;
                EditorUserBuildSettings.allowDebugging = true;
                EditorUserBuildSettings.waitForManagedDebugger = true;
            }

            var scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(s => s.path).ToArray();
            var scenesWantExclude = new[] { 
                "Login Scene",
            };
            var excludedScenes = scenes.Where(scene => !scenesWantExclude.Any(e => scene.Contains(e))).ToArray();
            var playerOptions = new BuildPlayerOptions
            {
                scenes = excludedScenes,
                locationPathName = "../build/",
                options = BuildOptions.None,
                target = BuildTarget.StandaloneWindows64
            };
            
            var report = BuildPipeline.BuildPlayer(playerOptions);
            if (report.summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"Build successful");
            }
            else if (report.summary.result == BuildResult.Failed)
            {
                Debug.LogError($"Build failed");
            }
        }

        // https://note4iffydog.tistory.com/55

        private static Dictionary<string, string> GetValidatedOptions()
        {
            ParseCommandLineArguments(out Dictionary<string, string> validatedOptions);

            if (!validatedOptions.TryGetValue("buildMode", out var _))
            {
                const string defaultEnableBuildModeValue = "release";
                validatedOptions.Add("buildMode", defaultEnableBuildModeValue);
            }

            return validatedOptions;
        }

        private static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments)
        {
            string[] args = Environment.GetCommandLineArgs();

            providedArguments = new Dictionary<string, string>();
            for (int current = 0, next = 1; current < args.Length; current++, next++)
            {
                if (!args[current].StartsWith("-")) continue;

                var flag = args[current].TrimStart('-');

                var flagHasValue = next < args.Length && !args[next].StartsWith("-");
                var value = flagHasValue ? args[next].TrimStart('-') : "";

                providedArguments.Add(flag, value);
            }
        }
    }
}
