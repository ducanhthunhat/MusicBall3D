using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class UIStartPanel : UICanvas
{
    public void playGame()
    {
        UIManager.Instance.CloseUIDirectly<UIStartPanel>();
        UIManager.Instance.OpenUI<UIPauseGame>();
        UIManager.Instance.ResumeGame();
        BeatManager.Instance.PlayMusic();
    }
}
