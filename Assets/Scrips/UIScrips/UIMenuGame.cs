using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuGame : UICanvas
{
    public void playGame()
    {
        UIManager.Instance.CloseUIDirectly<UIMenuGame>();
        UIManager.Instance.OpenUI<UIStartPanel>();
    }
}
