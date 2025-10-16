using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPauseGame : UICanvas
{
    public void pauseGame()
    {
        UIManager.Instance.PauseGame();
    }
}
