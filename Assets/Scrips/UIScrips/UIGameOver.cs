using UnityEngine;

public class UIGameOver : UICanvas
{
    private void OnEnable()
    {
        GroundCheck.OnFall += GameOver;
    }

    private void OnDisable()
    {
        GroundCheck.OnFall -= GameOver;
    }
    private void GameOver()
    {
        UIManager.Instance.OpenUI<UIGameOver>();
        UIManager.Instance.CloseUIDirectly<UIPauseGame>();
        UIManager.Instance.PauseGame();
        BeatManager.Instance.PauseMusic();
    }

    public void RestartGame()
    {
        UIManager.Instance.ResetLevel();
    }
}
