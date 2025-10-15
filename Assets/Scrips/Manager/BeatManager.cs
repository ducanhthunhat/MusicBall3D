using UnityEngine;

public class BeatManager : MonoBehaviour
{
    public static BeatManager Instance;

    [SerializeField] private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void PauseMusic()
    {
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        {
            musicSource.UnPause();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
