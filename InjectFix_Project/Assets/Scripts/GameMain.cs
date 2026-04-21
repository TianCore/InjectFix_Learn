using UnityEngine;

public class GameMain : MonoBehaviour
{
    private void Awake()
    {
        HotfixPatchLoader.TryLoadFromStreamingAssets();
        Debug.Log("ĺ č˝˝čĄĽä¸");
        GameManager.Instance.LoadMainUI();
    }
}
