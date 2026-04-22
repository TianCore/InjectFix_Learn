using UnityEditor;
using UnityEngine;

public class DebugWindow : EditorWindow
{
    private string persistentDataPath = string.Empty;
    private string injectCheckResult = "未检测";
    private string runtimeInjectTestResult = "未测试";
    private string patchFileName = "Assembly-CSharp.patch.bytes";
    private string patchLoadTestResult = "未测试";

    [MenuItem("Tools/Debug/Persistent Data Path Window")]
    public static void Open()
    {
        var window = GetWindow<DebugWindow>("Debug Window");
        window.minSize = new Vector2(480f, 140f);
        window.RefreshPath();
        window.Show();
    }

    private void OnEnable()
    {
        RefreshPath();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Application.persistentDataPath", EditorStyles.boldLabel);

        EditorGUILayout.TextArea(persistentDataPath, GUILayout.Height(48f));
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Refresh"))
        {
            RefreshPath();
        }

        if (GUILayout.Button("Copy"))
        {
            EditorGUIUtility.systemCopyBuffer = persistentDataPath;
            Debug.Log("已复制 persistentDataPath 到剪贴板。");
        }

        if (GUILayout.Button("Print To Console"))
        {
            Debug.Log($"Application.persistentDataPath: {persistentDataPath}");
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Inject 状态检测", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(injectCheckResult, MessageType.Info);
        if (GUILayout.Button("Check IFix.ILFixInterfaceBridge"))
        {
            CheckInjectBridge();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("运行时注入测试", EditorStyles.boldLabel);
        EditorGUILayout.HelpBox(runtimeInjectTestResult, MessageType.Info);
        using (new EditorGUI.DisabledScope(!EditorApplication.isPlaying))
        {
            if (GUILayout.Button("Run Runtime Inject Test (PlayMode)"))
            {
                RunRuntimeInjectTest();
            }
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("PatchManager.Load 注入测试", EditorStyles.boldLabel);
        patchFileName = EditorGUILayout.TextField("Patch 文件名", patchFileName);
        EditorGUILayout.HelpBox(patchLoadTestResult, MessageType.Info);
        using (new EditorGUI.DisabledScope(!EditorApplication.isPlaying))
        {
            if (GUILayout.Button("Run PatchManager.Load Test (PlayMode)"))
            {
                HotfixPatchLoader.TryLoadFromStreamingAssets(patchFileName);
            }
        }

        if (!EditorApplication.isPlaying)
        {
            EditorGUILayout.HelpBox("请先进入 Play 模式，再执行运行时注入测试。", MessageType.Warning);
        }
    }

    private void RefreshPath()
    {
        persistentDataPath = Application.persistentDataPath;
    }

    private void CheckInjectBridge()
    {
        var bridgeType =
            System.Type.GetType("IFix.ILFixInterfaceBridge, Assembly-CSharp")
            ?? System.Type.GetType("IFix.ILFixInterfaceBridge");

        if (bridgeType == null)
        {
            injectCheckResult =
                "检测失败：assembly may be not injected yet, can not find IFix.ILFixInterfaceBridge";
            Debug.LogError($"{injectCheckResult}\n请先执行 InjectFix/Inject，并避免 Inject 后再次触发脚本重编译。");
            return;
        }

        injectCheckResult = $"检测通过：已找到 {bridgeType.FullName}";
        Debug.Log(injectCheckResult);
    }

    private void RunRuntimeInjectTest()
    {
        if (!EditorApplication.isPlaying)
        {
            runtimeInjectTestResult = "运行时测试失败：当前不在 Play 模式。";
            Debug.LogWarning(runtimeInjectTestResult);
            return;
        }

        var bridgeType =
            System.Type.GetType("IFix.ILFixInterfaceBridge, Assembly-CSharp")
            ?? System.Type.GetType("IFix.ILFixInterfaceBridge");

        if (bridgeType == null)
        {
            runtimeInjectTestResult =
                "运行时测试失败：assembly may be not injected yet, can not find IFix.ILFixInterfaceBridge";
            Debug.LogError($"{runtimeInjectTestResult}\n请重新执行 InjectFix/Inject，并确保测试前未触发脚本重编译。");
            return;
        }

        runtimeInjectTestResult = $"运行时测试通过：已找到 {bridgeType.FullName}";
        Debug.Log(runtimeInjectTestResult);
    }
}
