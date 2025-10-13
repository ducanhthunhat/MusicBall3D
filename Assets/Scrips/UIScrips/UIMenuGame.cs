using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuGame : UICanvas
{
    public void playGame()
    {
        UIManager.Instance.OpenUI<UIGamePlay>();
        UIManager.Instance.CloseUI<UIMenuGame>(0.5f);
    }
}
