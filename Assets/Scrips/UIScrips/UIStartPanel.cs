using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIStartPanel : UICanvas
{
    public void playGame()
    {
        //UIManager.Instance.ResumeGame();
        Time.timeScale = 1;
        UIManager.Instance.CloseUIDirectly<UIStartPanel>();
        UIManager.Instance.OpenUI<UIPauseGame>();
      
        BeatManager.Instance.PlayMusic();
    }
}
