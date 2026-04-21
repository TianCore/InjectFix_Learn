using UnityEditor;
using UnityEngine;

public class DebugWindow : EditorWindow
{
    private string persistentDataPath = string.Empty;

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
    }

    private void RefreshPath()
    {
        persistentDataPath = Application.persistentDataPath;
    }
}
