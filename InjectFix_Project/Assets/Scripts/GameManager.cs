using UnityEngine;

public class GameManager
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameManager();
            }
            return _instance;
        }
    }

    // Assets/Resources/UIManager.asset
    public const string UIManagerPath = "UIManager"; // 不需要后缀，也不需要完整路径

    public void LoadMainUI()
    {
        var manager = Resources.Load<UIManager>(UIManagerPath);
        if (manager == null)
        {
            Debug.LogError("UIManager 加载失败，UI加载失败");
            return;
        }

        manager.LoadMainUI();
    }
}
