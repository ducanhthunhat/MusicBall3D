using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIStartPanel : UICanvas
{
    void Start()
    {
        UIManager.Instance.PauseGame();

    }
    public void playGame()
    {
        UIManager.Instance.ResumeGame();
        // Time.timeScale = 1;
        UIManager.Instance.CloseUIDirectly<UIStartPanel>();

        BeatManager.Instance.PlayMusic();
    }
}
