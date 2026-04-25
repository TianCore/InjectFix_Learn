using IFix;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : MonoBehaviour
{
    [SerializeField]
    private Text _title;
    
    [Patch]
    public void OnClickStart()
    {
        SetTitle("游戏开始补丁1", "#3EC3CAFF");
    }

    public void OnClickEnd()
    {
        SetTitle("游戏结束", "#DAA520FF");
    }

    public void OnClickInject()
    {
        SetTitle("注入Inject", "#8470FFFF");
        HotfixPatchLoader.TryLoadFromStreamingAssets();
    }

    private void SetTitle(string text, string colorStr)
    {
        if (_title != null)
        {
            _title.text = text;
            if (ColorUtility.TryParseHtmlString(colorStr, out Color color))
            {
                _title.color = color;
            }
        }
    }
}