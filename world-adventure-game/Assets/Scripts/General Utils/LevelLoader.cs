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
        }

        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void RestartGame()
    {
        if (Health.Instance.GetIsGameOver())
        {
            int selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
            PlayerPrefs.SetInt("Score" + selectedPlayer, 0);
            PlayerPrefs.SetInt("CurrentScore" + selectedPlayer, 0);
            PlayerPrefs.Save();
        }

        AudioManager.Instance.PlaySound("interaction");
        UIManager.Instance.setPausedScreen(false);
        FreezePlayer.Instance.Freeze();
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex));
        StartCoroutine(Health.Instance.AlwaysInvincible());
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        if (Health.Instance.GetIsGameOver())
        {
            int selectedPlayer = PlayerPrefs.GetInt("SelectedPlayer");
            CollectiblesManager.Instance?.SaveTotalScore();
        }

        AudioManager.Instance.PlaySound("interaction");
        FreezePlayer.Instance.Freeze();
        StartCoroutine(LoadLevel(0));   
        StartCoroutine(Health.Instance.AlwaysInvincible());
        Time.timeScale = 1;
    }

    IEnumerator LoadLevel(int levelIndex)
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
