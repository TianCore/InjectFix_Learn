using UnityEngine;

public class GameMain : MonoBehaviour
{
    private void Awake()
    {
        // HotfixPatchLoader.TryLoadFromStreamingAssets();
        // Debug.Log("加载补丁完成");
        GameManager.Instance.LoadMainUI();
    }
}
