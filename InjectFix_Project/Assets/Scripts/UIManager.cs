using UnityEngine;

[CreateAssetMenu(fileName = "UIManager", menuName = "Scriptable Objects/UIManager")]
public class UIManager : ScriptableObject
{
    [SerializeField]
    private GameObject[] _uiPrefabs;

    public void LoadMainUI()
    {
        if (_uiPrefabs == null || _uiPrefabs.Length == 0)
        {
            Debug.LogError("没有找到任何UI，加载失败");
            return;
        }

        GameObject.Instantiate(_uiPrefabs[0]);
    }
}
