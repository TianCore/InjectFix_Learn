// using UnityEditor;
// using UnityEditor.Build;
// using UnityEditor.Build.Reporting;
// using System.IO;
// using UnityEngine;
// using System.Collections.Generic;
// using System;
// using System.Linq;

// public class InjectFixBuildProcessor : IPostprocessBuildWithReport
// {
//     public int callbackOrder => 999; // 越大越晚执行，确保在其他处理之后

//     public void OnPostprocessBuild(BuildReport report)
//     {
//         var target = report.summary.platform;
//         var outputPath = report.summary.outputPath;

//         Debug.Log($"[InjectFix] 开始执行注入，平台: {target}, 输出路径: {outputPath}");

//         RunInjectFix(target, outputPath);
//     }

//     private static void RunInjectFix(BuildTarget target, string outputPath)
//     {
//         // 找到 IFix.exe / IFix.CLI 工具路径
//         string ifixToolPath = Path.Combine(
//             Application.dataPath,
//             "../IFixToolKit/IFix.exe"  // 根据你项目实际路径调整
//         );

//         if (!File.Exists(ifixToolPath))
//         {
//             Debug.LogError($"[InjectFix] 找不到 IFix.exe: {ifixToolPath}");
//             return;
//         }


//         foreach (var assembly in IFix.Editor.IFixEditor.injectAssemblys)
//         {
//             // 找到编译后的 dll 路径（不同平台路径不同）
//             string dllPath = GetDllPath(target, outputPath);
//             Debug.Log("编译后的 dll 路径: " + dllPath);

//             CallIFix(dllPath, assembly);
//         }

//         // 调用 InjectFix CLI 执行注入
//         // var process = new System.Diagnostics.Process();
//         // process.StartInfo.FileName = ifixToolPath;
//         // process.StartInfo.Arguments = $"Inject \"{dllPath}\"";
//         // process.StartInfo.UseShellExecute = false;
//         // process.StartInfo.RedirectStandardOutput = true;
//         // process.StartInfo.RedirectStandardError = true;
//         // process.Start();

//         // string output = process.StandardOutput.ReadToEnd();
//         // string error = process.StandardError.ReadToEnd();
//         // process.WaitForExit();

//         // if (process.ExitCode != 0)
//         //     Debug.LogError($"[InjectFix] 注入失败:\n{error}");
//         // else
//         //     Debug.Log($"[InjectFix] 注入成功:\n{output}");
//     }

//     private static void CallIFix(string assembly_path, string assembly)
//     {
//         var processCfgPath = "./process_cfg";
//         var core_path = "./Assets/Plugins/IFix.Core.dll";
//         var patch_path = string.Format("./{0}.ill.bytes", assembly);

//         List<string> args = new List<string>() { "-inject", core_path, assembly_path,
//             processCfgPath, patch_path, assembly_path };

//         foreach (var path in
//             (from asm in AppDomain.CurrentDomain.GetAssemblies()
//                 select Path.GetDirectoryName(asm.ManifestModule.FullyQualifiedName)).Distinct())
//         {
//             try
//             {
//                 //UnityEngine.Debug.Log("searchPath:" + path);
//                 args.Add(path);
//             }
//             catch { }
//         }

//         IFix.Editor.IFixEditor.CallIFix(args);
//     }

//     private static string GetDllPath(BuildTarget target, string outputPath)
//     {
//         string appName = Path.GetFileNameWithoutExtension(outputPath);
//         outputPath = outputPath.Substring(0, outputPath.LastIndexOf("/"));

//         return target switch
//         {
//             BuildTarget.Android =>
//                 // Android AAB/APK 打包后需要从 Temp 目录找，或用 Gradle hook
//                 Path.Combine("Temp/StagingArea/assets/bin/Data/Managed", "Assembly-CSharp.dll"),

//             BuildTarget.iOS =>
//                 Path.Combine(outputPath, "Libraries/Managed", "Assembly-CSharp.dll"),

//             BuildTarget.StandaloneWindows64 =>
//                 Path.Combine(outputPath, $"{appName}_Data/Managed", "Assembly-CSharp.dll"),

//             BuildTarget.StandaloneOSX =>
//                 Path.Combine(outputPath, $"{appName}.app/Contents/Resources/Data/Managed", "Assembly-CSharp.dll"),

//             _ => throw new System.NotSupportedException($"不支持平台: {target}")
//         };
//     }
// }