using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader Instance;

    [SerializeField] private Animator transition;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            int selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
            PlayerPrefs.SetInt("Score" + selectedPlayer, 0);
            PlayerPrefs.Save();
        }

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartGame()
    {
        int selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");

        if (Health.Instance.GetIsGameOver())
        {
            PlayerPrefs.SetInt("Score" + selectedPlayer, 0);
            PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, 0);
        }
        else
        {
            int currentScore = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer);
            int removeScore = CollectiblesManager.Instance.GetToBeSavedScore();

            PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, currentScore - removeScore);
        }

        PlayerPrefs.Save();
        AudioManager.Instance.PlaySound("interaction");
        UIManager.Instance.setPausedScreen(false);
        FreezePlayer.Instance.Freeze();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        StartCoroutine(Health.Instance.AlwaysInvincible());
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        AudioManager.Instance.PlaySound("interaction");

        if (Health.Instance.GetIsGameOver() && SceneManager.GetActiveScene().buildIndex > 1)
        {
            int selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
            int currentScore = PlayerPrefs.GetInt("CurrentScore" + selectedPlayer, 0);
            int time = TimeElapsedManager.Instance.GetTimeElapsed();
            PlayerPrefs.SetInt("CurrentScoreNoTime" + selectedPlayer, currentScore);
            PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, currentScore - time);
            CollectiblesManager.Instance.SaveTotalScore();
            PostGameScreenManager.Instance.gameObject.SetActive(true);
            PostGameScreenManager.Instance.ShowPostGameScreen();
            return;
        }

        FreezePlayer.Instance.Freeze();
        StartCoroutine(LoadLevel(0));   
        StartCoroutine(Health.Instance.AlwaysInvincible());
        Time.timeScale = 1;
    }

    public IEnumerator LoadLevel(int levelIndex)
    {
        if (levelIndex > 4)
        {
            transition.SetTrigger("start");
            yield return new WaitForSeconds(2);
            SceneManager.LoadScene(0);
        }

        transition.SetTrigger("start");
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(levelIndex);
    }
}
