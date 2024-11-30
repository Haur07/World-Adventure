using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Teste. Futuramente o áudio será realocado para AudioManager
    [SerializeField] private GameObject pausedScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSong;
    private AudioSource audioSource;
    private bool isPaused;

    private void Awake()
    {
        isPaused = false;
        pausedScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public bool getIsPaused()
    {
        return isPaused;
    }

    public bool getGameOverScreen()
    {
        return gameOverScreen;
    }

    public void setPausedScreen(bool value)
    {
        pausedScreen.SetActive(value);
    }

    public void setGameOverScreen(bool value)
    {
        gameOverScreen.SetActive(value);
    }

    public void Paused()
    {
        pausedScreen.SetActive(true);
        isPaused = true;
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        pausedScreen.SetActive(false);
        isPaused = false;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        audioSource.PlayOneShot(gameOverSong);
    }
}
