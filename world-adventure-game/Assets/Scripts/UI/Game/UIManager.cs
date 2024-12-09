using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject pausedScreen;
    [SerializeField] private GameObject gameOverScreen;

    private bool isPaused;
    private bool pauseDisabled;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            isPaused = false;
            pauseDisabled = false;
            pausedScreen.SetActive(false);
            gameOverScreen.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused) {
            Unpause();
        } 
    }

    public bool getGameOverScreen()
    {
        return gameOverScreen;
    }

    public void setGameOverScreen(bool value)
    {
        gameOverScreen.SetActive(value);
    }

    public void setPausedScreen(bool value)
    {
        pausedScreen.SetActive(value);
    }

    public bool getPauseDisabled()
    {
        return pauseDisabled;
    }

    public void setPauseDisabled(bool value)
    {
        pauseDisabled = value;
    }

    public void Pause()
    {
        if (!Health.Instance.GetIsGameOver() && !pauseDisabled)
        {
            AudioManager.Instance.PlaySound("interaction");
            isPaused = true;
            pausedScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Unpause()
    {
        AudioManager.Instance.PlaySound("interaction");
        isPaused = false;
        pausedScreen.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        AudioManager.Instance.PlaySound("gameover", loop: true);
    }
}
