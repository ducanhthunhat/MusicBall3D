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

    void Start()
    {
        GameOver();
    }
    private void GameOver()
    {
        UIManager.Instance.OpenUI<UIGameOver>();
        UIManager.Instance.PauseGame();
        BeatManager.Instance.PauseMusic();
    }

    public void RestartGame()
    {
        UIManager.Instance.ResumeGame();

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
        );
    }
}
