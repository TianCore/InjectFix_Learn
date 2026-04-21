using System.IO;
using IFix.Core;
using UnityEngine;

public static class HotfixPatchLoader
{
    private const string DefaultPatchFileName = "Assembly-CSharp.patch.bytes";

    public static bool TryLoadFromStreamingAssets(string patchFileName = DefaultPatchFileName)
    {
        CheckInjectBridge();

        string patchPath = Path.Combine(Application.streamingAssetsPath, patchFileName);
        Debug.Log($"准备加载补丁，patchPath: {patchPath}");
        if (!File.Exists(patchPath))
        {
            Debug.LogWarning($"未找到补丁文件: {patchPath}");
            return false;
        }

        byte[] patchBytes = File.ReadAllBytes(patchPath);
        if (patchBytes == null || patchBytes.Length == 0)
        {
            Debug.LogWarning("补丁文件为空，跳过加载");
            return false;
        }

        if (TryLoadPatch(patchPath))
        {
            Debug.Log($"补丁加载成功: {patchPath}");
            return true;
        }

        Debug.LogError("补丁加载失败：IFix.Core.PatchManager.Load 调用未成功（请确认已执行 Inject）");
        return false;
    }

    private static bool TryLoadPatch(string patchPath)
    {
        try
        {
            Debug.Log($"调用 PatchManager.Load，patchPath: {patchPath}");
            PatchManager.Load(patchPath);
            Debug.Log("使用 IFix.Core.PatchManager.Load(string) 加载补丁");
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"调用 IFix.Core.PatchManager 失败: {e.GetType().Name}: {e.Message}\n{e.StackTrace}");
        }

        return false;
    }

    private static void CheckInjectBridge()
    {
        var bridgeType = System.Type.GetType("IFix.ILFixInterfaceBridge, Assembly-CSharp");
        if (bridgeType == null)
        {
            Debug.LogError("Inject 自检失败：当前包未注入，找不到 IFix.ILFixInterfaceBridge。");
            return;
        }

        Debug.Log("Inject 自检通过：已找到 IFix.ILFixInterfaceBridge。");
    }
}
