using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartUI : UICanvas
{
    public void Start()
    {
        UIManager.Instance.OpenUI<UIMenuGame>();
        UIManager.Instance.CloseUIDirectly<UIGameOver>();
        Time.timeScale = 0;
    }
}
