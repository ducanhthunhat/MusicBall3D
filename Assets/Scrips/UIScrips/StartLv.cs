using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StartUI : UICanvas
{
    public void Start()
    {
        UIManager.Instance.OpenUI<UIStartPanel>();
        Time.timeScale = 0;
    }
}
