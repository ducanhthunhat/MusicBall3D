using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameOver : UICanvas
{
    public void RestartGame()
    {
        UIManager.Instance.CloseUIDirectly<UIGameOver>();
        UIManager.Instance.ResumeGame();
        UIManager.Instance.RestartGame();
    }
    public void QuitToMainMenu()
    {
        UIManager.Instance.CloseUIDirectly<UIGameOver>();
        UIManager.Instance.ResumeGame();
        UIManager.Instance.QuitGame();
    }
}

