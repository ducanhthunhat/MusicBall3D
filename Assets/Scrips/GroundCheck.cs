using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.SceneManagement;

public class GroundCheck : MonoBehaviour
{
    public static event Action OnFall;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnFall?.Invoke();
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f; // Khôi phục tốc độ thời gian
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại scene hiện tại
    }
}
