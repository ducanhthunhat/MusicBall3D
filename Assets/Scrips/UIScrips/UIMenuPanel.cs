using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuPanel : UICanvas
{

    public void Song_1()
    {
        UIManager.Instance.CloseUIDirectly<UIMenuPanel>();
        LevelManager.Instance.LoadLevel(0);
        UIManager.Instance.OpenUI<UIStartPanel>();
    }
}
