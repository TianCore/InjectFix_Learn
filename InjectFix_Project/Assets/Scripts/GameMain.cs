using UnityEngine;

public class GameMain : MonoBehaviour
{
    private void Awake()
    {
        Debug.Log("¿ªÊ¼ÓÎÏ·");
        GameManager.Instance.LoadMainUI();
    }
}
