using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGamePlay : UICanvas
{
    public void MenuGame()
    {
        UIManager.Instance.OpenUI<UIMenuGame>();
        UIManager.Instance.CloseUI<UIGamePlay>(0.5f);

    }
}
